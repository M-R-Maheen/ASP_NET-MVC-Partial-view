using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace work_01.Models
{
    public class Category
    {
        public Category()
        {
            this.Toy = new List<Toy>();
        }
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CId { get; set; }
        [Required,StringLength(50),Display(Name ="Category")]
        public string CName { get; set; }
        //nev
        public virtual ICollection<Toy> Toy { get; set; }

    }
    public class Toy
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Toy name is required!!"),StringLength(50),Display(Name ="Name")]
        public string ToyName { get; set; }
        [Required,Column(TypeName ="money"),Display(Name ="Price"),DisplayFormat(DataFormatString ="{0:0.00}",ApplyFormatInEditMode =true)]
        public decimal Price { get; set; }
        [Required,Column(TypeName ="date"),Display(Name = "Store Date"),DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime StoreDate { get; set; }
        [Display(Name ="Picture")]
        public string PicturePath { get; set; }
        //fk
        [Display(Name = "Category")]
        [ForeignKey("Category")]
        public int CId { get; set; }
        //nev
        public virtual Category Category { get; set; }
    }
    public class ToyDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Toy> Toys { get; set; }
    }

}