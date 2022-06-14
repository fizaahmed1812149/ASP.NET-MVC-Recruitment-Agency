using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class CandidateRole
    {
        [Key]
        public int RoleId { get; set; }

        [Display(Name = "Candidate Role")]
        [Required(ErrorMessage = "Role is Required!")]
        public string Role { get; set; }
    }
}