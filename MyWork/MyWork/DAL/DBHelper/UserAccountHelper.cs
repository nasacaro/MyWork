using MyWork.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.DAL.DBHelper
{
    public class UserAccountHelper
    {
        protected static UserAccountHelper me;
        private readonly Repository db;

        public UserAccountHelper()
        {
            db = Repository.GetInstance();
        }

        public static UserAccountHelper GetInstance()
        {
            if (me == null)
            {
                me = new UserAccountHelper();
            }

            return me;
        }

        public UserAccount GetUserAccount()
        {
            return GetAllUserAccount().FirstOrDefault();
        }

        public UserAccount GetUserAccountById(int id)
        {
            return db.GetItem<UserAccount>(id);
        }

        public bool CheckUserExists(string userName)
        {
            string query = string.Format("select * from UserAccount where UserName = '{0}'", userName);
            var user = db.GetItem<UserAccount>(query);
            return user != null;
        }

        public List<UserAccount> GetAllUserAccount()
        {
            //string query = "select * from UserAccount";
            return db.GetItems<UserAccount>();
        }

        public void SaveUserAccount(UserAccount userAccount)
        {
            db.SaveItem(userAccount);
        }

        public void DeleteUserAccount(UserAccount userAccount)
        {
            var existItem = GetUserAccountById(userAccount.Id);
            if (existItem != null)
            {
                db.DeleteItem(userAccount);
            }
        }

        public UserAccount DoLogIn(string userName, string password)
        {
            string query = string.Format("select * from UserAccount where UserName = '{0}' and Password = '{1}'", userName, password);
            return db.GetItem<UserAccount>(query);
        }

        public bool CheckAdminAccount()
        {
            string query = "select * from UserAccount where IsAdmin";
            var user = db.GetItem<UserAccount>(query);
            return user != null;
        }

        public int GetUserId(string userName, string password)
        {
            string query = string.Format("select * from UserAccount where UserName = '{0}' and Password = '{1}'", userName, password);
            var user = db.GetItem<UserAccount>(query);
            return user != null ? user.Id : 0;
        }
    }
}
