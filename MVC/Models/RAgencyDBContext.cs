using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class RAgencyDBContext : DbContext
    {
        public DbSet<Candidates> Candidates { get; set; }
        public DbSet<Jobs> Jobs { get; set; }

        public DbSet<CandidateRole> candidateRoles { get; set; }

        public DbSet<CandidateRoleMapping> candidateRoleMappings { get; set; }
    }
}