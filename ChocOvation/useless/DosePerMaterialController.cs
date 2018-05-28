using ChocOvation.Models;
using ChocOvation.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChocOvation.Controllers
{
    [Authorize(Roles = "ProductionManager, CEO, Admin")]
    public class DosePerMaterialController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DosePerMaterial
        public ActionResult Index()
        {
            var dosesPerMaterials = db.DosesPerMaterials.Include(d => d.Choco).Include(d => d.Material).ToList();

            var distinct = db.DosesPerMaterials.Select(m => m.Material.MaterialName).Distinct().ToList();
            var list = new List<DosePerMaterial>();

            for (int i = 0; i < distinct.Count(); i++)
            {
                var myDist = distinct[i];
                DosePerMaterial dose = new DosePerMaterial
                {
                    ChocoID = db.DosesPerMaterials.First(c => c.Material.MaterialName == myDist).ChocoID,
                    Choco = db.DosesPerMaterials.First(c => c.Material.MaterialName == myDist).Choco,
                    MaterialID = db.DosesPerMaterials.First(c => c.Material.MaterialName == myDist).MaterialID,
                    Material = db.DosesPerMaterials.First(c => c.Material.MaterialName == myDist).Material,
                    QuantityPer100gr = db.DosesPerMaterials.First(c => c.Material.MaterialName == myDist).QuantityPer100gr
                };
                list.Add(dose);
            }

            IEnumerable<DosePerMaterial> doseList = list.AsEnumerable();
            return View(doseList);
        }

        //// GET: DosePerMaterial/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DoseFormViewModel viewModel = await db.DosesPerMaterials.FindAsync(id);
        //    if (dosePerMaterial == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dosePerMaterial);
        //}

        // GET: DosePerMaterial/Create
    //    public ActionResult Create(int chocoID)
    //    {

    //        var distinctmaterials = db.Materials.Select(m => m.MaterialName).Distinct().ToList();
    //        var numberOfMaterials = distinctmaterials.Count();
    //        DoseFormViewModel[] DoseperMat = new DoseFormViewModel[numberOfMaterials];

    //        for (int i = 0; i < numberOfMaterials; i++)
    //        {
    //            var viewModel = new DoseFormViewModel();
    //            viewModel.MaterialName = distinctmaterials[i];
    //            viewModel.ChocoID = chocoID;
    //            viewModel.QuantityPer100gr = 1;

    //            DoseperMat[i] = viewModel;
    //        }

    //        var recipe = DoseperMat.AsEnumerable();

    //        return View(recipe);
    //    }
    //    //ViewBag.ChocoID = new SelectList(db.Chocos, "ChocoID", "ChocoName");

    //    //var query = db.Materials.Select(m => m.MaterialName).Distinct().ToList();

    //    //ViewBag.MaterialName = new SelectList(query);

    //    //return View();
    //}

    //    // POST: DosePerMaterial/Create
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<ActionResult> Create([Bind(Include = "DosePerMaterialID,ChocoName,MaterialName,QuantityPer100gr")] DoseFormViewModel viewModel)
    //    {


    //        if (ModelState.IsValid)
    //        {

    //            var ids = db.Materials.Where(m => m.MaterialName == viewModel.MaterialName).Select(m => m.MaterialID).ToList();
    //            var dose = new DosePerMaterial();


    //            for (int i = 0; i < 3; i++)
    //            {
    //                dose.MaterialID = ids[i];
    //                dose.QuantityPer100gr = viewModel.QuantityPer100gr;
    //                dose.ChocoID = viewModel.ChocoID;

    //                db.DosesPerMaterials.Add(dose);
    //                await db.SaveChangesAsync();
    //            }

    //            return RedirectToAction("Index");
    //        }

    //        ViewBag.ChocoID = new SelectList(db.Chocos, "ChocoID", "ChocoName", viewModel.ChocoID);
    //        //ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "MaterialName", viewModel.MaterialID);
    //        var query = db.Materials.Select(m => m.MaterialName).Distinct().ToList();
    //        ViewBag.MaterialName = new SelectList(query);
    //        return View(viewModel);
    //    }

        // GET: DosePerMaterial/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DosePerMaterial dosePerMaterial = await db.DosesPerMaterials.FindAsync(id);
            if (dosePerMaterial == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChocoID = new SelectList(db.Chocos, "ChocoID", "ChocoName", dosePerMaterial.ChocoID);
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "MaterialName", dosePerMaterial.MaterialID);
            return View(dosePerMaterial);
        }

        // POST: DosePerMaterial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DosePerMaterialID,ChocoID,MaterialID,QuantityPer100gr")] DosePerMaterial dosePerMaterial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dosePerMaterial).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ChocoID = new SelectList(db.Chocos, "ChocoID", "ChocoName", dosePerMaterial.ChocoID);
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "MaterialName", dosePerMaterial.MaterialID);
            return View(dosePerMaterial);
        }

        // GET: DosePerMaterial/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DosePerMaterial dosePerMaterial = await db.DosesPerMaterials.FindAsync(id);
            if (dosePerMaterial == null)
            {
                return HttpNotFound();
            }
            return View(dosePerMaterial);
        }

        // POST: DosePerMaterial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DosePerMaterial dosePerMaterial = await db.DosesPerMaterials.FindAsync(id);
            db.DosesPerMaterials.Remove(dosePerMaterial);
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
