using MyWork.BusinessLogicLayer.Manager;
using MyWork.Common;
using MyWork.Common.DelegateCommand;
using MyWork.Models.DBModels;
using MyWork.Models.DisplayModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;

namespace MyWork.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private static MainPageViewModel _me;

        public bool IsEditMode { get; set; }

        public List<UserAccountModel> FullUserList { get; set; }

        public static MainPageViewModel GetInstance()
        {
            if (_me == null)
            {
                _me = new MainPageViewModel();
            }
            return _me;
        }

        public MainPageViewModel()
        {
            ImportDataCommand = new RelayCommand(ShowSideloadDataDialog);
            SelectFileToSideloadCommand = new RelayCommand(SelectFile);
            SideloadFileCommand = new RelayCommand(SideloadFile);
            AddNewAccountCommand = new RelayCommand(ShowAddNewAccountDialog);
            EditCommand = new RelayCommand(EnableEditMode);
            SearchCommand = new RelayCommand(OpenSearchMode);
            CompleteCommand = new RelayCommand(DisableEditMode);
            ChangeSortTypeCommand = new DelegateCommand(ChangeSortType);
            DeleteSelectedItemCommand = new RelayCommand(DeleteSelectedItem);
            GotoMyWorkPageCommand = new RelayCommand(GotoMyWorkPage);
            IsEditMode = false; 
        }

        private LoginViewModel loginViewModel;
        public LoginViewModel LoginViewModel
        {
            get
            {
                if (loginViewModel == null)
                {
                    loginViewModel = new LoginViewModel();
                    loginViewModel.Context = this;
                }

                return loginViewModel;
            }
            set { SetProperty(ref loginViewModel, value); }
        }

        private AddNewAccountViewModel addNewAccountViewModel;
        public AddNewAccountViewModel AddNewAccountViewModel
        {
            get
            {
                if (addNewAccountViewModel == null)
                {
                    addNewAccountViewModel = new AddNewAccountViewModel();
                    addNewAccountViewModel.Context = this;
                }

                return addNewAccountViewModel;
            }
            set { SetProperty(ref addNewAccountViewModel, value); }
        }

        public RelayCommand GotoMyWorkPageCommand { get; private set; }

        public RelayCommand ImportDataCommand { get; private set; }        

        public RelayCommand SelectFileToSideloadCommand { get; private set; }

        public RelayCommand SideloadFileCommand { get; private set; }

        public RelayCommand AddNewAccountCommand { get; private set; }

        public RelayCommand EditCommand { get; private set; }

        public RelayCommand SearchCommand { get; private set; }

        public RelayCommand CompleteCommand { get; private set; }

        public RelayCommand DeleteSelectedItemCommand { get; private set; }        

        public DelegateCommand ChangeSortTypeCommand { get; private set; }


        private Visibility sideloadDataDialogVisibility = Visibility.Collapsed;
        public Visibility SideloadDataDialogVisibility
        {
            get { return sideloadDataDialogVisibility; }
            set { SetProperty(ref sideloadDataDialogVisibility, value); }
        }

        private Visibility progressRingVisibility = Visibility.Collapsed;
        public Visibility ProgressRingVisibility
        {
            get { return progressRingVisibility; }
            set { SetProperty(ref progressRingVisibility, value); }
        }

        private Visibility loginViewVisibility = Visibility.Collapsed;
        public Visibility LoginViewVisibility
        {
            get { return loginViewVisibility; }
            set { SetProperty(ref loginViewVisibility, value); }
        }

        private Visibility addNewAccountViewVisibility = Visibility.Collapsed;
        public Visibility AddNewAccountViewVisibility
        {
            get { return addNewAccountViewVisibility; }
            set { SetProperty(ref addNewAccountViewVisibility, value); }
        }

        private Visibility editButtonVisibility = Visibility.Collapsed;
        public Visibility EditButtonVisibility
        {
            get { return editButtonVisibility; }
            set { SetProperty(ref editButtonVisibility, value); }
        }

        private Visibility addNewButtonVisibility = Visibility.Visible;
        public Visibility AddNewButtonVisibility
        {
            get { return addNewButtonVisibility; }
            set { SetProperty(ref addNewButtonVisibility, value); }
        }

        private Visibility completeButtonVisibility = Visibility.Collapsed;
        public Visibility CompleteButtonVisibility
        {
            get { return completeButtonVisibility; }
            set { SetProperty(ref completeButtonVisibility, value); }
        }

        private Visibility searchFieldVisibility = Visibility.Collapsed;
        public Visibility SearchFieldVisibility
        {
            get { return searchFieldVisibility; }
            set { SetProperty(ref searchFieldVisibility, value); }
        }

        private StorageFile sideloadedFile;
        public StorageFile SideloadedFile
        {
            get { return sideloadedFile; }
            set { SetProperty(ref sideloadedFile, value); }
        }

        private string sideloadErrorMessage = "";
        public string SideloadErrorMessage
        {
            get { return sideloadErrorMessage; }
            set { SetProperty(ref sideloadErrorMessage, value); }
        }

        private string searchText = "";
        public string SearchText
        {
            get { return searchText; }
            set { SetProperty(ref searchText, value); }
        }

        private List<UserAccountModel> userList = new List<UserAccountModel>();
        public List<UserAccountModel> UserList
        {
            get { return userList; }
            set { SetProperty(ref userList, value); }
        }

        private int selectedIndex = 0;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { SetProperty(ref selectedIndex, value); }
        }

        public void RefreshPage()
        {
            var userAccountList = UserAccountManager.GetInstance().GetAllUserAccount();
            if(userAccountList != null && userAccountList.Count > 0)
            {
                List<UserAccountModel> userModels = new List<UserAccountModel>();
                //userAccountList = userAccountList.OrderByDescending(x => x.ModifiedDate).ToList();
                foreach (var account in userAccountList)
                {
                    var model = new UserAccountModel()
                    {
                        UserAccount = account
                    };
                    model.ItemClick += GiftItemClick;
                    userModels.Add(model);
                }
                FullUserList = userModels;
                UserList = SortUserList(userModels);
            }
        }

        public void RefreshPageHeader()
        {
            var config = AppConfigurationManager.GetInstance().GetAppConfiguration();
            if(config != null)
            {
                var currentUser = UserAccountManager.GetInstance().GetUserAccountById(config.UserAccountId);
                if(currentUser != null)
                {
                    EditButtonVisibility = currentUser.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void GiftItemClick(object sender, EventArgs e)
        {
            UserAccountModel item = (UserAccountModel)sender;
            if (IsEditMode)
            {

                item.SelectedImageVisibility = item.SelectedImageVisibility == Visibility.Visible 
                                                ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                LoginViewModel.Username = item.UserAccount.UserName;
                ShowLoginDialog();
            }            
        }

        private void ChangeSortType(object obj)
        {
            string sortType = (string)obj;
            switch (sortType)
            {
                case "recent":
                    SelectedIndex = 0;
                    //UserList = UserList.OrderByDescending(x => x.UserAccount.ModifiedDate).ToList();
                    break;
                case "username":
                    SelectedIndex = 1;
                    //UserList = UserList.OrderBy(x => x.UserAccount.UserName).ToList();
                    break;
                case "email":
                    SelectedIndex = 2;
                    //UserList = UserList.OrderBy(x => x.UserAccount.Email).ToList();
                    break;
            }
            UserList = SortUserList(UserList);
        }

        private List<UserAccountModel> SortUserList(List<UserAccountModel> userList)
        {
            switch (SelectedIndex)
            {
                case 0:
                    return userList.OrderByDescending(x => x.UserAccount.ModifiedDate).ToList();
                case 1:
                    return userList.OrderBy(x => x.UserAccount.UserName).ToList();
                case 2:
                    return userList.OrderBy(x => x.UserAccount.Email).ToList();
                default:
                    return userList.OrderByDescending(x => x.UserAccount.ModifiedDate).ToList();
            }
        }

        private void ShowSideloadDataDialog()
        {
            SideloadDataDialogVisibility = Visibility.Visible;           
        }

        private void GotoMyWorkPage()
        {
            var config = AppConfigurationManager.GetInstance().GetAppConfiguration();
            if(config != null)
            {
                ApplicationViewModel.Default.NavigateToMyWorkPage();
            }
            else
            {
                ShowLoginDialog();
            }
        }

        private async void SelectFile()
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".db3");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                SideloadedFile = file;
            }
        }

        private async void SideloadFile()
        {
            if(SideloadedFile == null || string.IsNullOrEmpty(SideloadedFile.Name) || !ValidExtensionsDataFile(SideloadedFile.Name))
            {
                SideloadErrorMessage = "Please select a valid file to sideload. (Note : The full name of valid file is MyWork.db3)";
                return;
            }
            SideloadErrorMessage = "";
            ProgressRingVisibility = Visibility.Visible;
            try
            {
                await Task.Delay(2000);
                var desFolder = await StorageFolder.GetFolderFromPathAsync(ApplicationData.Current.LocalFolder.Path);
                if (desFolder != null)
                {
                    await SideloadedFile.CopyAsync(desFolder, SideloadedFile.Name, NameCollisionOption.ReplaceExisting);
                }
                SideloadDataDialogVisibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                SideloadErrorMessage = "There are some error when sideload data file. Please try it again.";
            }
            ProgressRingVisibility = Visibility.Collapsed;
        }

        private bool ValidExtensionsDataFile(string fileName)
        {
            int index = fileName.IndexOf(".");
            if (index < 0) return false;
            return fileName.Substring(index + 1).Equals("db3");
        }

        private void ShowLoginDialog()
        {
            LoginViewVisibility = Visibility.Visible;
        }

        private void ShowAddNewAccountDialog()
        {
            AddNewAccountViewVisibility = Visibility.Visible;
        }

        private void DeleteSelectedItem()
        {
            foreach (var item in UserList)
            {
                if (item.SelectedImageVisibility == Visibility.Visible)
                {
                    UserAccountManager.GetInstance().DeleteUserAccount(item.UserAccount);
                    RefreshPage();
                }
            }
        }

        private void EnableEditMode()
        {
            IsEditMode = true;
            EditButtonVisibility = Visibility.Collapsed;
            AddNewButtonVisibility = Visibility.Collapsed;
            CompleteButtonVisibility = Visibility.Visible;
        }

        private void OpenSearchMode()
        {
            if(SearchFieldVisibility == Visibility.Visible)
            {
                SearchFieldVisibility = Visibility.Collapsed;
                SearchText = "";
            }
            else
            {
                SearchFieldVisibility = Visibility.Visible;
            }
        }

        private void DisableEditMode()
        {
            IsEditMode = false;
            EditButtonVisibility = Visibility.Visible;
            AddNewButtonVisibility = Visibility.Visible;
            CompleteButtonVisibility = Visibility.Collapsed;
            foreach(var item in UserList)
            {
                item.SelectedImageVisibility = Visibility.Collapsed;
            }
        }

        public void HideLoginDialog()
        {
            LoginViewModel.Username = "";
            LoginViewModel.Password = "";
            LoginViewVisibility = Visibility.Collapsed;
        }

        public void HideSideloadDataDialog()
        {
            SideloadDataDialogVisibility = Visibility.Collapsed;
        }

        public void SearchUserFromList(string searchKey)
        {
            UserList = SortUserList(FullUserList.Where(x => x.UserAccount.UserName.ToLower().Contains(searchKey.ToLower())
                                        || x.UserAccount.Email.ToLower().Contains(searchKey.ToLower())).ToList());
        }
    }
}
