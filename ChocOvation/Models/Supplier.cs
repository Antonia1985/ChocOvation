using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ChocOvation.Models
{
    public class Supplier : ApplicationUser
    {
        [Required]
        public string Profession { get; set; }


        //public int? OfferID { get; set; }

        public ICollection<Offer> Offers { get; set; }
    }
}