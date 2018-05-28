using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocOvation.Models
{
    public class Warehouse
    {
        //public int FactoryID { get; set; }
        //public int DepartmentID { get; set; }
        public int QuantityOfStoredProducts { get; set; }

        [Required]
        [Display(Name = "Manager's ID")]
        [Column("Manager's ID")]
        public string Id { get; set; }


        [ForeignKey("Id")]
        public virtual Employee Manager { get; set; }


        public IEnumerable<Employee> WarehouseEmployees { get; set; }
        public IEnumerable<Product> StoredProducts { get; set; }

    }
}