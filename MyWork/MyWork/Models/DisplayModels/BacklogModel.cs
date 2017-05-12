using MyWork.Common;
using MyWork.Models.DBModels;
using MyWork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Models.DisplayModels
{
    public class BacklogModel : BaseViewModel
    {
        public BacklogModel()
        {
            EditBacklogCommand = new RelayCommand(OnEditBacklog);
            QuickCloseBacklogCommand = new RelayCommand(QuickCloseBacklog);
            OpenBacklogDetailCommand = new RelayCommand(OpenBacklogDetail);
        }

        public Backlog Backlog { get; set; }

        public RelayCommand EditBacklogCommand { get; private set; }

        public RelayCommand QuickCloseBacklogCommand { get; private set; }

        public RelayCommand OpenBacklogDetailCommand { get; private set; }

        public event EventHandler EditBacklog;

        public event EventHandler QuickClose;

        public event EventHandler OpenDetail;

        private void OnEditBacklog()
        {
            if (EditBacklog != null)
            {
                EditBacklog(this, EventArgs.Empty);
            }
        }

        private void QuickCloseBacklog()
        {
            if (QuickClose != null)
            {
                QuickClose(this, EventArgs.Empty);
            }
        }

        private void OpenBacklogDetail()
        {
            if (OpenDetail != null)
            {
                OpenDetail(this, EventArgs.Empty);
            }
        }
    }
}
