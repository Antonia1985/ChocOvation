using ChocOvation.Models;
using System.Collections.Generic;

namespace ChocOvation.ViewModels
{
    public class SupplierOfferViewModel
    {
        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<Offer> Offers { get; set; }
    }
}