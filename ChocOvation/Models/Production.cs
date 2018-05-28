using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocOvation.Models
{
    public class Production
    {
        
        public int ProductionID { get; set; }
        
        [Display(Name = "Day of production")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "Date")]
        public DateTime DayOfProduction { get; set; }

        [Display(Name = "Amount Of Product")]
        public int ItemsProducedPerDay { get; set; }

        public int FixedCosts { get; set; }
        
        [Required]
        [Display(Name = "Manager's ID")]
        [Column("Manager's ID")]
        [ForeignKey("Manager")]
        public string ManagerId { get; set; }

        //[ForeignKey("Id")]
        public virtual Employee Manager { get; set; }

        public IEnumerable<Choco> KindsOfProducedChocos { get; set; }
        public IEnumerable<Employee> ProductionEmployees { get; set; }
        public IEnumerable<Product> ChocoItems { get; set; }
    }
}