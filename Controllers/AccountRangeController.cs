using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScheduleManagement.Models;

namespace ScheduleManagement.Controllers
{
    public class AccountRangeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccountRange
        public ActionResult Index()
        {
            var accountRanges = db.AccountRanges.Include(a => a.MasterSchedule);
            return View(accountRanges.ToList());
        }

        // GET: AccountRange/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountRange accountRange = db.AccountRanges.Find(id);
            if (accountRange == null)
            {
                return HttpNotFound();
            }
            return View(accountRange);
        }

        // GET: AccountRange/Create
        public ActionResult Create()
        {
            ViewBag.MasterScheduleID = new SelectList(db.MasterSchedules, "MasterScheduleID", "ScheduleID");
            return View();
        }

        // POST: AccountRange/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountRangeID,CostCenterFrom,CostCenterTo,NaturalAccountFrom,NaturalAccountTo,Org,MasterScheduleID")] AccountRange accountRange)
        {
            if (ModelState.IsValid)
            {
                db.AccountRanges.Add(accountRange);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MasterScheduleID = new SelectList(db.MasterSchedules, "MasterScheduleID", "ScheduleID", accountRange.MasterScheduleID);
            return View(accountRange);
        }

        // GET: AccountRange/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountRange accountRange = db.AccountRanges.Find(id);
            if (accountRange == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterScheduleID = new SelectList(db.MasterSchedules, "MasterScheduleID", "ScheduleID", accountRange.MasterScheduleID);
            return View(accountRange);
        }

        // POST: AccountRange/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountRangeID,CostCenterFrom,CostCenterTo,NaturalAccountFrom,NaturalAccountTo,Org,MasterScheduleID")] AccountRange accountRange)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountRange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MasterScheduleID = new SelectList(db.MasterSchedules, "MasterScheduleID", "ScheduleID", accountRange.MasterScheduleID);
            return View(accountRange);
        }

        // GET: AccountRange/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountRange accountRange = db.AccountRanges.Find(id);
            if (accountRange == null)
            {
                return HttpNotFound();
            }
            return View(accountRange);
        }

        // POST: AccountRange/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountRange accountRange = db.AccountRanges.Find(id);
            db.AccountRanges.Remove(accountRange);
            db.SaveChanges();
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
