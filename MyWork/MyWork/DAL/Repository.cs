using MyWork.Models.DBModels;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.DAL
{
    public class Repository
    {
        readonly SQLiteConnection database;

        private static string dbPath = string.Empty;
        private static string DbPath
        {
            get
            {
                return dbPath;
            }
            set
            {
                dbPath = value;
            }
        }

        public Repository(string dbPath)
        {
            DbPath = dbPath;
            using (database = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), dbPath))
            {
                database.CreateTable<UserAccount>();
                database.CreateTable<AppConfiguration>();
            }
            //database.CreateTablesAsync<UserAccount, UserInfo>().Wait();
        }

        private static Repository _me;
        public static Repository GetInstance()
        {
            if (_me == null)
            {
                ISQLite sqlite = new Sqlite();
                _me = new Repository(sqlite.GetLocalFilePath("MyWork.db3"));
            }
            return _me;
        }

        public List<T> GetItems<T>() where T : Entity, new()
        {
            using (var db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DbPath))
            {
                var items = (from p in db.Table<T>()
                             select p).ToList();
                return items;
            }           
        }

        public T GetItem<T>(int id) where T : Entity, new()
        {
            using (var db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DbPath))
            {
                //SQLiteCommand cmd = db.CreateCommand();
                return db.Table<T>().Where(i => i.Id == id).ToList().FirstOrDefault();
            }            
        }

        public T GetItem<T>(string query) where T : Entity, new()
        {
            using (var db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DbPath))
            {
                SQLiteCommand cmd = db.CreateCommand(query);
                return cmd.ExecuteQuery<T>().FirstOrDefault();
            }
        }

        public int SaveItem<T>(T item) where T : Entity, new()
        {
            using (var db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DbPath))
            {
                if (item.Id != 0)
                {
                    return db.Update(item);
                }
                else
                {
                    return db.Insert(item);
                }
            }            
        }

        public int DeleteItem<T>(T item) where T : Entity, new()
        {
            using (var db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DbPath))
            {
                return db.Delete(item);
            }            
        }
    }
}
