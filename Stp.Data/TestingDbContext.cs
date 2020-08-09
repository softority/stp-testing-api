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

            modelBuilder.Entity<TaskAndSkill>()
                .HasKey(ts => new { ts.TaskId, ts.SkillId });
            modelBuilder.Entity<TaskAndSkill>()
                .HasOne(t => t.Task)
                .WithMany(ts => ts.TaskAndSkills)
                .HasForeignKey(ts => ts.TaskId);
            modelBuilder.Entity<TaskAndSkill>()
                .HasOne(s => s.Skill)
                .WithMany(ts => ts.TaskAndSkills)
                .HasForeignKey(ts => ts.SkillId);
        }

        public DbSet<TestCategory> TestCategoryList { get; set; }
        public DbSet<TaskCategory> TaskCategoryList { get; set; }
        public DbSet<StpTask> TaskList { get; set; }
        public DbSet<MultichoiceTaskAnswer> MultichoiceAnswerList { get; set; }
        public DbSet<StpSkill> Skills { get; set; }
        public DbSet<TaskAndSkill> TaskAndSkills { get; set; }

    }
}
