using MyWork.Common;
using MyWork.Models.DBModels;
using MyWork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MyWork.Models.DisplayModels
{
    public class UserAccountModel : BaseViewModel
    {
        public UserAccountModel()
        {
            ItemClickCommand = new RelayCommand(OnItemClick);
        }

        public UserAccount UserAccount { get; set; }

        public RelayCommand ItemClickCommand { get; private set; }

        public event EventHandler ItemClick;

        private Visibility selectedImageVisibility = Visibility.Collapsed;
        public Visibility SelectedImageVisibility
        {
            get { return selectedImageVisibility; }
            set { SetProperty(ref selectedImageVisibility, value); }
        }

        private void OnItemClick()
        {
            if (ItemClick != null)
            {
                ItemClick(this, EventArgs.Empty);
            }
        }
    }
}
