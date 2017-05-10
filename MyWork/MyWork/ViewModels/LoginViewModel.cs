using MyWork.BusinessLogicLayer.Manager;
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
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            CancelLoginCommand = new RelayCommand(CancelLogin);
            LoginCommand = new RelayCommand(DoLogin);
        }

        public MainPageViewModel Context;

        public RelayCommand CancelLoginCommand { get; private set; }

        public RelayCommand LoginCommand { get; private set; }

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

        private string loginErrorMessage = "";
        public string LoginErrorMessage
        {
            get { return loginErrorMessage; }
            set { SetProperty(ref loginErrorMessage, value); }
        }

        private void CancelLogin()
        {
            LoginErrorMessage = "";
            Context.HideLoginDialog();
        }

        private async void DoLogin()
        {
            Context.ProgressRingVisibility = Visibility.Visible;
            await Task.Delay(2000);
            LoginErrorMessage = "";
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                LoginErrorMessage = "Please input username and password to log in";
                Context.ProgressRingVisibility = Visibility.Collapsed;
                return; 
            }
            var user = UserAccountManager.GetInstance().DoLogIn(Username.ToLower(), Password);
            if (user == null)
            {
                LoginErrorMessage = "Incorrect User Name or Password. Please try again.";
                Context.ProgressRingVisibility = Visibility.Collapsed;
                return;
            }
            var appConfig = AppConfigurationManager.GetInstance().GetAppConfiguration();
            if(appConfig == null)
            {
                appConfig = new AppConfiguration();
            }
            appConfig.UserAccountId = user.Id;
            AppConfigurationManager.GetInstance().SaveAppConfiguration(appConfig);
            user.ModifiedDate = DateTime.Now;
            UserAccountManager.GetInstance().UpdateUserAccount(user);
            Context.HideLoginDialog();
            Context.RefreshPageHeader();
            Context.ProgressRingVisibility = Visibility.Collapsed;
            ApplicationViewModel.Default.NavigateToMyWorkPage();
        }
    }
}
