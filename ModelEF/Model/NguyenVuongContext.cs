using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ModelEF.Model
{
    public partial class NguyenVuongContext : DbContext
    {
        public NguyenVuongContext()
            : base("name=NguyenVuongContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.Category_Id);

            modelBuilder.Entity<Product>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<UserAccount>()
                .Property(e => e.UserName)
                .IsFixedLength();

            modelBuilder.Entity<UserAccount>()
                .Property(e => e.Password)
                .IsFixedLength();
        }

        public System.Data.Entity.DbSet<ModelEF.Model1.ProductCategoryModel> ProductCategoryModels { get; set; }
    }
}
