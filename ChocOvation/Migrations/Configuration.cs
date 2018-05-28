namespace ChocOvation.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ChocOvation.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}

            ContextKey = "ChocOvation.Models.ApplicationDbContext";
            AutomaticMigrationsEnabled = false;
        }

        //protected override void Seed(ChocOvation.DAL.ChocoContext context)
        //{
        //    var Employees = new List<Employee>
        //    {
        //        new Employee {VATNumber = "000000001", FirstName = "Carson",   LastName = "Alexander", Address = "L.katsoni 50, Aghia Varvara ", Telephone = "6907970000",
        //            HireDate = DateTime.Parse("2010-09-01"), Salary = 5400, },
        //        new Employee {VATNumber = "000000021", FirstName = "Meredith", LastName = "Alonso", Address = "L.katsoni 85, Aghia Varvara ", Telephone = "6900222000",
        //            HireDate = DateTime.Parse("2010-09-01"), Salary = 5040/*,  DepartmentID  = 2*/ },
        //        new Employee { VATNumber = "000002222",FirstName = "Arturo",   LastName = "Anand",Address = "L.katsoni 26, Aghia Varvara ", Telephone = "6900067600",
        //            HireDate = DateTime.Parse("2010-09-01"), Salary = 5800/*,  DepartmentID  = 3 */},
        //        new Employee { VATNumber = "000009999",FirstName = "Gytis",    LastName = "Barzdukas",Address = "L.katsoni 14, Aghia Varvara ", Telephone = "6900012300",
        //            HireDate = DateTime.Parse("2010-09-01"), Salary = 5090/*,  DepartmentID  = 2*/ },
        //        new Employee {VATNumber = "789400000", FirstName = "Yan",      LastName = "Li",Address = "L.katsoni 5, Aghia Varvara ", Telephone = "6900000963",
        //            HireDate = DateTime.Parse("2010-09-01"), Salary = 6000/*,  DepartmentID  = 1 */},
        //        new Employee {VATNumber = "000004567", FirstName = "Peggy",    LastName = "Justice",Address = "L.katsoni 78, Aghia Varvara ", Telephone = "6900565000",
        //            HireDate = DateTime.Parse("2010-09-01"), Salary = 8000/*,  DepartmentID  = 2*/ },
        //        new Employee {VATNumber = "000656500", FirstName = "Laura",    LastName = "Norman",Address = "L.katsoni 150, Aghia Varvara ", Telephone = "6900004646",
        //            HireDate = DateTime.Parse("2010-09-01"), Salary = 12000/*,  DepartmentID  = 3*/ },
        //        new Employee {VATNumber = "000394000", FirstName = "Nino",     LastName = "Olivetto",Address = "L.katsoni 250, Aghia Varvara ", Telephone = "6901100000",
        //            HireDate = DateTime.Parse("2010-09-01"), Salary = 5300/*,  DepartmentID  = 4 */}
        //    };

        //    Employees.ForEach(l => context.Employees.AddOrUpdate(d => d.LastName, l));
        //    context.SaveChanges();

        //    var departments = new List<Department>
        //    {
        //        new Department {DepartmentName = "Finance", VATNumber  = Employees.Single( i => i.LastName == "Li").VATNumber, Employees = new List<Employee>() },
        //        new Department {DepartmentName = "Supply", VATNumber  = Employees.Single( i => i.LastName == "Barzdukas").VATNumber, Employees = new List<Employee>() },
        //        new Department {DepartmentName = "Purchasing", VATNumber  = Employees.Single(i => i.LastName == "Norman").VATNumber, Employees = new List<Employee>() },
        //        new Department {DepartmentName = "Sales",   VATNumber  = Employees.Single( i => i.LastName == "Olivetto").VATNumber , Employees = new List<Employee>() }
        //    };
        //    departments.ForEach(l => context.Departments.AddOrUpdate(d => d.DepartmentName, l));
        //    context.SaveChanges();

        //    var suppliers = new List<Supplier>
        //    {
        //        new Supplier { VATNumber = "300394066", FirstName = "EPE",     LastName = "Fine Products",Address = "Hrakelitou 250, Egaleo ", Telephone = "6901100000", Profession = "Row Products Supplier" },
        //        new Supplier { VATNumber = "240124008", FirstName = "AE",     LastName = "Pastry Suppliers",Address = " L.Kifisias 66, Kifisia ", Telephone = "6901100000", Profession = "Row Products Supplier" },
        //        new Supplier { VATNumber = "111397070", FirstName = "AE",     LastName = "Caramel Creations",Address = "ovkratous 5, Athens ", Telephone = "6901100000", Profession = "Row Products Supplier" },
        //    };

        //    //void AddOrUpdateEmployee( ChocoContext con, string departmentName, string employeeName)
        //    //{
        //    //    var dep = con.Departments.SingleOrDefault(c => c.DepartmentName == departmentName);
        //    //    var emp = con.Employees.SingleOrDefault(i => i.LastName == employeeName);
        //    //    if (emp == null)
        //    //        crs.Instructors.Add(con.Instructors.Single(i => i.LastName == instructorName));
        //    //}
        //    //  This method will be called after migrating to the latest version.

        //    //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //    //  to avoid creating duplicate seed data.
        //}
    }

}
