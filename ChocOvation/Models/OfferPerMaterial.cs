using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocOvation.Models
{
    public class OfferPerMaterial
    {
        // public int OfferPerMaterialID { get; set; }

        [Key, Column(Order = 0)]
        public int OfferID { get; set; }
        public Offer Offer { get; set; }

        [Key, Column(Order = 1)]
        public int MaterialID { get; set; }
        public Material Material { get; set; }

        [Range(typeof(int), "1", "1000")]
        public int PricePerKg { get; set; }

        [Display(Name = "Price & Quality")]
        public float? PriceQuality { get; set; }


        public IEnumerable<Material> Materials { get; set; }
    }
}