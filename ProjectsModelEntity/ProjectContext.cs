using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsModel;

namespace ProjectsModelEntity
{
    public class ProjectContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Duration> Durations { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<OperationSystem> OperationSystems { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<ProjectsModel.Type> Types { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasRequired<Customer>(p => p.Customer).WithMany(c => c.Projects);
            modelBuilder.Entity<Project>().HasRequired<Objective>(p => p.Objective).WithMany(o => o.Projects);
            modelBuilder.Entity<Project>().HasRequired<Duration>(p => p.Duration).WithMany(d => d.Projects);
            modelBuilder.Entity<Project>().HasRequired<Lead>(p => p.Lead).WithMany(l => l.Projects);
            modelBuilder.Entity<Project>().HasRequired<Objective>(p => p.Objective).WithMany(o => o.Projects);
            modelBuilder.Entity<Project>().HasRequired<Stage>(p => p.Stage).WithMany(s => s.Projects);
            modelBuilder.Entity<File>().HasRequired<Project>(f => f.Project).WithMany(p => p.Files);
            modelBuilder.Entity<File>().HasRequired<Project>(f => f.Project).WithMany(p => p.Files);

            modelBuilder.Entity<Project>().HasMany(p => p.Types).
                WithMany(t => t.Projects).
                Map(t => t.MapLeftKey("ProjectId").
                    MapRightKey("TypeId").
                    ToTable("ProjectsTypes"));

            modelBuilder.Entity<Project>().HasMany(p => p.Skills).
                WithMany(s => s.Projects).
                Map(s => s.MapLeftKey("ProjectId").
                    MapRightKey("SkillId").
                    ToTable("ProjectsSkills"));

            modelBuilder.Entity<Project>().HasMany(p => p.OperationSystems).
                WithMany(o => o.Projects).
                Map(s => s.MapLeftKey("ProjectId").
                    MapRightKey("OperationSystemId").
                    ToTable("ProjectsOperationSystems"));
        }

    }
}
