using System.Collections.Generic;
using System.Web.Mvc;

namespace ChocOvation.Models
{
    //[Authorize(Roles = "CEO, Admin")]
    public class Choco
    {
        public int ChocoID { get; set; }
        public string ChocoName { get; set; }

        //[Required]
        //public int FactoryID { get; set; }
        //public Factory factory { get; set; }

        //public IEnumerable<Material> Materials { get; set; }
        public IEnumerable<DosePerMaterial> Recipe { get; set; }
        public IEnumerable<Product> ProducedProducts { get; set; }

    }
}