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
    public class CandidateController : Controller
    {


        RAgencyDBContext db = new RAgencyDBContext();

        // GET: Candidate
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            IEnumerable<CandidateRoleMapping> candidates = db.candidateRoleMappings
                .Include("Candidates")
                .Include("CandidateRole")
                .Where(x => x.CandidateRole.Role == "Candidate")
                .Select(x => x);
            return View(candidates);
        }

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Create(Candidates candidate)
        //{
        //    db.Candidates.Add(candidate);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");

        //}

        [Authorize(Roles = "Candidate")]
        public ActionResult Edit(int? id)
        {
            string userEmail = User.Identity.Name;
            Candidates candidate = db.Candidates.FirstOrDefault(c => c.CandidateName == userEmail);

            return View(candidate);
        }
        [HttpPost]
        [Authorize(Roles = "Candidate")]
        public ActionResult Edit([Bind(Exclude = "Status")] Candidates candidate)
        {
            if (!ModelState.IsValid)
                return View(candidate);

            Candidates dbCandidate = db.Candidates.FirstOrDefault(u => u.CandidateID == candidate.CandidateID);
            dbCandidate.CandidateName = candidate.CandidateName;
            dbCandidate.Password = candidate.Password;
            dbCandidate.CandidateName = candidate.CandidateName;
            dbCandidate.Qualification = candidate.Qualification;
            dbCandidate.Industry = candidate.Industry;
            dbCandidate.ExperienceInYears = candidate.ExperienceInYears;
            dbCandidate.ActiveDesignation = candidate.ActiveDesignation;

            db.Entry(dbCandidate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("JobList", "Job");
        }
        //[HttpGet]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //        return HttpNotFound();
        //    var candidate = db.Candidates.Where(c => c.CandidateID == id).FirstOrDefault();
        //    if (candidate == null)
        //        return HttpNotFound();
        //    return View(candidate);
        //}
        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    var candidate = db.Candidates.Where(c => c.CandidateID == id).FirstOrDefault(); 
        //    db.Candidates.Remove(candidate);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}