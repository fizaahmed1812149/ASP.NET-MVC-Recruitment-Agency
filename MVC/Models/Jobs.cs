using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Jobs
    {
        [Display(Name = "Job IDs")]
        [Key]
        public int JobID { get; set; }
        [Display(Name = "Job Description")]
        [Required]
        [MaxLength(50)]
        public string JobDescription { get; set; }
        [Display(Name = "Position")]
        [Required]
        public string Position { get; set; }

        [Display(Name = "Salary")]
        [Required]
        public int Salary { get; set; }
        [Display(Name = "ExperiencedRequired")]
        [Required]
        public int ExperienceRequired { get; set; }
        [Display(Name = "No. of Positions")]
        [Required]
        public int NoOfPositions { get; set; }
        [Display(Name = "Location")]
        public string Location { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
    }
   
}