namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int? UnitCost { get; set; }

        public int? Quantity { get; set; }

        [StringLength(300)]
        public string Image { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public int? Category_Id { get; set; }
     

        public virtual Category Category { get; set; }
    }
}
