using PagedList;
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
     public class UserDao
    {
        private NguyenVuongContext db;
        public UserDao()
        {
            db = new NguyenVuongContext();
        }

        public int Login(String user, String pass)
        {
            var result = db.UserAccounts.SingleOrDefault(x => x.UserName.Contains(user) && x.Password.Contains(pass));
            if (result == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        ///////////////////
        public List<UserAccount> ListAll()
        {
            return db.UserAccounts.ToList();
        }
        //////////////////
        public int Insert(UserAccount entitiUser)
        {
            db.UserAccounts.Add(entitiUser);
            db.SaveChanges();
            return entitiUser.ID;
        }

        /////////////
        public UserAccount Find(string userName)
        {
            return db.UserAccounts.Find(userName);
        }
        ///////=============
        public UserAccount FindId(int id)
        {
            return db.UserAccounts.Find(id);
        }

        ///////////Tim kiem
        public IEnumerable<UserAccount> ListWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<UserAccount> model = db.UserAccounts;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.UserName.Contains(keysearch));
            }
            return model.OrderBy(x => x.UserName).ToPagedList(page, pagesize);
        }
        ///////////////////
        public bool Delete(int id)
        {
            try
            {
                var user = db.UserAccounts.Find(id);
                db.UserAccounts.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
