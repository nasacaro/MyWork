using MyWork.BusinessLogicLayer.Manager;
using MyWork.Common;
using MyWork.Models.DBModels;
using MyWork.Models.DisplayModels;
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

        private List<BacklogModel> futureBacklogs = new List<BacklogModel>();
        public List<BacklogModel> FutureBacklogs
        {
            get { return futureBacklogs; }
            set { SetProperty(ref futureBacklogs, value); }
        }

        private List<BacklogModel> inprogressBacklogs = new List<BacklogModel>();
        public List<BacklogModel> InprogressBacklogs
        {
            get { return inprogressBacklogs; }
            set { SetProperty(ref inprogressBacklogs, value); }
        }

        private List<BacklogModel> doneBacklogs = new List<BacklogModel>();
        public List<BacklogModel> DoneBacklogs
        {
            get { return doneBacklogs; }
            set { SetProperty(ref doneBacklogs, value); }
        }

        private List<BacklogModel> acceptBacklogs = new List<BacklogModel>();
        public List<BacklogModel> AcceptBacklogs
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
            var inprogressBacklogs = BacklogManager.GetInstance().GetAllInProgressBacklog(userId, sprint);
            var backlogModels = new List<BacklogModel>();
            foreach (var backlog in inprogressBacklogs)
            {
                var model = new BacklogModel()
                {
                    Backlog = backlog
                };
                model.EditBacklog += EditBacklog;
                backlogModels.Add(model);
            }

            InprogressBacklogs = backlogModels;
        }

        private void LoadFutureBacklog(int userId, int sprint)
        {
            var futureBacklogs = BacklogManager.GetInstance().GetAllFutureBacklog(userId, sprint);
            var backlogModels = new List<BacklogModel>();
            foreach(var backlog in futureBacklogs)
            {
                var model = new BacklogModel()
                {
                    Backlog = backlog
                };
                model.EditBacklog += EditBacklog;
                backlogModels.Add(model);
            }

            FutureBacklogs = backlogModels;
        }

        private void LoadDoneBacklog(int userId, int sprint)
        {
            var doneBacklogs = BacklogManager.GetInstance().GetAllDoneBacklog(userId, sprint);
            var backlogModels = new List<BacklogModel>();
            foreach (var backlog in doneBacklogs)
            {
                var model = new BacklogModel()
                {
                    Backlog = backlog
                };
                model.EditBacklog += EditBacklog;
                backlogModels.Add(model);
            }

            DoneBacklogs = backlogModels;
        }

        private void LoadAcceptBacklog(int userId, int sprint)
        {
            var acceptBacklogs = BacklogManager.GetInstance().GetAllAcceptBacklog(userId, sprint);
            var backlogModels = new List<BacklogModel>();
            foreach (var backlog in acceptBacklogs)
            {
                var model = new BacklogModel()
                {
                    Backlog = backlog
                };
                model.EditBacklog += EditBacklog;
                backlogModels.Add(model);
            }

            AcceptBacklogs = backlogModels;
        }

        private void EditBacklog(object sender, EventArgs e)
        {
            BacklogModel editBacklog = (BacklogModel)sender;
            if(editBacklog != null)
            {
                BacklogItemViewModel.CurrentBacklog = editBacklog.Backlog;
                ShowAddNewBacklogDialog();
            }
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
