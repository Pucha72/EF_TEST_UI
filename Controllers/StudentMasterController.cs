using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using EF_TEST_UI.Models;
using EF_Migration;
using EF_Models;
using System.Configuration;

namespace EF_TEST_UI.Controllers
{
    public class StudentMasterController : Controller
    {
        private LocalContext db = new LocalContext(ConfigurationSettings.AppSettings.Get("DatabaseLink").ToString());

        // GET: StudentMaster
        public async Task<ActionResult> Index()
        {
            var student = db.student.Include(s => s.standard);
            return View(await student.ToListAsync());
        }

        // GET: StudentMaster/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.student.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: StudentMaster/Create
        public ActionResult Create()
        {
            ViewBag.standard_id = new SelectList(db.standard, "standard_id", "standard_name");
            return View();
        }

        // POST: StudentMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "student_id,student_name,standard_id")] Student student)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Error = null;
                if (StudentExists(student.student_name, Convert.ToInt32(student.standard_id)))
                {
                    ViewBag.Error = "Student Name Already Exists in the selected Standard!";
                    return View();
                }
                db.student.Add(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.standard_id = new SelectList(db.standard, "standard_id", "standard_name", student.standard_id);
            return View(student);
        }

        // GET: StudentMaster/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.student.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.standard_id = new SelectList(db.standard, "standard_id", "standard_name", student.standard_id);
            return View(student);
        }

        // POST: StudentMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "student_id,student_name,standard_id")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.standard_id = new SelectList(db.standard, "standard_id", "standard_name", student.standard_id);
            return View(student);
        }

        // GET: StudentMaster/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.student.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: StudentMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Student student = await db.student.FindAsync(id);
            db.student.Remove(student);
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
        private bool StudentExists(string key, int standard)
        {
            var count = (from s in db.student
                         where s.standard_id == standard && s.student_name == key
                         select s).Count();
            return (count > 0);
        }
    }
}
