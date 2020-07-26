using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stp.Data;
using Stp.TestingApi.Contracts;

namespace Stp.TestingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestCategoryController : ControllerBase
    {
        private readonly TestingDbContext _db;
        public TestCategoryController(TestingDbContext db)
        {
            _db = db;
        }

        /* https://localhost:5001/api/TestCategory/GetCategories */
        [HttpGet(nameof(GetCategories))]
        public IEnumerable<TestCategoryDto> GetCategories()
        {
            var res = _db.TestCategoryList
                .ToList()
                .Select(x => new TestCategoryDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    Position = x.Position
                });

            return res;
        }
    }
}
