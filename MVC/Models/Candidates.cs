using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVC.Models
{
    [Authorize]
    public class Candidates
    {
        [Display(Name = "Candidate IDs")]
        [Key]
        public int CandidateID { get; set; }
        [Display(Name = "Candidate Name")]
        [Required]
        [MaxLength (50)]
        public string CandidateName { get; set; }
        [Display(Name = "Experience in Years")]
        public int ExperienceInYears { get; set; }

        [Display(Name = "Qualification")]
        public string Qualification { get; set; }
        [Display(Name = "Active Designation")]
        public string ActiveDesignation { get; set; }
        [Display(Name = "Industry")]
        public string Industry { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }
    }

  
}