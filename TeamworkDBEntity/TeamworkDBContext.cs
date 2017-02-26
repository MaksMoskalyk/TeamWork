using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamworkDB;

namespace TeamworkDBEntity
{   
    public class TeamworkDBContext : DbContext
    {
        #region DbSets and constructor

        public TeamworkDBContext() : base("TeamworkDBContext")
        {
            Database.SetInitializer(new TeamworkDBInitializer());
        }

        // Employee.
        public DbSet<Citizenship> Citizenships { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAndMail> EmployeeAndMails { get; set; }
        public DbSet<EmployeeAndPhone> EmployeeAndPhones { get; set; }
        public DbSet<EmployeeAndWeb> EmployeeAndWebs { get; set; }      
        public DbSet<WebService> WebServices { get; set; }

        // Language.
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageProficiency> LanguageProficiencies { get; set; }

        // Education.
        public DbSet<EducationEntitlingDocument> EducationEntitlingDocuments { get; set; }
        public DbSet<EducationFacility> EducationFacilities { get; set; }
        public DbSet<EducationSpecialty> EducationSpecialties { get; set; }

        // Account.
        public DbSet<AccessLevel> AccessLevels { get; set; }
        public DbSet<Account> Accounts { get; set; }

        // Many to many entities.
        public DbSet<EmployeeAndEducation> EmployeesAndEducations { get; set; }
        public DbSet<EmployeeAndExperience> EmployeesAndExperiences { get; set; }
        public DbSet<EmployeeAndLanguage> EmployeesAndLanguages { get; set; }
        public DbSet<EmployeeAndSkill> EmployeesAndSkills { get; set; }
        public DbSet<EmployeeAndProject> EmployeesAndProjects { get; set; }
        public DbSet<CountryAndCity> CountriesAndCities { get; set; }

        // Skill.
        public DbSet<SkillsGroup> SkillsGroups { get; set; }
        public DbSet<SkillProficiency> SkillProficiencies { get; set; }
        public DbSet<Skill> Skills { get; set; }

        // Experiencce.
        public DbSet<Company> Companies { get; set; }
        public DbSet<Position> Positions { get; set; }

        #endregion

        #region Project
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCustomer> Customers { get; set; }
        public DbSet<ProjectDuration> Durations { get; set; }
        public DbSet<ProjectObjective> Objectives { get; set; }
        public DbSet<ProjectFile> ProjectFiles { get; set; }
        public DbSet<OperationSystem> OperationSystems { get; set; }
        public DbSet<ProjectStage> Stages { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        #endregion


        #region Task
        public DbSet<Priority> Priorityes { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<TaskFile> TaskFiles { get; set; }
        public DbSet<TaskComment> Comments { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        #endregion


        #region OnModelCreating

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Employee.
            Set_Citizenship_Config(modelBuilder);
            Set_City_Config(modelBuilder);
            Set_Country_Config(modelBuilder);
            Set_Employee_Config(modelBuilder);
            Set_EmployeeAndMail_Config(modelBuilder);
            Set_EmployeeAndPhone_Config(modelBuilder);
            Set_EmployeeAndWeb_Config(modelBuilder);           
            Set_WebService_Config(modelBuilder);

            // Language single entity.
            Set_Language_Config(modelBuilder);
            Set_LanguageProficiency_Config(modelBuilder);

            // Education single entity.
            Set_EducationEntitlingDocument_Config(modelBuilder);
            Set_EducationFacility_Config(modelBuilder);
            Set_EducationSpecialty_Config(modelBuilder);
            
            // Account single entity.
            Set_AccessLevel_Config(modelBuilder);
            Set_Account_Config(modelBuilder);

            // Many to many entities.
            Set_EmployeeAndEducation_Config(modelBuilder);
            Set_EmployeeAndExperience_Config(modelBuilder);
            Set_EmployeeAndLanguage_Config(modelBuilder);
            Set_EmployeeAndSkill_Config(modelBuilder);
            Set_EmployeeAndProject_Config(modelBuilder);
            Set_CountryAndCity_Config(modelBuilder);

            // Projects association.
            //Set_Project_Config(modelBuilder);

            // Skill.
            Set_SkillsGroup_Config(modelBuilder);
            Set_SkillProficiency_Config(modelBuilder);
            Set_Skill_Config(modelBuilder);

            // Experiencce.
            Set_Company_Config(modelBuilder);
            Set_Position_Config(modelBuilder);

            #region Project
            modelBuilder.Entity<Project>().HasRequired<ProjectCustomer>(p => p.Customer).WithMany(c => c.Projects);
            modelBuilder.Entity<Project>().HasRequired<ProjectObjective>(p => p.Objective).WithMany(o => o.Projects);
            modelBuilder.Entity<Project>().HasRequired<ProjectDuration>(p => p.Duration).WithMany(d => d.Projects);
            modelBuilder.Entity<Project>().HasRequired<ProjectObjective>(p => p.Objective).WithMany(o => o.Projects);
            modelBuilder.Entity<Project>().HasRequired<ProjectStage>(p => p.Stage).WithMany(s => s.Projects);
            modelBuilder.Entity<ProjectFile>().HasRequired<Project>(f => f.Project).WithMany(p => p.Files);
            modelBuilder.Entity<ProjectFile>().HasRequired<Project>(f => f.Project).WithMany(p => p.Files);

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
            #endregion

            #region Task
            modelBuilder.Entity<Issue>().HasRequired(s => s.Project).WithMany(p => p.Issues).WillCascadeOnDelete(true);
            modelBuilder.Entity<Issue>().HasOptional(s => s.Creator);
            modelBuilder.Entity<Issue>().HasOptional(s => s.Status).WithMany(p => p.Issues);
            modelBuilder.Entity<Issue>().HasOptional(s => s.Priority).WithMany(p => p.Issues);
            modelBuilder.Entity<Issue>().HasOptional(s => s.Type).WithMany(p => p.Issues);
            modelBuilder.Entity<TaskComment>().HasRequired(s => s.Issue).WithMany(p => p.Comments);
            modelBuilder.Entity<TaskComment>().HasRequired(s => s.Creator).WithMany(p => p.IssueComments);
            modelBuilder.Entity<TaskFile>().HasRequired(s => s.Issue).WithMany(p => p.Files);

            modelBuilder.Entity<Issue>().HasMany(p => p.Employees).
            WithMany(t => t.Issues).
            Map(t => t.MapLeftKey("IssueEmployeeId").
            MapRightKey("IssueId").
            ToTable("IssuesAssigees"));



            #endregion
        }

        #endregion


        #region Employee's entities

        // Employee.
        private void Set_Citizenship_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citizenship>().ToTable("Citizenships");
            modelBuilder.Entity<Citizenship>().HasKey(d => d.Id);
            modelBuilder.Entity<Citizenship>().Property(d => d.Name)
                .HasMaxLength(20).HasColumnName("Name").IsRequired();
        }

        private void Set_City_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<City>().HasKey(d => d.Id);
            modelBuilder.Entity<City>().Property(d => d.Name)
                .HasMaxLength(20).HasColumnName("Name").IsRequired();
        }

        private void Set_Country_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Country>().HasKey(d => d.Id);
            modelBuilder.Entity<Country>().Property(d => d.Name)
                .HasMaxLength(20).HasColumnName("Name").IsRequired();
        }
       
