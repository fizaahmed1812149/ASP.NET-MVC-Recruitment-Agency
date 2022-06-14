using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class CandidateRoleMapping
    {
        [Key]
        public int Id { get; set; }

        public Candidates candidates { get; set; }

        public CandidateRole CandidateRole { get; set; }
    }
}