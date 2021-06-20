using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.Model1
{
    public class ProductCategoryModel
    {
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }
        public int UnitCost { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Category_Id { get; set; }
        public string CategoryName { get; set; }
        
    }
}
