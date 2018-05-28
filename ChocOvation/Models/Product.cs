using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocOvation.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        public string BarCode { get; set; }

        [Display(Name = "Day Of Production")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "Date")]
        public DateTime DayOfProduction { get; set; }

        [Display(Name = "Weight")]
        public int WeightPerItem { get; set; }

        [Display(Name = "Price to Sell")]
        public int PricePerItem { get; set; }

        public bool IsSold { get; set; }

        public int ChocoID { get; set; }

        [ForeignKey("ChocoID")]
        public Choco ChocoCategory { get; set; }

        [Display(Name = "Destination ID")]
        public int DepartmentID { get; set; }
        public Department DestinationDepartment { get; set; }

        public int ProductionID { get; set; }
        public Production Production { get; set; }



    }
}