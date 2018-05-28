using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocOvation.Models
{
    public class ChocoStore
    {
        public int ChocoStoreID { get; set; }

        [Display(Name = "Day of Sales")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        public string StoreName { get; set; }
        public string Address { get; set; }

        public int DepartmentID { get; set; }
        public Department Department { get; set; }

        public int NumberofProductsSoldToday { get; set; }
        public int TodaysStock { get; set; }
        public int TodaysProfit { get; set; }

        [Required]
        [Display(Name = "Manager's ID")]
        [Column("Manager's ID")]
        [ForeignKey("Manager")]
        public string ManagerId { get; set; }

        public virtual Employee Manager { get; set; }

        public IEnumerable<Product> ProductsSoldToday { get; set; }
        public IEnumerable<Employee> StoreEmployees { get; set; }


        //public int NumberofProductsSoldToday()
        //{

        //    var numberOfItems = ProductsSoldToday.Count();
        //    return numberOfItems;
        //}

        //public int TodaysStock(int newReceipt, int lastStock)
        //{
        //    var remaining = (newReceipt + lastStock) - NumberofProductsSoldToday();
        //    return remaining;
        //}

        //public int TodaysProfit(IEnumerable<Product> ProductsSold)
        //{
        //    var totalPrice = 0;
        //    foreach (var prdct in ProductsSold)
        //    {
        //        var price = prdct.PricePerItem;
        //        totalPrice = totalPrice + price;
        //    }
        //    return totalPrice;
        //}

    }
}