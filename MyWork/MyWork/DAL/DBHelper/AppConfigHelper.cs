using MyWork.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.DAL.DBHelper
{
    public class AppConfigHelper
    {
        protected static AppConfigHelper me;
        private readonly Repository db;

        public AppConfigHelper()
        {
            db = Repository.GetInstance();
        }

        public static AppConfigHelper GetInstance()
        {
            if (me == null)
            {
                me = new AppConfigHelper();
            }

            return me;
        }

        public AppConfiguration GetAppConfiguration()
        {
            return GetAllAppConfiguration().FirstOrDefault();
        }

        public List<AppConfiguration> GetAllAppConfiguration()
        {
            //string query = "select * from UserAccount";
            return db.GetItems<AppConfiguration>();
        }

        public AppConfiguration GetAppConfigurationById(int id)
        {
            return db.GetItem<AppConfiguration>(id);
        }

        public void SaveAppConfiguration(AppConfiguration appConfiguration)
        {
            db.SaveItem(appConfiguration);
        }

        public void DeleteAppConfiguration(AppConfiguration appConfiguration)
        {
            var existItem = GetAppConfigurationById(appConfiguration.Id);
            if (existItem != null)
            {
                db.DeleteItem(appConfiguration);
            }
        }
    }
}
