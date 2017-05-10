using MyWork.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.DAL.DBHelper
{
    public class SprintHelper
    {
        protected static SprintHelper me;
        private readonly Repository db;

        public SprintHelper()
        {
            db = Repository.GetInstance();
        }

        public static SprintHelper GetInstance()
        {
            if (me == null)
            {
                me = new SprintHelper();
            }

            return me;
        }

        public List<Sprint> GetAllSprint()
        {
            //string query = "select * from UserAccount";
            return db.GetItems<Sprint>();
        }

        public List<Sprint> GetAllActiveSprint()
        {
            string query = string.Format("select * from Sprint order by SprintNum desc limit 20");
            return db.GetItems<Sprint>(query);
        }

        public Sprint GetSprintById(int id)
        {
            return db.GetItem<Sprint>(id);
        }

        public void SaveSprint(Sprint sprint)
        {
            db.SaveItem(sprint);
        }

        public void DeleteSprint(Sprint sprint)
        {
            var existItem = GetSprintById(sprint.Id);
            if (existItem != null)
            {
                db.DeleteItem(sprint);
            }
        }
    }
}
