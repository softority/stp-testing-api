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

        public DbSet<TestCategory> TestCategoryList { get; set; }
    }
}
