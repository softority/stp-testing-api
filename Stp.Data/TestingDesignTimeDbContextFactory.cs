using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stp.Data
{
    public class TestingDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TestingDbContext>
    {
        public TestingDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestingDbContext>();
            optionsBuilder.UseNpgsql("Server=localhost; Port=5432; User Id=postgres; Password=123qweasd; Database=testing", options => options.CommandTimeout(3600));
            return new TestingDbContext(optionsBuilder.Options);
        }
    }
}
