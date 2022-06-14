namespace MVC.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVC.Models.RAgencyDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MVC.Models.RAgencyDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Candidates.AddOrUpdate(c => c.CandidateName,
                new Models.Candidates
                {
                    CandidateID = 1,
                    CandidateName = "Fiza",
                    ExperienceInYears = 2,
                    Qualification = "Bachelors in CS",
                    ActiveDesignation = "Sr Java Developer",
                    Industry = "IT",
                    Status = "Active",
                    Password = "fizaahmed"
                }
                );

            context.Jobs.AddOrUpdate(j => j.JobDescription,
              new Models.Jobs
              {
                  JobID = 1,
                  JobDescription = "You have to fix errors",
                  Position = "Quality Assurance",
                  Salary = 130054,
                  ExperienceRequired = 1,
                  NoOfPositions = 3,
                  Location = "Karachi",
                  Status = "Active"
              }
              );
        }
    }
}
