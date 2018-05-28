using ChocOvation.Models;
using ChocOvation.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChocOvation.Controllers
{
    [Authorize(Roles = "ProductionManager, CEO, Admin")]
    public class ChocoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Choco
        public async Task<ActionResult> Index()
        {
            return View(await db.Chocos.ToListAsync());
        }

        // GET: Choco/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Choco choco = await db.Chocos.FindAsync(id);
            if (choco == null)
            {
                return HttpNotFound();
            }

            var dosesPerMaterials = db.DosesPerMaterials.Include(d => d.Choco).Include(d => d.Material).ToList();
            var distinct = db.DosesPerMaterials.Where(m => m.ChocoID == id).Select(m => m.Material.MaterialName).Distinct().ToList();
            var doselist = new List<DoseFormViewModel>();

            for (int i = 0; i < distinct.Count(); i++)
            {
                var myDist = distinct[i];
                DoseFormViewModel dose = new DoseFormViewModel
                {
                    ChocoName = choco.ChocoName,
                    MaterialName = myDist,
                    QuantityPer100gr = db.DosesPerMaterials.First(c => c.Material.MaterialName == myDist && c.ChocoID == id).QuantityPer100gr
                };
                doselist.Add(dose);
            }
            var ChocoDoses = new ChocoDoseViewModel
            {
                Choco = choco,
                DosesViewModel = doselist
            };
            return View(ChocoDoses);
        }

        // GET: Choco/Create
        // GET: DosePerMaterial/Create
        public ActionResult Create()
        {
            var choco = new Choco();
            var distinctmaterials = db.Materials.Select(m => m.MaterialName).Distinct().ToList();
            var numberOfMaterials = distinctmaterials.Count();
            DoseFormViewModel[] Doses = new DoseFormViewModel[numberOfMaterials];

            for (int i = 0; i < numberOfMaterials; i++)
            {
                var viewModel = new DoseFormViewModel();
                viewModel.MaterialName = distinctmaterials[i];
                viewModel.ChocoName = choco.ChocoName;
                viewModel.QuantityPer100gr = 1;

                Doses[i] = viewModel;
            }

            var recipe = new ChocoDoseViewModel
            {
                Choco = choco,
                DosesViewModel = Doses.ToList()
            };

            return View(recipe);
        }

        // POST: Choco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(/*[Bind(Include = "DosePerMaterialID,ChocoName,MaterialName,QuantityPer100gr")]*/
            ChocoDoseViewModel recipe)
        {

            if (ModelState.IsValid)
            {

                var choco = new Choco();
                choco.ChocoName = recipe.Choco.ChocoName;

                db.Chocos.Add(choco);
                await db.SaveChangesAsync();

                foreach (DoseFormViewModel dose in recipe.DosesViewModel)
                {
                    var ids = db.Materials.Where(m => m.MaterialName == dose.MaterialName).Select(m => m.MaterialID).ToList();
                    var ds = new DosePerMaterial();
                    for (int i = 0; i < 3; i++)
                    {
                        ds.MaterialID = ids[i];
                        ds.ChocoID = choco.ChocoID;
                        ds.QuantityPer100gr = dose.QuantityPer100gr;
                        db.DosesPerMaterials.Add(ds);
                        await db.SaveChangesAsync();
                    }
                }


                return RedirectToAction("Index");
            }

            return View(recipe);
        }

        // GET: Choco/Edit/5
        public async Task<ActionResult> Edit([Bind(Include = "ChocoID")] int id)
        {

            Choco choco = await db.Chocos.FindAsync(id);
            if (choco == null)
            {
                return HttpNotFound();
            }
            var distinctmaterials = db.Materials.Select(m => m.MaterialName).Distinct().ToList();
            var numberOfMaterials = distinctmaterials.Count();
            DoseFormViewModel[] Doses = new DoseFormViewModel[numberOfMaterials];

            for (int i = 0; i < numberOfMaterials; i++)
            {
                var myMatId = distinctmaterials[i];
                var query = db.DosesPerMaterials.Where(m => (m.Material.MaterialName == myMatId) && (m.ChocoID == choco.ChocoID)).Select(m => m.QuantityPer100gr).FirstOrDefault();
                var viewModel = new DoseFormViewModel();
                viewModel.MaterialName = distinctmaterials[i];
                viewModel.ChocoName = choco.ChocoName;
                viewModel.QuantityPer100gr = query;

                Doses[i] = viewModel;
            }

            var recipe = new ChocoDoseViewModel
            {
                Choco = choco,
                DosesViewModel = Doses.ToList()
            };

            return View(recipe);
        }

        // POST: Choco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(/*[Bind(Include = "ChocoID")]*/ ChocoDoseViewModel recipe)
        {
            if (ModelState.IsValid)
            {
                //var oldChocoName = db.Chocos.Where(c => c.ChocoID == recipe.Choco.ChocoID).Select(c => c.ChocoName).ToString();
                //var oldRecipeList = db.DosesPerMaterials.Where(d => d.ChocoID == oldChoco);

                var choco = recipe.Choco;

                db.Entry(choco).State = EntityState.Modified;
                //await db.SaveChangesAsync();

                foreach (DoseFormViewModel dose in recipe.DosesViewModel)
                {
                    var ids = db.Materials.Where(m => m.MaterialName == dose.MaterialName).Select(m => m.MaterialID).ToList();
                    var ds = new DosePerMaterial();
                    for (int i = 0; i < 3; i++)
                    {
                        ds.MaterialID = ids[i];
                        ds.ChocoID = choco.ChocoID;
                        ds.QuantityPer100gr = dose.QuantityPer100gr;
                        db.Entry(ds).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                }
                return RedirectToAction("Index");
            }
            return View(recipe);
        }

        // GET: Choco/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choco choco = await db.Chocos.FindAsync(id);
            if (choco == null)
            {
                return HttpNotFound();
            }
            return View(choco);
        }

        // POST: Choco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Choco choco = await db.Chocos.FindAsync(id);
            db.Chocos.Remove(choco);
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
