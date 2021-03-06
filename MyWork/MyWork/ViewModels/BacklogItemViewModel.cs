﻿using MyWork.BusinessLogicLayer.Manager;
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
    public class BacklogItemViewModel : BaseViewModel
    {
        private static BacklogItemViewModel _me;
        public static BacklogItemViewModel GetInstance()
        {
            if (_me == null)
            {
                _me = new BacklogItemViewModel();
            }
            return _me;
        }

        public BacklogItemViewModel()
        {
            GeneratePriorityList();
            GenerateStatusList();
            CancelCommand = new RelayCommand(CancelAction);
            SaveCommand = new RelayCommand(SaveBacklogItem);
            ActiveSpints = SprintManager.GetInstance().GetAllActiveSprint().OrderBy(x => x.SprintNum).ToList();
        }

        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand SaveCommand { get; private set; }

        private List<string> priorityList = new List<string>();
        public List<string> PriorityList
        {
            get { return priorityList; }
            set { SetProperty(ref priorityList, value); }
        }

        private List<string> statusList = new List<string>();
        public List<string> StatusList
        {
            get { return statusList; }
            set { SetProperty(ref statusList, value); }
        }

        private List<Sprint> activeSpints = new List<Sprint>();
        public List<Sprint> ActiveSpints
        {
            get { return activeSpints; }
            set { SetProperty(ref activeSpints, value); }
        }

        private int sprintIndex = 0;
        public int SprintIndex
        {
            get { return sprintIndex; }
            set
            {
                SetProperty(ref sprintIndex, value);
            }
        }

        private Backlog currentBacklog = new Backlog();
        public Backlog CurrentBacklog
        {
            get { return currentBacklog; }
            set { SetProperty(ref currentBacklog, value); }
        }

        private string addNewBacklogErrorMessage = "";
        public string AddNewBacklogErrorMessage
        {
            get { return addNewBacklogErrorMessage; }
            set { SetProperty(ref addNewBacklogErrorMessage, value); }
        }

        public MyWorkPageViewModel Context;

        private void GeneratePriorityList()
        {
            List<string> temp = new List<string>();
            temp.Add("High");
            temp.Add("Medium");
            temp.Add("Low");
            temp.Add("None");
            PriorityList = temp;
        }

        private void GenerateStatusList()
        {
            List<string> temp = new List<string>();
            temp.Add("Future");
            temp.Add("In Progress");
            temp.Add("Done");
            temp.Add("Accept");
            StatusList = temp;
        }

        private void CancelAction()
        {
            AddNewBacklogErrorMessage = "";
            CurrentBacklog = new Backlog();
            SprintIndex = 0;
            Context.CloseAddNewBacklogDialog();
        }

        private async void SaveBacklogItem()
        {
            Context.ProgressRingVisibility = Visibility.Visible;
            await Task.Delay(2000);
            if (string.IsNullOrEmpty(CurrentBacklog.BacklogTitle))
            {
                AddNewBacklogErrorMessage = "Please input title for new Backlog.";
                Context.ProgressRingVisibility = Visibility.Collapsed;
                return;
            }
            if (CurrentBacklog.EstimateHour <= 0 && CurrentBacklog.Id == 0)
            {
                AddNewBacklogErrorMessage = "Please input Positive number for estimate hours.";
                Context.ProgressRingVisibility = Visibility.Collapsed;
                return;
            }
            if (ActiveSpints == null || ActiveSpints.Count == 0)
            {
                AddNewBacklogErrorMessage = "There are no active sprint to add.";
                Context.ProgressRingVisibility = Visibility.Collapsed;
                return;
            }
            var appConfig = AppConfigurationManager.GetInstance().GetAppConfiguration();
            if (appConfig == null)
            {
                AddNewBacklogErrorMessage = "There is an error when create new backlog. Please try it again later.";
                Context.ProgressRingVisibility = Visibility.Collapsed;
                return;
            }
            var user = UserAccountManager.GetInstance().GetUserAccountById(appConfig.UserAccountId);
            if (user == null)
            {
                AddNewBacklogErrorMessage = "There is an error when create new backlog. Please try it again later.";
                Context.ProgressRingVisibility = Visibility.Collapsed;
                return;
            }
            CurrentBacklog.UserId = appConfig.UserAccountId;
            CurrentBacklog.UserName = user.UserName;
            CurrentBacklog.Sprint = ActiveSpints[SprintIndex].SprintNum;
            BacklogManager.GetInstance().SaveBacklog(CurrentBacklog);
            Context.ProgressRingVisibility = Visibility.Collapsed;
            //AddNewBacklogErrorMessage = "";
            //CurrentBacklog = new Backlog();
            //SprintIndex = 0;
            //Context.CloseAddNewBacklogDialog();
            CancelAction();
            Context.RefreshPage();
        }
    }
}
