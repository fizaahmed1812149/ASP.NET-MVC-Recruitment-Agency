using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        RAgencyDBContext db = new RAgencyDBContext();

        // GET: Jobs
        [Authorize(Roles = "Admin")]
        public ActionResult IndexJobs()
        {
            var job = db.Jobs.Select(x => x);
            return View(job);
        }

        [Authorize(Roles = "Candidate")]
        public ActionResult JobList()
        {
            var job = db.Jobs.Select(x => x);
            return View(job);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CreateJobs()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateJobs(Jobs jobs)
        {
            db.Jobs.Add(jobs);
            db.SaveChanges();
            return RedirectToAction("IndexJobs");

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditJobs(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var jobs = db.Jobs.Where(j => j.JobID == id).FirstOrDefault();
            if (jobs == null)
                return HttpNotFound();
            return View(jobs);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditJobs(Jobs jobs)
        {

            db.Entry(jobs).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("IndexJobs");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult DeleteJobs(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var jobs = db.Jobs.Where(j => j.JobID == id).FirstOrDefault();
            if (jobs == null)
                return HttpNotFound();
            return View(jobs);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteJobs(int id)
        {
            var jobs = db.Jobs.Where(j => j.JobID == id).FirstOrDefault();
            db.Jobs.Remove(jobs);
            db.SaveChanges();
            return RedirectToAction("IndexJobs");
        }
    }
}