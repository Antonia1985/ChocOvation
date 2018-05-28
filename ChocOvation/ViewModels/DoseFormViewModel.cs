using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocOvation.ViewModels
{
    public class DoseFormViewModel
    {
        public string ChocoName { get; set; }
        //public Choco Choco { get; set; }

        public string MaterialName { get; set; }

        [Display(Name = "Material Dose Per 100gr Of Choco")]
        [Column("QuantityPer100gr")]
        public int QuantityPer100gr { get; set; }

        //public int MaterialID { get; set; }
        //public Material Material { get; set; }
    }
}