        private void Set_Employee_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Employee>().HasKey(d => d.Id);
            modelBuilder.Entity<Employee>().Property(d => d.Id).
                HasColumnOrder(1).HasColumnName("Id");

            modelBuilder.Entity<Employee>().Property(d => d.Name)
                .HasMaxLength(20).HasColumnOrder(2).HasColumnName("Name").IsRequired();
            modelBuilder.Entity<Employee>().Property(d => d.Surname)
                .HasMaxLength(24).HasColumnOrder(3).HasColumnName("Surname").IsRequired();
            modelBuilder.Entity<Employee>().Property(d => d.DateOfBirth)
                .HasColumnType("date").HasColumnOrder(4).HasColumnName("DateOfBirth").IsRequired();
            modelBuilder.Entity<Employee>().Property(d => d.Citizenship_Id)
                .HasColumnOrder(7).HasColumnName("Citizenship_Id").IsOptional();
            modelBuilder.Entity<Employee>().Property(d => d.Gender)
                .HasMaxLength(1).HasColumnOrder(8).HasColumnName("Gender").IsRequired();

            modelBuilder.Entity<Employee>().Property(d => d.Photo)
                .HasColumnOrder(9).HasColumnName("Photo").IsOptional();
            modelBuilder.Entity<Employee>().Property(d => d.Residence_Id)
                .HasColumnOrder(10).HasColumnName("Residence_Id").IsOptional();
            modelBuilder.Entity<Employee>().Property(d => d.GeneralDescription)
                .HasColumnType("ntext").HasColumnOrder(11).HasColumnName("GeneralDescription").IsOptional();
            modelBuilder.Entity<Employee>().Property(d => d.ProfessionalDescription)
                .HasColumnType("ntext").HasColumnOrder(12).HasColumnName("ProfessionalDescription").IsOptional();
            modelBuilder.Entity<Employee>().Property(d => d.Position_Id)
                .HasColumnOrder(13).HasColumnName("Position_Id").IsOptional();

