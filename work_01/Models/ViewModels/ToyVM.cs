using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace work_01.Models.ViewModels
{
    public class ToyVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Toy name is required!!"), StringLength(50), Display(Name = "Name")]
        public string ToyName { get; set; }

        [Required, Column(TypeName = "money"), Display(Name = "Price"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Required, Column(TypeName = "date"), Display(Name = "Store Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StoreDate { get; set; }

        [Display(Name = "Picture")]
        public string PicturePath { get; set; }

        //fk
        [Display(Name = "Category")]
        [ForeignKey("Category")]
        public int CId { get; set; }

        public string CName { get; set; }

        public HttpPostedFileBase Picture { get; set; }

        //public static implicit operator ToyVM(ToyVM v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}