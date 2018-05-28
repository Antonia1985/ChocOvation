using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocOvation.Models
{
    public class SoldProduct
    {
        public int SoldProductID { get; set; }

        [Display(Name = "Sold At")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "Date")]
        public DateTime DateSold { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }

        //public int ChocoID { get; set; }
        //public Choco Choco { get; set; }
    }
}