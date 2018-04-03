using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Profile;
using CafeGrove.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CafeGrove.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

    public class Product
    {
        public Product()
        {
            this.Orders = new HashSet<Order>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required] public string Name { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required] public decimal Price { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }

    public class Order
    {
        public Order()
        {
            //this.Products= new HashSet<Product>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string TableNo { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }

        //public virtual ICollection<Product> Products { get; set; }
    }
}