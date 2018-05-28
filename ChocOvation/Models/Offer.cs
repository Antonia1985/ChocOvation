using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocOvation.Models
{
    public class Offer
    {
        [Display(Name = "Offer ID")]
        public int OfferID { get; set; }

        [ForeignKey("SupplierID")]
        public Supplier Supplier { get; set; }
        public string SupplierID { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Offer's Date")]
        [Column(TypeName = "Date")]
        public DateTime DateOfOffer { get; set; }

        public IEnumerable<OfferPerMaterial> OffersPerMaterial { get; set; }

        public float? TotalPriceQuality { get; set; }
    }
}