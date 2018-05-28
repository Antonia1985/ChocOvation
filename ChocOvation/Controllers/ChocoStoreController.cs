using ChocOvation.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChocOvation.Controllers
{
    [Authorize(Roles = "ChocoStore Manager, CEO, Admin")]
    public class ChocoStoreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult ProductSoldToday()
        {

            var soldToday = db.SoldProducts.Where(p => p.DateSold == DateTime.Today).Include(p => p.Product).Include(c => c.Product.ChocoCategory).Select(p => p);


            return View(soldToday);
        }

        [Authorize]
        public async Task<ActionResult> ProductsToBeSold()
        {

            var available = db.Products.Include(p => p.ChocoCategory).Where(p => p.DestinationDepartment.DepartmentName == "Sales" && !p.IsSold).Select(p => p);
            return View(await available.ToListAsync());
        }

        [Authorize]
        public ActionResult Sold(int id)
        {
            SoldProduct soldProduct = new SoldProduct()
            {
                DateSold = DateTime.Now,
                ProductID = id
            };
            db.SoldProducts.Add(soldProduct);
            db.SaveChanges();

            Product product = db.Products.Find(id);
            product.IsSold = true;
            //db.Products.Update(product);
            db.SaveChanges();


            return RedirectToAction("Receipt");
        }

        [Authorize]
        public ActionResult Receipt()
        {
            return View();
        }

        // GET: ChocoStore
        public ActionResult Index()
        {
            //var chocoStores = db.ChocoStores.Include(c => c.Manager);
            var available = db.Products
                .Include(p => p.ChocoCategory)
                .Where(p => p.DestinationDepartment.DepartmentName == "Sales" && !p.IsSold)
                .Select(p => p)
                .Count();
            var sold = db.SoldProducts
                        .Where(p => p.DateSold == DateTime.Today)
                        .Include(p => p.Product)
                        .Include(c => c.Product.ChocoCategory)
                        .Select(p => p)
                        .Count();

            var ourStoreToday = new ChocoStore()
            {
                Date = DateTime.Today,
                StoreName = "Choco Store",
                Address = "Egaleo",
                NumberofProductsSoldToday = sold,
                TodaysStock = available - sold,
                TodaysProfit = sold * 3,
                //Manager = db.Employees.Include(d => d.).Where(d => d.Roles. == "ChocoStore Manager").Select(n => n)
                
                
            };

            return View(ourStoreToday);
        }

        // GET: ChocoStore/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChocoStore chocoStore = await db.ChocoStores.FindAsync(id);
            if (chocoStore == null)
            {
                return HttpNotFound();
            }
            return View(chocoStore);
        }

        // GET: ChocoStore/Create
        public ActionResult Create()
        {
            ViewBag.ManagerId = new SelectList(db.Users, "Id", "VATNumber");
            return View();
        }

        // POST: ChocoStore/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ChocoStoreID,Date,StoreName,Address,DepartmentID,NumberofProductsSoldToday,TodaysStock,TodaysProfit,ManagerId")] ChocoStore chocoStore)
        {
            if (ModelState.IsValid)
            {
                db.ChocoStores.Add(chocoStore);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ManagerId = new SelectList(db.Users, "Id", "VATNumber", chocoStore.ManagerId);
            return View(chocoStore);
        }

        // GET: ChocoStore/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChocoStore chocoStore = await db.ChocoStores.FindAsync(id);
            if (chocoStore == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagerId = new SelectList(db.Users, "Id", "VATNumber", chocoStore.ManagerId);
            return View(chocoStore);
        }

        // POST: ChocoStore/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ChocoStoreID,Date,StoreName,Address,DepartmentID,NumberofProductsSoldToday,TodaysStock,TodaysProfit,ManagerId")] ChocoStore chocoStore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chocoStore).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ManagerId = new SelectList(db.Users, "Id", "VATNumber", chocoStore.ManagerId);
            return View(chocoStore);
        }

        // GET: ChocoStore/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChocoStore chocoStore = await db.ChocoStores.FindAsync(id);
            if (chocoStore == null)
            {
                return HttpNotFound();
            }
            return View(chocoStore);
        }

        // POST: ChocoStore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ChocoStore chocoStore = await db.ChocoStores.FindAsync(id);
            db.ChocoStores.Remove(chocoStore);
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
