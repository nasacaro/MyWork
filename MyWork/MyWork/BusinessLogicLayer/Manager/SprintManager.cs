using MyWork.DAL.DBHelper;
using MyWork.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.BusinessLogicLayer.Manager
{
    public class SprintManager
    {
        protected static SprintManager me;
        public static SprintManager GetInstance()
        {
            if (me == null)
            {
                me = new SprintManager();
            }

            return me;
        }

        public void SaveSprint(Sprint sprint)
        {
            SprintHelper.GetInstance().SaveSprint(sprint);
        }

        public List<Sprint> GetAllActiveSprint()
        {
            return SprintHelper.GetInstance().GetAllActiveSprint();
        }

        public void DeleteSprint(Sprint sprint)
        {
            SprintHelper.GetInstance().DeleteSprint(sprint);
        }
    }
}
