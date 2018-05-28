using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocOvation.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        //[Required]
        //public int FactoryID { get; set; }
        //public Factory Factory { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        //[Required]
        [Display(Name = "Manager's ID")]
        [Column("Manager's ID")]
        public string Id { get; set; }


        [ForeignKey("Id")]
        public virtual Employee Manager { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

    }
}