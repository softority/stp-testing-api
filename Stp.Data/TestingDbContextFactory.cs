using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stp.Data
{
    public class TestingDbContextFactory
    {
        public TestingDbContextFactory()
        {

        }

        public TestingDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestingDbContext>();
            // TODO: <!!> move to config
            optionsBuilder.UseNpgsql("Server=localhost; Port=5432; User Id=postgres; Password=postgres; Database=testing");

            return new TestingDbContext(optionsBuilder.Options);
        }
    }
}
