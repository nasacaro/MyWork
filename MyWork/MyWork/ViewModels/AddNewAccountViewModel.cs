using MyWork.BusinessLogicLayer.Manager;
using MyWork.BusinessLogicLayer.Utilities;
using MyWork.Common;
using MyWork.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MyWork.ViewModels
{
    public class AddNewAccountViewModel :BaseViewModel
    {
        public AddNewAccountViewModel()
        {
            CancelAddNewCommand = new RelayCommand(CancelAddNewAccount);
            AddNewCommand = new RelayCommand(AddNewAccount);
        }

        public MainPageViewModel Context;

        public RelayCommand CancelAddNewCommand { get; private set; }

        public RelayCommand AddNewCommand { get; private set; }

        private string username = "";
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private string password = "";
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private string reEnterPassword = "";
        public string ReEnterPassword
        {
            get { return reEnterPassword; }
            set { SetProperty(ref reEnterPassword, value); }
        }

        private string email = "";
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private string addNewErrorMessage = "";
        public string AddNewErrorMessage
        {
            get { return addNewErrorMessage; }
            set { SetProperty(ref addNewErrorMessage, value); }
        }

        private async void AddNewAccount()
        {
            if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ReEnterPassword) || string.IsNullOrEmpty(Email))
            {
                AddNewErrorMessage = "Please input the required info for your account.";
                return;
            }
            AddNewErrorMessage = "";
            if (!Password.Equals(ReEnterPassword))
            {
                AddNewErrorMessage = "The re-enter password not match.";
                return;
            }
            if (!EmailUtils.ValidateEmailFormat(Email))
            {
                AddNewErrorMessage = "Invalid email format.";
                return;
            }

            if (UserAccountManager.GetInstance().CheckUserExists(Username.ToLower()))
            {
                AddNewErrorMessage = "User already exists.";
                return;
            }

            try
            {
                Context.ProgressRingVisibility = Visibility.Visible;
                await Task.Delay(2000);
                var user = new UserAccount()
                {
                    UserName = Username.ToLower(),
                    Password = Utils.GenerateMD5String(Password),
                    ModifiedDate = DateTime.Now,
                    Email = Email,
                    IsAdmin = false
                };
                UserAccountManager.GetInstance().InsertNewUserAccount(user);
            }
            catch (Exception)
            {
                AddNewErrorMessage = "There was an error when add new account. Please try again.";
                Context.ProgressRingVisibility = Visibility.Collapsed;
                return;
            }
            int userId = UserAccountManager.GetInstance().GetUserId(Username.ToLower(), Password);
            if(userId > 0)
            {
                var appConfig = AppConfigurationManager.GetInstance().GetAppConfiguration();
                if (appConfig == null)
                {
                    appConfig = new AppConfiguration();
                }
                appConfig.UserAccountId = userId;
                AppConfigurationManager.GetInstance().SaveAppConfiguration(appConfig);
            }
            
            Context.ProgressRingVisibility = Visibility.Collapsed;
            Context.AddNewAccountViewVisibility = Visibility.Collapsed;
            ApplicationViewModel.Default.NavigateToMyWorkPage();
        }

        private void CancelAddNewAccount()
        {
            Context.AddNewAccountViewVisibility = Visibility.Collapsed;
        }
    }
}
