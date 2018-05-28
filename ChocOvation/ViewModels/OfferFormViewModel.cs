using ChocOvation.Models;
using System.ComponentModel.DataAnnotations;

namespace ChocOvation.ViewModels
{
    public class OfferFormViewModel
    {
        [Required]
        public string MaterialName { get; set; }

        [Required]
        public Quality Quality { get; set; }

        [Required]
        [Range(typeof(int), "1", "1000")]
        public int PricePerKg { get; set; }

        [Display(Name = "Price & Quality")]
        public float PriceANDQuality
        {
            //int x = (int)Quality;
            //int y = PricePerKg;
            //return x / y;

            get
            {
                return (float)Quality / (float)PricePerKg;
            }
        }


    }
}