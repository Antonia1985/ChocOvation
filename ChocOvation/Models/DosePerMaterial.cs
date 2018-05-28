using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocOvation.Models
{
    public class DosePerMaterial
    {
        public int DosePerMaterialID { get; set; }
        
        public int ChocoID { get; set; }
        
        public int MaterialID { get; set; }

        [Display(Name = "Material Dose Per 100gr Of Choco")]
        [Column("QuantityPer100gr")]
        public int QuantityPer100gr { get; set; }

        public Choco Choco { get; set; }
        public Material Material { get; set; }



    }
}