            modelBuilder.Entity<Employee>().HasOptional(d => d.Account)
                .WithRequired(d => d.Employee)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Employee>().HasOptional(d => d.Citizenship)
                .WithMany(d => d.Employees).HasForeignKey(d => d.Citizenship_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>().HasOptional(d => d.CountryAndCity)
                .WithMany(d => d.Employees).HasForeignKey(d => d.Residence_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>().HasOptional(d => d.Position)
                .WithMany(d => d.Employees).HasForeignKey(d => d.Position_Id)
                .WillCascadeOnDelete(false);
        }

        private void Set_EmployeeAndMail_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeAndMail>().ToTable("EmployeesAndMails");
            modelBuilder.Entity<EmployeeAndMail>().HasKey(d => d.Mail);
            modelBuilder.Entity<EmployeeAndMail>().Property(d => d.Mail)
                .HasMaxLength(30).HasColumnName("Mail");

            modelBuilder.Entity<EmployeeAndMail>().HasRequired(d => d.Employee)
                .WithMany(d => d.EmployeesAndMails).HasForeignKey(d => d.Employee_Id)
                .WillCascadeOnDelete(true);
        }

        private void Set_EmployeeAndPhone_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeAndPhone>().ToTable("EmployeesAndPhones");
            modelBuilder.Entity<EmployeeAndPhone>().HasKey(d => d.PhoneNumber);
            modelBuilder.Entity<EmployeeAndPhone>().Property(d => d.PhoneNumber)
                .HasMaxLength(20).HasColumnName("PhoneNumber");

            modelBuilder.Entity<EmployeeAndPhone>().HasRequired(d => d.Employee)
                .WithMany(d => d.EmployeesAndPhones).HasForeignKey(d => d.Employee_Id)
                .WillCascadeOnDelete(true);
        }

        private void Set_EmployeeAndWeb_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeAndWeb>().ToTable("EmployeesAndWebs");
            modelBuilder.Entity<EmployeeAndWeb>().HasKey(d => new
            {
                d.WebService_Id,
                d.Account
            });
            modelBuilder.Entity<EmployeeAndWeb>().Property(d => d.Account)
                .HasMaxLength(20).HasColumnName("Account").IsRequired();

            modelBuilder.Entity<EmployeeAndWeb>().HasRequired(d => d.Employee)
                .WithMany(d => d.EmployeesAndWebs).HasForeignKey(d => d.Employee_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<EmployeeAndWeb>().HasRequired(d => d.WebService)
                .WithMany(d => d.EmployeesAndWebs).HasForeignKey(d => d.WebService_Id)
                .WillCascadeOnDelete(true);
        }
       
