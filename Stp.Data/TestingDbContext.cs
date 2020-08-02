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

    public class TestingDbContext : DbContext
    {
        public TestingDbContext(DbContextOptions<TestingDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestCategory>().HasQueryFilter(x => EF.Property<bool>(x, "IsDeleted") == false);
            modelBuilder.Entity<Exercise>().HasQueryFilter(x => EF.Property<bool>(x, "IsDeleted") == false);
            modelBuilder.Entity<MultichoiceExerciseAnswer>().HasQueryFilter(x => EF.Property<bool>(x, "IsDeleted") == false);
        }

        public DbSet<TestCategory> TestCategoryList { get; set; }
        public DbSet<Exercise> ExerciseList { get; set; }
        public DbSet<MultichoiceExerciseAnswer> MultichoiceAnswerList { get; set; }

}
}
