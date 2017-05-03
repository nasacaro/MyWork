using MyWork.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public RelayCommand BackToHomePageCommand { get; private set; }

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

        }

        private void BackToHomePage()
        {
            ApplicationViewModel.Default.NavigateToMainPage();
        }
    }
}
