using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class CategoryDao
    {
        private NguyenVuongContext db;
        public CategoryDao()
        {
            db = new NguyenVuongContext();
        }
        ///////////////////
        public List<Category> ListAll()
        {
            return db.Categories.ToList();
        }

        ///////////Tim kiem
        public IEnumerable<Category> ListWhereAll(string keysearchCategory)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(keysearchCategory))
            {
                model = model.Where(x => x.Name.Contains(keysearchCategory));
            }
            return model.OrderBy(x => x.Name);
        }

        ///////////////////
        ///kiểm tra id
        public Category FindId(int id)
        {
            return db.Categories.Find(id);
        }

        /////////////////////
        public int Insert(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }

        ///////////////////
        public bool Delete(int id)
        {
            try
            {
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
