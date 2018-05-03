using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScheduleManagement.Models;
using ScheduleManagement.ViewModels;

namespace ScheduleManagement.Controllers
{
    public class MasterScheduleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MasterSchedule
        //public ActionResult Index()
        //{
        //    return View(db.MasterSchedules.ToList());
        //}

        public ActionResult Index()
        {
            IEnumerable<MasterScheduleViewModel> masterSchedules = from m in db.MasterSchedules
                                                                   join ms in db.MasterSchedules
                                                                   on m.MinorScheduleID equals ms.MasterScheduleID into output
                                                                   from j in output.DefaultIfEmpty()
                                                                   select new MasterScheduleViewModel
                                                                   {
                                                                       MS_VM_ID = m.MasterScheduleID,
                                                                       ScheduleID = m.ScheduleID,
                                                                       ScheduleDescription = m.ScheduleDescription,
                                                                       MinorScheduleDescription = j == null ? "" : j.ScheduleDescription, 
                                                                       HasReport = m.HasReport
                                                                   };

            return View(masterSchedules.ToList());
        }


        // GET: MasterSchedule/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //MasterSchedule masterSchedule = db.MasterSchedules.Find(id);
            MasterSchedule masterSchedule = db.MasterSchedules.Find(id);
            PopulateMinorScheduleDropDownList(masterSchedule.MinorScheduleID);
            if (masterSchedule == null)
            {
                return HttpNotFound();
            }
            //PopulateMinorScheduleDropDownList(masterSchedule.MinorScheduleID);
            return View(masterSchedule);
        }

        // GET: MasterSchedule/Create
        public ActionResult Create()
        {
            PopulateMinorScheduleDropDownList();
            return View();
        }

        // POST: MasterSchedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MasterScheduleID,ScheduleID,ScheduleDescription,HasReport,MinorScheduleID")] MasterSchedule masterSchedule)
        {
            if (ModelState.IsValid)
            {
                db.MasterSchedules.Add(masterSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterSchedule);
        }

        // GET: MasterSchedule/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterSchedule masterSchedule = db.MasterSchedules.Find(id);
            if (masterSchedule == null)
            {
                return HttpNotFound();
            }
            PopulateMinorScheduleDropDownList(masterSchedule.MinorScheduleID);
            return View(masterSchedule);
        }

        // POST: MasterSchedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MasterScheduleID,ScheduleID,ScheduleDescription,HasReport,MinorScheduleID")] MasterSchedule masterSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(masterSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterSchedule);
        }

        // GET: MasterSchedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterSchedule masterSchedule = db.MasterSchedules.Find(id);
            if (masterSchedule == null)
            {
                return HttpNotFound();
            }
            return View(masterSchedule);
        }

        // POST: MasterSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterSchedule masterSchedule = db.MasterSchedules.Find(id);
            db.MasterSchedules.Remove(masterSchedule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void PopulateMinorScheduleDropDownList(object selectedMinorSchedule = null)
        {
            var minorScheduleQuery = from d in db.MasterSchedules
                                     orderby d.ScheduleID
                                     select d;
            ViewBag.minorScheduleID = new SelectList(minorScheduleQuery, "MasterScheduleID", "ScheduleDescription", selectedMinorSchedule);
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
