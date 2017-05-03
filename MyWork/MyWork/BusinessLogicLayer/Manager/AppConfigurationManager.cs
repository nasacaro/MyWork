using MyWork.DAL.DBHelper;
using MyWork.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.BusinessLogicLayer.Manager
{
    public class AppConfigurationManager
    {
        protected static AppConfigurationManager me;
        public static AppConfigurationManager GetInstance()
        {
            if (me == null)
            {
                me = new AppConfigurationManager();
            }

            return me;
        }

        public void SaveAppConfiguration(AppConfiguration appConfiguration)
        {
            AppConfigHelper.GetInstance().SaveAppConfiguration(appConfiguration);
        }

        public AppConfiguration GetAppConfiguration()
        {
            return AppConfigHelper.GetInstance().GetAppConfiguration();
        }

        public List<AppConfiguration> GetAllAppConfiguration()
        {
            return AppConfigHelper.GetInstance().GetAllAppConfiguration();
        }

        public void DeleteAppConfiguration(AppConfiguration appConfiguration)
        {
            AppConfigHelper.GetInstance().DeleteAppConfiguration(appConfiguration);
        }
    }
}
