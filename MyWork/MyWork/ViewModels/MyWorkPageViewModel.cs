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
    public class MyWorkPageViewModel : BaseViewModel
    {
        private static MyWorkPageViewModel _me;
        public static MyWorkPageViewModel GetInstance()
        {
            if (_me == null)
            {
                _me = new MyWorkPageViewModel();
            }
            return _me;
        }

        public MyWorkPageViewModel()
        {
            BackToHomePageCommand = new RelayCommand(BackToHomePage);
            AddNewBacklogCommand = new RelayCommand(ShowAddNewBacklogDialog);
            ActiveSpints = SprintManager.GetInstance().GetAllActiveSprint().OrderBy(x => x.SprintNum).ToList();
        }

        private BacklogItemViewModel backlogItemViewModel;
        public BacklogItemViewModel BacklogItemViewModel
        {
            get
            {
                if (backlogItemViewModel == null)
                {
                    backlogItemViewModel = new BacklogItemViewModel();
                    backlogItemViewModel.Context = this;
                }

                return backlogItemViewModel;
            }
            set { SetProperty(ref backlogItemViewModel, value); }
        }

        public RelayCommand BackToHomePageCommand { get; private set; }

        public RelayCommand AddNewBacklogCommand { get; private set; }

        private Visibility backlogItemVisibility = Visibility.Collapsed;
        public Visibility BacklogItemVisibility
        {
            get { return backlogItemVisibility; }
            set { SetProperty(ref backlogItemVisibility, value); }
        }

        private Visibility progressRingVisibility = Visibility.Collapsed;
        public Visibility ProgressRingVisibility
        {
            get { return progressRingVisibility; }
            set { SetProperty(ref progressRingVisibility, value); }
        }

        private List<Sprint> activeSpints = new List<Sprint>();
        public List<Sprint> ActiveSpints
        {
            get { return activeSpints; }
            set { SetProperty(ref activeSpints, value); }
        }

        private List<Backlog> futureBacklogs = new List<Backlog>();
        public List<Backlog> FutureBacklogs
        {
            get { return futureBacklogs; }
            set { SetProperty(ref futureBacklogs, value); }
        }

        private List<Backlog> inprogressBacklogs = new List<Backlog>();
        public List<Backlog> InprogressBacklogs
        {
            get { return inprogressBacklogs; }
            set { SetProperty(ref inprogressBacklogs, value); }
        }

        private List<Backlog> doneBacklogs = new List<Backlog>();
        public List<Backlog> DoneBacklogs
        {
            get { return doneBacklogs; }
            set { SetProperty(ref doneBacklogs, value); }
        }

        private List<Backlog> acceptBacklogs = new List<Backlog>();
        public List<Backlog> AcceptBacklogs
        {
            get { return acceptBacklogs; }
            set { SetProperty(ref acceptBacklogs, value); }
        }


        private int currentSprintIndex = 0;
        public int CurrentSprintIndex
        {
            get { return currentSprintIndex; }
            set
            {
                SetProperty(ref currentSprintIndex, value);
                RefreshPage();
            }
        }

        private double possitionX = 0;
        public double PossitionX
        {
            get { return possitionX; }
            set { SetProperty(ref possitionX, value); }
        }

        private double possitionY = 0;
        public double PossitionY
        {
            get { return possitionY; }
            set { SetProperty(ref possitionY, value); }
        }

        public void RefreshPage()
        {
            var appConfig = AppConfigurationManager.GetInstance().GetAppConfiguration();
            if(appConfig != null && ActiveSpints != null && ActiveSpints.Count > 0)
            {
                LoadFutureBacklog(appConfig.UserAccountId, ActiveSpints[CurrentSprintIndex].SprintNum);
                LoadInprogressBacklog(appConfig.UserAccountId, ActiveSpints[CurrentSprintIndex].SprintNum);
                LoadDoneBacklog(appConfig.UserAccountId, ActiveSpints[CurrentSprintIndex].SprintNum);
                LoadAcceptBacklog(appConfig.UserAccountId, ActiveSpints[CurrentSprintIndex].SprintNum);
            }
        }

        public void CloseAddNewBacklogDialog()
        {
            BacklogItemVisibility = Visibility.Collapsed;
        }

        private void LoadInprogressBacklog(int userId, int sprint)
        {
            InprogressBacklogs = BacklogManager.GetInstance().GetAllInProgressBacklog(userId, sprint);
        }

        private void LoadFutureBacklog(int userId, int sprint)
        {
            FutureBacklogs = BacklogManager.GetInstance().GetAllFutureBacklog(userId, sprint);
        }

        private void LoadDoneBacklog(int userId, int sprint)
        {
            DoneBacklogs = BacklogManager.GetInstance().GetAllDoneBacklog(userId, sprint);
        }

        private void LoadAcceptBacklog(int userId, int sprint)
        {
            AcceptBacklogs = BacklogManager.GetInstance().GetAllAcceptBacklog(userId, sprint);
        }

        private void ShowAddNewBacklogDialog()
        {
            BacklogItemVisibility = Visibility.Visible;
        }
        
        private void BackToHomePage()
        {
            ApplicationViewModel.Default.NavigateToMainPage();
        }
    }
}
