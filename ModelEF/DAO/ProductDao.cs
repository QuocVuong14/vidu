using PagedList;
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ModelEF.Model1;

namespace ModelEF.DAO
{
    public class ProductDao
    {
        private NguyenVuongContext db;
        public ProductDao()
        {
            db = new NguyenVuongContext();
        }
        /////////////////
        ///// chuyển về dạng list và tăng dần theo giá
        public List<Product> ListAll()
        {
            //return db.Products.OrderBy(x => x.Quantity).ToList();
            return db.Products.OrderBy(x => x.UnitCost).ToList();
        }
        ///////////////////tìm theo id
        public Product FindId(int id)
        {
            return db.Products.Find(id);
        }
        ////////////////
        [HttpGet]
        public ProductCategoryModel ShowDetail(int idProduct)
        {

            var model = from a in db.Categories.AsEnumerable() //trả về dưới dạng iemunerable 
                        join b in db.Products.AsEnumerable()
                        on a.ID equals b.Category_Id into dgroup

                        from b in dgroup.DefaultIfEmpty()
                        where b.ID == idProduct
                        select new ProductCategoryModel()
                        {
                            ID = b.ID,
                            Name = b.Name,
                            UnitCost = (int)b.UnitCost,
                            Quantity =(int)b.Quantity,
                            Image = b.Image,
                            Description = b.Description,
                      
                            Status =  b.Status,
                            Category_Id =(int) b.Category_Id,
                            CategoryName = a.Name,
               
                        };
            var model1 = model.FirstOrDefault();
            return model1;

        }




        ////////////////////
        ///thêm vào bảng product
        public int Insert(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return product.ID;
        }
        ///////////////////
        ///xóa
        public bool Delete(int id)
        {
            try
            {
                
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }

            catch (Exception ex)

            {
                return false;
            }
        }

        ///////////Tim kiem
        public IEnumerable<Product> ListWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(keysearch))//nếu khác null hoặc rỗng
            {
                model = model.Where(x => x.Name.Contains(keysearch)); // tìm kiếm từ khóa có trong name ko
            }
            return model.OrderBy(x => x.UnitCost).ToPagedList(page, pagesize);// sắp xếp theo giá tăng dần
        }
    }
}
