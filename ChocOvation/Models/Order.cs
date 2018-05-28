using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocOvation.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        [Column(TypeName="Money")]
        public int Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Order's Date")]
        [Column(TypeName = "Date")]
        public DateTime DateOfOrder { get; set; }

        public int OfferID { get; set; }
        public Offer ChosenOffer { get; set; }

        public IEnumerable<OrderPerMaterial> OrdersPermaterial { get; set; }
        //public IEnumerable<OfferPerMaterial> OffersPermaterial { get; set; }

    }
}