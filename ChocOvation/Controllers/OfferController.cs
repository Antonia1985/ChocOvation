using ChocOvation.Models;
using ChocOvation.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChocOvation.Controllers
{
    [Authorize(Roles = "Supplier, CEO, Admin")]
    public class OfferController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult MyOffers()
        {

            var userId = User.Identity.GetUserId();


            var myOffers = db.Offers
                .Where(a => a.SupplierID == userId)
                .ToList();


            return View(myOffers);
        }

        // GET: Offer
        [Authorize]
        public async Task<ActionResult> BestOffers()
        {
            var offers = db.Offers.Include(o => o.Supplier).OrderByDescending(o => o.TotalPriceQuality).Take(3);

            ViewBag.checkRole = User.IsInRole("Admin");

            return View(await offers.ToListAsync());
        }

        // GET: Offer
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var offers = db.Offers.Include(o => o.Supplier).OrderByDescending(o => o.TotalPriceQuality);

            ViewBag.checkRole = User.IsInRole("Admin");

            return View(await offers.ToListAsync());
        }

        // GET: Offer/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = await db.Offers.FindAsync(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            var offersPerMaterial = db.OffersPerMaterials
                .Where(s => s.OfferID == id)
                .Include(s => s.Material)
                .ToList();


            var ListOfLittleOffers = offersPerMaterial.AsEnumerable();


            ViewBag.checkRole = User.IsInRole("Supplier");
            ViewBag.supID = offer.SupplierID;
            return View(ListOfLittleOffers);
        }

        // GET: Offer/Create
        [Authorize]
        public ActionResult Create()
        {
            var distinctmaterials = db.Materials.Select(m => m.MaterialName).Distinct().ToList();
            var numberOfMaterials = distinctmaterials.Count();
            OfferFormViewModel[] LittleOffers = new OfferFormViewModel[numberOfMaterials];

            for (int i = 0; i < numberOfMaterials; i++)
            {
                var viewModel = new OfferFormViewModel();
                viewModel.MaterialName = distinctmaterials[i];
                viewModel.Quality = Quality.High;
                viewModel.PricePerKg = 1;

                LittleOffers[i] = viewModel;
            }

            var ListOfLittleOffers = LittleOffers.ToList();

            return View(ListOfLittleOffers);
        }


        // POST: Offer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaterialName,Quality,PricePerKg")] List<OfferFormViewModel> viewModel)
        {
            var distinctmaterials = db.Materials.Select(m => m.MaterialName).Distinct().ToList();
            var numberOfMaterials = distinctmaterials.Count();

            if (ModelState.IsValid)
            {
                var offer = new Offer();
                offer.DateOfOffer = DateTime.Today;
                offer.SupplierID = User.Identity.GetUserId();
                db.Offers.Add(offer);
                await db.SaveChangesAsync();
                float? index = 0;

                foreach (var item in viewModel)
                {
                    var materialID = db.Materials
                        .Where(m => (m.MaterialName == item.MaterialName)
                        && (m.Quality == item.Quality))
                        .Select(i => i.MaterialID)
                        .FirstOrDefault();


                    var littleOffer = new OfferPerMaterial
                    {
                        OfferID = offer.OfferID,
                        MaterialID = materialID,
                        PricePerKg = item.PricePerKg,
                        PriceQuality = item.PriceANDQuality
                    };
                    index = index + littleOffer.PriceQuality;
                    db.OffersPerMaterials.Add(littleOffer);
                    await db.SaveChangesAsync();
                }


                if (db.OffersPerMaterials.Count() != 0)
                {

                    offer.TotalPriceQuality = (100 * index) / db.OffersPerMaterials.Count();
                }
                await db.SaveChangesAsync();

                return RedirectToAction("MyOffers");
            }
            return View(viewModel);
        }

        // GET: Offer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var offer = db.Offers.Where(o => o.OfferID == id).Include(o => o.Supplier).Single();
            if (offer == null)
            {
                return HttpNotFound();
            }
            ViewBag.checkRole = User.IsInRole("Supplier");

            return View(offer);
        }

        // POST: Offer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OfferID,SupplierID,DateOfOffer")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("MyOffers");
            }

            ViewBag.checkRole = User.IsInRole("Supplier");
            //ViewBag.supID = offer.SupplierID;
            return View(offer);
        }

        // GET: Offer/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.checkRole = User.IsInRole("Supplier");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = await db.Offers.FindAsync(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);

        }

        // POST: Offer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            bool check = User.IsInRole("Supplier");

            Offer offer = await db.Offers.FindAsync(id);
            db.Offers.Remove(offer);
            await db.SaveChangesAsync();

            if (check)
            {
                return RedirectToAction("MyOffers");
            }
            else
            {
                return RedirectToAction("Index");
            }

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
