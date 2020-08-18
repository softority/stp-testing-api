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

        [HttpGet(nameof(GetCategories))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public List<TestCategoryDto> GetCategories()
        {
            var res = _db.TestCategories.Select(x => new TestCategoryDto()
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

            _db.TestCategories.Add(category);
            _db.SaveChanges();

            return category.Id;
        }

        [HttpPut(nameof(UpdateCategoryName))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategoryName(long categoryId, [FromBody]string name)
        {
            var category = _db.TestCategories.Find(categoryId);

            if (category == null)
            {
               return NotFound($"Test category with id={categoryId} doesn't exist");
            }

            category.Name = name;
            _db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Changes position of the category in the tree
        /// </summary>
        [HttpPut(nameof(MoveCategory))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult MoveCategory(MoveCategoryCommand cmd)
        {
            var categories = _db.TestCategories.Where(c => c.ParentId == cmd.ParentCategoryId).ToList().OrderBy(c => c.Position);
            var category = categories.FirstOrDefault(c => c.Id == cmd.CategoryId);

            if (category == null)
            {
                return NotFound($"Test category with id={cmd.CategoryId} and ParentId={cmd.ParentCategoryId} doesn't exist");
            }

            var maxPosition = Math.Max(category.Position, cmd.Position);
            var minPosition = Math.Min(category.Position, cmd.Position);
            category.Position = cmd.Position;
            int shift = cmd.Position == minPosition ? 1 : -1;
            categories.Where(c => (c.Position >= minPosition && c.Position <= maxPosition && c.Id != category.Id))
                .ToList()
                .ForEach(c => c.Position += shift);

            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete(nameof(DeleteCategory))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCategory(long categoryId)
        {
            // TODO: forbid deletion if category contains at least one test
            var category = _db.TestCategories.Find(categoryId);

            if (category == null)
            {
                return NotFound($"Test category with id={categoryId} doesn't exist");
            }

            category.IsDeleted = true;
            _db.SaveChanges();

            return NoContent();
        }
    }
}
