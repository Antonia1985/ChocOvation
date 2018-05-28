using ChocOvation.Models;
using System;
using System.Collections.Generic;

namespace ChocOvation.ViewModels
{
    public class StoreViewModel
    {
        public int ChocoStoreID { get; set; }
        public DateTime Date { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public int DepartmentID { get; set; }
        public int NumberofProductsSoldToday { get; set; }
        public int TodaysStock { get; set; }
        public int TodaysProfit { get; set; }


        public string ManagerId { get; set; }
        public virtual Employee Manager { get; set; }

        public IEnumerable<Product> ProductsSoldToday { get; set; }
        public IEnumerable<Employee> StoreEmployees { get; set; }
    }
}