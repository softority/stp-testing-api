using Microsoft.EntityFrameworkCore;
using Stp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stp.Data
{
    // cd <path to Stp.Data folder>
    // dotnet ef migrations add <Migration name> -o Migrations
    // dotnet ef database update

    // to remove last added migration:
    // dotnet ef migrations remove

    public class TestingDbContext : DbContext
    {
        public TestingDbContext(DbContextOptions<TestingDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestCategory>().HasQueryFilter(x => EF.Property<bool>(x, "IsDeleted") == false);
            modelBuilder.Entity<TaskCategory>().HasQueryFilter(x => EF.Property<bool>(x, "IsDeleted") == false);
            modelBuilder.Entity<StpTask>().HasQueryFilter(x => EF.Property<bool>(x, "IsDeleted") == false);
            modelBuilder.Entity<MultichoiceTaskAnswer>().HasQueryFilter(x => EF.Property<bool>(x, "IsDeleted") == false);
            modelBuilder.Entity<TestSection>().HasQueryFilter(x => EF.Property<bool>(x, "IsDeleted") == false);            
        }

        public DbSet<TestCategory> TestCategories { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }
        public DbSet<StpTask> Tasks { get; set; }
        public DbSet<MultichoiceTaskAnswer> MultichoiceTaskAnswers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<TaskAndSkill> TaskAndSkills { get; set; }
        public DbSet<TestSection> TestSections { get; set; }
        public DbSet<TestSectionAndTask> TestSectionAndTasks { get; set; }

    }
}
