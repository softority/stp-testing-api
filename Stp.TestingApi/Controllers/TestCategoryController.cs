using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stp.Data;
using Stp.Data.Entities;
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
        public List<TestCategoryDto> GetCategories()
        {
            var res = _db.TestCategoryList.Select(x => new TestCategoryDto()
            {
                Id = x.Id,
                Name = x.Name,
                ParentId = x.ParentId,
                Position = x.Position,
                Tests = new List<TestSummaryDto>()
            }).ToList();

            return res;
        }

        /// <returns>Id of just added category</returns>
        [HttpPost(nameof(CreateCategory))]
        public long CreateCategory(CreateCategoryCommand cmd)
        {
            if (cmd == null)
            {
                return -1;
            }

            var category = new TestCategory()
            {
                Name = cmd.Name,
                ParentId = cmd.ParentCategoryId
            };

            _db.TestCategoryList.Add(category);
            _db.SaveChanges();

            return category.Id;
        }

        [HttpPut(nameof(UpdateCategoryName))]
        public IActionResult UpdateCategoryName(long categoryId, [FromBody]string name)
        {
            var category = _db.TestCategoryList.Find(categoryId);

            if (category == null)
            {
               return BadRequest($"Test category with id={categoryId} not found");
            }

            category.Name = name;
            _db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Changes position of the category in the tree
        /// </summary>
        [HttpPut(nameof(MoveCategory))]
        public IActionResult MoveCategory(MoveCategoryCommand cmd)
        {
            var category = _db.TestCategoryList.Find(cmd.CategoryId);

            if (category == null)
            {
                return BadRequest($"Test category with id={cmd.CategoryId} not found");
            }

            category.Position = cmd.Position;
            category.ParentId = cmd.ParentCategoryId;
            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete(nameof(DeleteCategory))]
        public IActionResult DeleteCategory(long categoryId)
        {
            // TODO: forbid deletion if category contains at least one test
            var category = _db.TestCategoryList.Find(categoryId);

            if (category == null)
            {
                return BadRequest($"Test category with id={categoryId} not found");
            }

            category.IsDeleted = true;
            _db.SaveChanges();

            return NoContent();
        }
    }
}
