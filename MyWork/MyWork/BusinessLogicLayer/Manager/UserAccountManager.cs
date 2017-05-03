using MyWork.BusinessLogicLayer.Utilities;
using MyWork.DAL.DBHelper;
using MyWork.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.BusinessLogicLayer.Manager
{
    public class UserAccountManager
    {
        protected static UserAccountManager me;
        public static UserAccountManager GetInstance()
        {
            if (me == null)
            {
                me = new UserAccountManager();
            }

            return me;
        }

        public void InsertNewUserAccount(UserAccount userAccount)
        {
            UserAccountHelper.GetInstance().SaveUserAccount(userAccount);
        }

        public void UpdateUserAccount(UserAccount userAccount)
        {
            UserAccountHelper.GetInstance().SaveUserAccount(userAccount);
        }

        public void DeleteUserAccount(UserAccount userAccount)
        {
            UserAccountHelper.GetInstance().DeleteUserAccount(userAccount);
        }

        public List<UserAccount> GetAllUserAccount()
        {
            return UserAccountHelper.GetInstance().GetAllUserAccount();
        }

        public UserAccount GetUserAccountById(int id)
        {
            return UserAccountHelper.GetInstance().GetUserAccountById(id);
        }

        public UserAccount DoLogIn(string userName, string password)
        {
            return UserAccountHelper.GetInstance().DoLogIn(userName, Utils.GenerateMD5String(password));
        }

        public bool CheckAdminAccount()
        {
            return UserAccountHelper.GetInstance().CheckAdminAccount();
        }

        public bool CheckUserExists(string userName)
        {
            return UserAccountHelper.GetInstance().CheckUserExists(userName);
        }

        public int GetUserId(string userName, string password)
        {
            return UserAccountHelper.GetInstance().GetUserId(userName, Utils.GenerateMD5String(password));
        }
    }
}
