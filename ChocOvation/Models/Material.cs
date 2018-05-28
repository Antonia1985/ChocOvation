using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChocOvation.Models
{
    public enum Quality
    {
        High = 3,
        Medium = 2,
        Low = 1
    }
    public class Material
    {
        public int MaterialID { get; set; }

        [Display(Name = "Material Name")]
        public string MaterialName { get; set; }


        public Quality Quality { get; set; }

        public IEnumerable<OfferPerMaterial> OffersPerMaterial { get; set; }
        public IEnumerable<Choco> ChocosToBeUsed { get; set; }
        
    }
}