        private void Set_WebService_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WebService>().ToTable("WebServices");
            modelBuilder.Entity<WebService>().HasKey(d => d.Id);
            modelBuilder.Entity<WebService>().Property(d => d.Name)
                .HasMaxLength(20).HasColumnName("Name").IsRequired();
        }

        #endregion

        #region Many to many entities

        // Many to many entities.
        private void Set_EmployeeAndEducation_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeAndEducation>().ToTable("EmployeesAndEducations");
            modelBuilder.Entity<EmployeeAndEducation>().HasKey(d => new
            {
                d.Employee_Id,
                d.EducationEntitlingDocument_Id,
                d.EducationFacility_Id,
                d.EducationSpecialty_Id
            });
            modelBuilder.Entity<EmployeeAndEducation>().Property(d => d.GraduationDate)
                .HasColumnType("date").HasColumnName("GraduationDate").IsRequired();

            modelBuilder.Entity<EmployeeAndEducation>().HasRequired(d => d.Employee)
                .WithMany(d => d.EmployeesAndEducations).HasForeignKey(d => d.Employee_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<EmployeeAndEducation>().HasRequired(d => d.EducationEntitlingDocument)
                .WithMany(d => d.EmployeesAndEducations).HasForeignKey(d => d.EducationEntitlingDocument_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeAndEducation>().HasRequired(d => d.EducationFacility)
                .WithMany(d => d.EmployeesAndEducations).HasForeignKey(d => d.EducationFacility_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeAndEducation>().HasRequired(d => d.EducationSpecialty)
                .WithMany(d => d.EmployeesAndEducations).HasForeignKey(d => d.EducationSpecialty_Id)
                .WillCascadeOnDelete(false);
        }

        private void Set_EmployeeAndExperience_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeAndExperience>().ToTable("EmployeesAndExperiences");
            modelBuilder.Entity<EmployeeAndExperience>().HasKey(d => new
            {
                d.Employee_Id,
                d.Position_Id,
                d.HiringDate,
                d.Company_Id
            });

            modelBuilder.Entity<EmployeeAndExperience>().Property(d => d.HiringDate)
                .HasColumnType("date").HasColumnName("HiringDate").IsRequired();
            modelBuilder.Entity<EmployeeAndExperience>().Property(d => d.DismissalDate)
                .HasColumnType("date").HasColumnName("DismissalDate").IsOptional();

            modelBuilder.Entity<EmployeeAndExperience>().HasRequired(d => d.Employee)
                .WithMany(d => d.EmployeesAndExperiences).HasForeignKey(d => d.Employee_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<EmployeeAndExperience>().HasRequired(d => d.Position)
                .WithMany(d => d.EmployeesAndExperiences).HasForeignKey(d => d.Position_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeAndExperience>().HasRequired(d => d.Company)
                .WithMany(d => d.EmployeesAndExperiences).HasForeignKey(d => d.Company_Id)
                .WillCascadeOnDelete(false);
        }

        private void Set_EmployeeAndLanguage_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeAndLanguage>().ToTable("EmployeesAndLanguages");
            modelBuilder.Entity<EmployeeAndLanguage>().HasKey(d => new
            {
                d.Employee_Id,
                d.Language_Id
            });

            modelBuilder.Entity<EmployeeAndLanguage>().HasRequired(d => d.Employee)
                .WithMany(d => d.EmployeesAndLanguages).HasForeignKey(d => d.Employee_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<EmployeeAndLanguage>().HasRequired(d => d.Language)
                .WithMany(d => d.EmployeesAndLanguages).HasForeignKey(d => d.Language_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<EmployeeAndLanguage>().HasRequired(d => d.LanguageProficiency)
                .WithMany(d => d.EmployeesAndLanguages).HasForeignKey(d => d.LanguageProficiency_Id)
                .WillCascadeOnDelete(false);
        }

        private void Set_EmployeeAndSkill_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeAndSkill>().ToTable("EmployeesAndSkills");
            modelBuilder.Entity<EmployeeAndSkill>().HasKey(d => new
            {
                d.Employee_Id,
                d.Skill_Id
            });

            modelBuilder.Entity<EmployeeAndSkill>().HasRequired(d => d.Employee)
                .WithMany(d => d.EmployeesAndSkills).HasForeignKey(d => d.Employee_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<EmployeeAndSkill>().HasRequired(d => d.Skill)
                .WithMany(d => d.EmployeesAndSkills).HasForeignKey(d => d.Skill_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<EmployeeAndSkill>().HasRequired(d => d.SkillProficiency)
                .WithMany(d => d.EmployeesAndSkills).HasForeignKey(d => d.SkillProficiency_Id)
                .WillCascadeOnDelete(false);
        }

        private void Set_EmployeeAndProject_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeAndProject>().ToTable("EmployeesAndProjects");
            modelBuilder.Entity<EmployeeAndProject>().HasKey(d => new
            {
                d.Employee_Id,
                d.Project_Id
            });

            modelBuilder.Entity<EmployeeAndProject>().HasRequired(d => d.Employee)
                .WithMany(d => d.EmployeesAndProjects).HasForeignKey(d => d.Employee_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<EmployeeAndProject>().HasRequired(d => d.Project)
                .WithMany(d => d.EmployeesAndProjects).HasForeignKey(d => d.Project_Id)
                .WillCascadeOnDelete(true);
        }

        private void Set_CountryAndCity_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountryAndCity>().ToTable("CountriesAndCities");
            modelBuilder.Entity<CountryAndCity>().HasKey(d => d.Id);

            modelBuilder.Entity<CountryAndCity>().HasRequired(d => d.Country)
                .WithMany(d => d.CountriesAndCities).HasForeignKey(d => d.Country_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CountryAndCity>().HasRequired(d => d.City)
                .WithMany(d => d.CountriesAndCities).HasForeignKey(d => d.City_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CountryAndCity>().Property(d => d.Country_Id)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(
                    new IndexAttribute("IX_CountryAndCity", 1) { IsUnique = true }));

            modelBuilder.Entity<CountryAndCity>().Property(d => d.City_Id)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(
                    new IndexAttribute("IX_CountryAndCity", 2) { IsUnique = true }));
        }

        #endregion

        #region Other entities

        // Language single entity.
        private void Set_Language_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().ToTable("Languages");
            modelBuilder.Entity<Language>().HasKey(d => d.Id);
            modelBuilder.Entity<Language>().Property(d => d.Name)
                .HasMaxLength(20).HasColumnName("Name").IsRequired();
        }

        private void Set_LanguageProficiency_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LanguageProficiency>().ToTable("LanguageProficiencies");
            modelBuilder.Entity<LanguageProficiency>().HasKey(d => d.Id);
            modelBuilder.Entity<LanguageProficiency>().Property(d => d.Name)
                .HasMaxLength(30).HasColumnName("Name").IsRequired();
        }


        // Education single entity.
        private void Set_EducationFacility_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EducationFacility>().ToTable("EducationFacilities");
            modelBuilder.Entity<EducationFacility>().HasKey(d => d.Id);
            modelBuilder.Entity<EducationFacility>().Property(d => d.Name)
                .HasMaxLength(30).HasColumnName("Name").IsRequired();
        }

        private void Set_EducationSpecialty_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EducationSpecialty>().ToTable("EducationSpecialties");
            modelBuilder.Entity<EducationSpecialty>().HasKey(d => d.Id);
            modelBuilder.Entity<EducationSpecialty>().Property(d => d.Name)
                .HasMaxLength(30).HasColumnName("Name").IsRequired();
        }

        private void Set_EducationEntitlingDocument_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EducationEntitlingDocument>().ToTable("EducationEntitlingDocuments");
            modelBuilder.Entity<EducationEntitlingDocument>().HasKey(d => d.Id);
            modelBuilder.Entity<EducationEntitlingDocument>().Property(d => d.Name)
                .HasMaxLength(20).HasColumnName("Name").IsRequired();
        }


        // Account single entity.
        private void Set_AccessLevel_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessLevel>().ToTable("AccessLevels");
            modelBuilder.Entity<AccessLevel>().HasKey(d => d.Id);
            modelBuilder.Entity<AccessLevel>().Property(d => d.Name)
                .HasMaxLength(20).HasColumnName("Name").IsRequired();
        }

        private void Set_Account_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<Account>().HasKey(d => d.Employee_Id);
            modelBuilder.Entity<Account>().Property(d => d.Password)
                .HasMaxLength(20).HasColumnName("Password").IsRequired();
            modelBuilder.Entity<Account>().Property(d => d.Login)
                .HasMaxLength(20).HasColumnName("Login").IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(
                    new IndexAttribute("IX_Account") { IsUnique = true }));
            
            modelBuilder.Entity<Account>().HasRequired(d => d.AccessLevel)
                .WithMany(d => d.Accounts).HasForeignKey(d => d.AccessLevel_Id)
                .WillCascadeOnDelete(false);
        }
      

        // Projects association.
        //private void Set_Project_Config(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Project>().ToTable("Projects");
        //    modelBuilder.Entity<Project>().HasKey(d => d.Id);
        //    modelBuilder.Entity<Project>().Property(d => d.Name)
        //        .HasMaxLength(30).HasColumnName("Name").IsRequired();

        //    modelBuilder.Entity<Project>().HasRequired(d => d.ProjectStatus)
        //        .WithMany(d => d.Projects).HasForeignKey(d => d.ProjectStatus_Id)
        //        .WillCascadeOnDelete(false);
        //}

        // Skill.
        private void Set_SkillsGroup_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SkillsGroup>().ToTable("SkillsGroups");
            modelBuilder.Entity<SkillsGroup>().HasKey(d => d.Id);
            modelBuilder.Entity<SkillsGroup>().Property(d => d.Name)
                .HasMaxLength(30).HasColumnName("Name").IsRequired();

            modelBuilder.Entity<SkillsGroup>().HasRequired(d => d.Position)
                .WithMany(d => d.SkillsGroups).HasForeignKey(d => d.Position_Id)
                .WillCascadeOnDelete(true);
        }

        private void Set_SkillProficiency_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SkillProficiency>().ToTable("Proficiencies");
            modelBuilder.Entity<SkillProficiency>().HasKey(d => d.Id);
            modelBuilder.Entity<SkillProficiency>().Property(d => d.Name)
                .HasMaxLength(20).HasColumnName("Name").IsRequired();
        }

        private void Set_Skill_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().ToTable("Skills");
            modelBuilder.Entity<Skill>().HasKey(d => d.Id);
            modelBuilder.Entity<Skill>().Property(d => d.Name)
                .HasMaxLength(30).HasColumnName("Name").IsRequired();

            modelBuilder.Entity<Skill>().HasRequired(d => d.SkillsGroup)
                .WithMany(d => d.Skills).HasForeignKey(d => d.SkillsGroup_Id)
                .WillCascadeOnDelete(true);           
        }


        // Experiencce.
        private void Set_Company_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().ToTable("Companies");
            modelBuilder.Entity<Company>().HasKey(d => d.Id);
            modelBuilder.Entity<Company>().Property(d => d.Name)
                .HasMaxLength(30).HasColumnName("Name").IsRequired();
        }

        private void Set_Position_Config(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>().ToTable("Positions");
            modelBuilder.Entity<Position>().HasKey(d => d.Id);
            modelBuilder.Entity<Position>().Property(d => d.Name)
                .HasMaxLength(30).HasColumnName("Name").IsRequired();
        }

        #endregion
    }
}
