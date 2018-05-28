using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocOvation.Models
{
    public class OrderPerMaterial
    {
        public int OrderPerMaterialID { get; set; }

        //[Key, Column(Order = 0)]
        public int OrderID { get; set; }
        public Order Order { get; set; }

        [Column(TypeName = "Money")]
        public int PricePerMaterial { get; set; }

        //[Key, Column(Order = 2)]
        public int MaterialID { get; set; }
        public Material Material { get; set; }



        [Required]
        public int QuantityPerYear { get; set; }




    }
}