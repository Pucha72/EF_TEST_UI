using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EF_Migration;
using EF_Models;
using System.Configuration;

namespace EF_TEST_UI.Controllers
{
    public class StandardMasterController : Controller
    {

        private LocalContext db = new LocalContext(ConfigurationSettings.AppSettings.Get("DatabaseLink").ToString());
        //"LocalContextDB"
        // public LocalContext() : base("name=SchoolDBConnectionString")

        // GET: StandardMaster
        public async Task<ActionResult> Index()
        {

            return View(await db.standard.ToListAsync());
        }

        // GET: StandardMaster/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Standard standard = await db.standard.FindAsync(id);
            if (standard == null)
            {
                return HttpNotFound();
            }
            return View(standard);
        }

        // GET: StandardMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StandardMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "standard_id,standard_name")] Standard standard)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Error = null;
                if (StandardExists(standard.standard_name))
                {
                    ViewBag.Error = "Standard Name Already Exists!";
                    return View();
                }
                db.standard.Add(standard);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(standard);
        }

        // GET: StandardMaster/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Standard standard = await db.standard.FindAsync(id);
            if (standard == null)
            {
                return HttpNotFound();
            }
            return View(standard);
        }

        // POST: StandardMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "standard_id,standard_name")] Standard standard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(standard).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(standard);
        }

        // GET: StandardMaster/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Standard standard = await db.standard.FindAsync(id);
            if (standard == null)
            {
                return HttpNotFound();
            }
            return View(standard);
        }

        // POST: StandardMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Standard standard = await db.standard.FindAsync(id);
            db.standard.Remove(standard);
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
        private bool StandardExists(string key)
        {
            return db.standard.Count(e => e.standard_name == key) > 0;
        }
    }
}
