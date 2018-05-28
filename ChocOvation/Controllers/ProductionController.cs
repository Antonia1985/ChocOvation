using ChocOvation.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChocOvation.Controllers
{
    [Authorize(Roles = "Production Manager, CEO, Admin")]
    public class ProductionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IdentityDbContext idb = new IdentityDbContext();

        // GET: Production
        public async Task<ActionResult> Index()
        {
            var datesaved = db.Productions.Any();
            DateTime lastDateProduction = new DateTime();
            if (datesaved == false)
            {
                lastDateProduction = new DateTime(2018, 05, 14);
            }
            else
            {
                lastDateProduction = db.Productions.OrderByDescending(d => d.DayOfProduction).Select(d => d.DayOfProduction).First();

            }

            var todayProduction = DateTime.Today;

            if (todayProduction != lastDateProduction)
            {
                for (var day = lastDateProduction; day < todayProduction; day.AddDays(1))
                {
                    day = day.AddDays(1);

                    var production = new Production
                    {

                        DayOfProduction = day,
                        ItemsProducedPerDay = 500,
                        FixedCosts = 1000,
                        Manager = db.Departments.Single(d => d.DepartmentName == "Production").Manager
                    };
                    db.Productions.Add(production);
                    await db.SaveChangesAsync();

                    var chocoIdBlack = db.Chocos.Where(c => c.ChocoName == "Dark Pleasure").Select(c => c.ChocoID).Single();
                    var chocoIdWhite = db.Chocos.Where(c => c.ChocoName == "Pure White").Select(c => c.ChocoID).Single();
                    var chocoIdMilk = db.Chocos.Where(c => c.ChocoName == "Milky Dream").Select(c => c.ChocoID).Single();
                    var chocoIdAlmond = db.Chocos.Where(c => c.ChocoName == "Amore Amaretto").Select(c => c.ChocoID).Single();
                    var chocoIdHazelnut = db.Chocos.Where(c => c.ChocoName == "Hot Hazelnut").Select(c => c.ChocoID).Single();
                    var destinationWareHouse = db.Departments.Where(d => d.DepartmentName == "Warehouse").Select(d => d.DepartmentID).Single();
                    var destinationStore = db.Departments.Where(d => d.DepartmentName == "Sales").Select(d => d.DepartmentID).Single();

                    var count = 0;
                    string bc = "";
                    for (int prd = 0; prd < production.ItemsProducedPerDay; prd++)
                    {
                        count = count + 1;
                        if (count < 10)
                        {
                            bc = day.ToString("yyyyMMdd") + "00" + count.ToString();
                        }
                        else if (count < 100)
                        {
                            bc = day.ToString("yyyyMMdd") + "0" + count.ToString();
                        }
                        else
                        {
                            bc = day.ToString("yyyyMMdd") + count.ToString();

                        }

                        var product = new Product();

                        product.BarCode = bc;
                        product.DayOfProduction = day;
                        product.WeightPerItem = 100;
                        product.PricePerItem = 3;
                        product.ProductionID = db.Productions.Where(p => p.DayOfProduction == product.DayOfProduction).Select(p => p.ProductionID).Single();

                        if ((count >= 1) && (count <= 100))
                        {
                            product.ChocoID = chocoIdBlack;
                            product.DepartmentID = destinationWareHouse;
                            db.Products.Add(product);
                            await db.SaveChangesAsync();

                        }
                        else if ((count >= 101) && (count <= 200))
                        {
                            product.ChocoID = chocoIdBlack;
                            product.DepartmentID = destinationStore;
                            db.Products.Add(product);
                            await db.SaveChangesAsync();

                        }
                        else if ((count >= 201) && (count <= 250))
                        {
                            product.ChocoID = chocoIdWhite;
                            product.DepartmentID = destinationWareHouse;
                            db.Products.Add(product);
                            await db.SaveChangesAsync();

                        }
                        else if ((count >= 251) && (count <= 300))
                        {
                            product.ChocoID = chocoIdWhite;
                            product.DepartmentID = destinationStore;
                            db.Products.Add(product);
                            await db.SaveChangesAsync();
                            ;
                        }
                        else if ((count >= 301) && (count <= 350))
                        {
                            product.ChocoID = chocoIdMilk;
                            product.DepartmentID = destinationWareHouse;
                            db.Products.Add(product);
                            await db.SaveChangesAsync();

                        }
                        else if ((count >= 351) && (count <= 400))
                        {
                            product.ChocoID = chocoIdMilk;
                            product.DepartmentID = destinationStore;
                            db.Products.Add(product);
                            await db.SaveChangesAsync();

                        }
                        else if ((count >= 401) && (count <= 425))
                        {
                            product.ChocoID = chocoIdAlmond;
                            product.DepartmentID = destinationWareHouse;
                            db.Products.Add(product);
                            await db.SaveChangesAsync();

                        }
                        else if ((count >= 426) && (count <= 450))
                        {
                            product.ChocoID = chocoIdAlmond;
                            product.DepartmentID = destinationStore;
                            db.Products.Add(product);
                            await db.SaveChangesAsync();

                        }
                        else if ((count >= 451) && (count <= 475))
                        {
                            product.ChocoID = chocoIdHazelnut;
                            product.DepartmentID = destinationWareHouse;
                            db.Products.Add(product);
                            await db.SaveChangesAsync();

                        }
                        else if ((count >= 476) && (count <= 500))
                        {
                            product.ChocoID = chocoIdHazelnut;
                            product.DepartmentID = destinationStore;
                            db.Products.Add(product);
                            await db.SaveChangesAsync();

                        }

                    }
                }
            }
            var productions = db.Productions.Include(p => p.Manager);
            return View(await productions.ToListAsync());
        }


        // GET: Production/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Production production = await db.Productions.FindAsync(id);
            if (production == null)
            {
                return HttpNotFound();
            }
            return View(production);
        }

        // GET: Production/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Users, "Id", "VATNumber");
            return View();
        }

        // POST: Production/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductionId,DayOfProduction,ItemsProducedPerDay,FixedCosts,Id")] Production production)
        {
            if (ModelState.IsValid)
            {
                db.Productions.Add(production);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Users, "Id", "Fullname", production.ManagerId);
            return View(production);
        }

        // GET: Production/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Production production = await db.Productions.FindAsync(id);
            if (production == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "FullName", production.ManagerId);
            return View(production);
        }

        // POST: Production/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductionId,DayOfProduction,ItemsProducedPerDay,FixedCosts,Id")] Production production)
        {
            if (ModelState.IsValid)
            {
                db.Entry(production).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "FulName", production.ManagerId);
            return View(production);
        }

        // GET: Production/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Production production = await db.Productions.FindAsync(id);
            if (production == null)
            {
                return HttpNotFound();
            }
            return View(production);
        }

        // POST: Production/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Production production = await db.Productions.FindAsync(id);
            db.Productions.Remove(production);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
