using MyWork.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.DAL.DBHelper
{
    public class BacklogHelper
    {
        protected static BacklogHelper me;
        private readonly Repository db;

        public BacklogHelper()
        {
            db = Repository.GetInstance();
        }

        public static BacklogHelper GetInstance()
        {
            if (me == null)
            {
                me = new BacklogHelper();
            }

            return me;
        }

        public List<Backlog> GetAllBacklog()
        {
            //string query = "select * from UserAccount";
            return db.GetItems<Backlog>();
        }

        public List<Backlog> GetAllBacklogByUser(int userId)
        {
            string query = string.Format("select * from Backlog where UserId = {0}", userId);
            return db.GetItems<Backlog>(query);
        }

        public Backlog GetBacklogById(int id)
        {
            return db.GetItem<Backlog>(id);
        }

        public void SaveBacklog(Backlog backlog)
        {
            db.SaveItem(backlog);
        }

        public void DeleteBacklog(Backlog backlog)
        {
            var existItem = GetBacklogById(backlog.Id);
            if (existItem != null)
            {
                db.DeleteItem(backlog);
            }
        }
    }
}
