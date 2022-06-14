using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC.Models;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
            {
                Response.Redirect(Request.Url.AbsolutePath);
            }
        }

        // GET: Account
        private RAgencyDBContext db = new RAgencyDBContext();
        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Candidates candidate)
        {
            if (!ModelState.IsValid) return View(candidate);

            candidate.Password = HashPassword(candidate.Password);

            bool isValidUser = db.Candidates.Any(c => c.CandidateName.ToLower() == candidate.CandidateName.ToLower()
                && c.Password == candidate.Password);

            if (isValidUser)
            {
                FormsAuthentication.SetAuthCookie(candidate.CandidateName, false);

                var candidateRole = db.candidateRoleMappings
                    .Include("Candidates")
                    .Include("CandidateRole")
                    .Where(c => c.candidates.CandidateName.ToLower() == candidate.CandidateName.ToLower())
                    .FirstOrDefault();

                if (candidateRole.CandidateRole.Role == "Admin")
                    return RedirectToAction("Index", "Candidate");


                if (candidateRole.CandidateRole.Role == "Candidate")
                    return RedirectToAction("JobList", "Jobs");

            }
            ModelState.AddModelError("", "Invalid username or password!");
            return View();
        }
        public ActionResult SignUp()
        {
            var roles = from c in db.candidateRoles
                        select new
                        {
                            RoleId = c.RoleId,
                            Role = c.Role
                        };
            SelectList rolesList = new SelectList(roles, "RoleId", "Role");
            ViewBag.Roles = rolesList;
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Candidates candidate, string confirmPassword, int RoleId)
        {
            if (!ModelState.IsValid)
            {
                var roles = from c in db.candidateRoles
                            select new
                            {
                                RoleId = c.RoleId,
                                Role = c.Role
                            };
                SelectList rolesList = new SelectList(roles, "RoleId", "Role");
                ViewBag.Roles = rolesList;
                return View(candidate);
            }

            bool ifExists = db.Candidates.Any(u => u.CandidateName.ToLower() == candidate.CandidateName.ToLower());
            if (ifExists)
            {
                var roles = from c in db.candidateRoles
                            select new
                            {
                                RoleId = c.RoleId,
                                Role = c.Role
                            };
                SelectList rolesList = new SelectList(roles, "RoleId", "Role");
                ViewBag.Roles = rolesList;

                ModelState.AddModelError("", "Username already Exists!");
                return View(candidate);
            }
            else if (!confirmPassword.Equals(candidate.Password))
            {
                var roles = from c in db.candidateRoles
                            select new
                            {
                                RoleId = c.RoleId,
                                Role = c.Role
                            };
                SelectList rolesList = new SelectList(roles, "RoleId", "Role");
                ViewBag.Roles = rolesList;

                ModelState.AddModelError("", "Passwords donot match!");
                return View(candidate);
            }
            else
            {
                if (RoleId == 2 && (candidate.ExperienceInYears == null || candidate.ActiveDesignation == null || candidate.Qualification == null || candidate.Industry == null))
                {
                    var roles = from c in db.candidateRoles
                                select new
                                {
                                    RoleId = c.RoleId,
                                    Role = c.Role
                                };
                    SelectList rolesList = new SelectList(roles, "RoleId", "Role");
                    ViewBag.Roles = rolesList;

                    ModelState.AddModelError("", "Please complete all fields");
                    return View(candidate);
                }

                candidate.Status = "Active";
                candidate.Password = HashPassword(candidate.Password);
                db.Candidates.Add(candidate);
                db.SaveChanges();

                Models.CandidateRole role = db.candidateRoles.FirstOrDefault(r => r.RoleId == RoleId);

                CandidateRoleMapping candidateRoleMapping = new CandidateRoleMapping
                {
                    candidates = candidate,
                    CandidateRole = role
                };

                db.candidateRoleMappings.Add(candidateRoleMapping);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        private string HashPassword(string password)
        {
            byte[] encData_byte = new byte[password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encData_byte);
        }
    }
}