using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;

namespace FMS_Data
{
   public class FMSDbContext:DbContext
    {
       public FMSDbContext()
           : base("FMSDbConnection")
        {
            
        }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<OwnerInfo> OwnerInfos { get; set; }
        public DbSet<WorkerInfo> WorkerInfos { get; set; }
        public DbSet<PostAProject> PostAProjects { get; set; }
        public DbSet<ResponseToaJob> Response { get; set; }
        public DbSet<SkillCategory> SkillCategories { get; set; }
        public DbSet<Skill> Skills { get; set; }    
        public DbSet<EducationalBackground> EducationalBackgrounds { get; set; }
        public DbSet<WorkHistory> WorkHistories { get; set; }
        public DbSet<ProjectSkills> ProjectSkills { get; set; }
        public DbSet<ProjectSection> ProjectSections { get; set; }
        public DbSet<RatingOwner> RatingOwners { get; set; }
        public DbSet<RatingWorker> RatingWorkers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<SelectedWorker> SelectedWorkers { get; set; }
        public DbSet<COMMENTSEC> Commentsecs { get; set; }
        public DbSet<SavedFile> SavedFiles { get; set; }
        public DbSet<WorkerSkill> WorkerSkills { get; set; }


    }
}
