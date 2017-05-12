using MyWork.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.ViewModels
{
    public class BacklogDetailItemViewModel : BaseViewModel
    {
        private static BacklogDetailItemViewModel _me;
        public static BacklogDetailItemViewModel GetInstance()
        {
            if (_me == null)
            {
                _me = new BacklogDetailItemViewModel();
            }
            return _me;
        }

        public MyWorkPageViewModel Context;

        public BacklogDetailItemViewModel()
        {
            CloseBacklogDetailCommand = new RelayCommand(CloseBacklogDetail);
        }

        private RelayCommand CloseBacklogDetailCommand;

        private void CloseBacklogDetail()
        {
            Context.CloseBacklogDetailDialog();
        }
    }
}
