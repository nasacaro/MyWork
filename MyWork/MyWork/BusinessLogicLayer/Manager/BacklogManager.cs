using MyWork.DAL.DBHelper;
using MyWork.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.BusinessLogicLayer.Manager
{
    public class BacklogManager
    {
        private static BacklogManager me;
        public static BacklogManager GetInstance()
        {
            if (me == null)
            {
                me = new BacklogManager();
            }

            return me;
        }

        public void SaveBacklog(Backlog backlog)
        {
            BacklogHelper.GetInstance().SaveBacklog(backlog);
        }

        public List<Backlog> GetAllBacklog()
        {
            return BacklogHelper.GetInstance().GetAllBacklog();
        }

        public List<Backlog> GetAllBacklogByUser(int userId)
        {
            return BacklogHelper.GetInstance().GetAllBacklogByUser(userId);
        }

        public void DeleteBacklog(Backlog backlog)
        {
            BacklogHelper.GetInstance().DeleteBacklog(backlog);
        }

        public List<Backlog> GetAllFutureBacklog(int userId, int sprintNum)
        {
            var allBacklogs = GetAllBacklogByUser(userId);
            if(allBacklogs != null && allBacklogs.Count > 0)
            {
                return allBacklogs.Where(x => x.Status == 0 && x.Sprint == sprintNum).ToList();
            }
            return new List<Backlog>();
        }

        public List<Backlog> GetAllInProgressBacklog(int userId, int sprintNum)
        {
            var allBacklogs = GetAllBacklogByUser(userId);
            if (allBacklogs != null && allBacklogs.Count > 0)
            {
                return allBacklogs.Where(x => x.Status == 1 && x.Sprint == sprintNum).ToList();
            }
            return new List<Backlog>();
        }

        public List<Backlog> GetAllDoneBacklog(int userId, int sprintNum)
        {
            var allBacklogs = GetAllBacklogByUser(userId);
            if (allBacklogs != null && allBacklogs.Count > 0)
            {
                return allBacklogs.Where(x => x.Status == 2 && x.Sprint == sprintNum).ToList();
            }
            return new List<Backlog>();
        }

        public List<Backlog> GetAllAcceptBacklog(int userId, int sprintNum)
        {
            var allBacklogs = GetAllBacklogByUser(userId);
            if (allBacklogs != null && allBacklogs.Count > 0)
            {
                return allBacklogs.Where(x => x.Status == 3 && x.Sprint == sprintNum).ToList();
            }
            return new List<Backlog>();
        }
    }
}
