using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
    public class TaskCategoryController : ControllerBase
    {
        private readonly TestingDbContext _db;

        public TaskCategoryController(TestingDbContext db)
        {
            _db = db;
        }

        [HttpGet(nameof(GetCategories))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<TaskCategoryDto>> GetCategories()
        {
            var res = _db.TaskCategoryList
                .Select(x => new TaskCategoryDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    Position = x.Position
                })
                .OrderBy(x => x.Id) // TODO: OrderBy Position
                .ToList();

            return res;
        }

        /// <returns>Id of just added category</returns>
        [HttpPost(nameof(CreateCategory))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TaskCategoryDto> CreateCategory([FromBody] CreateCategoryCommand cmd)
        {
            // TODO: Validation
            if (cmd.ParentCategoryId != null)
            {
                var res = _db.TaskCategoryList.Find(cmd.ParentCategoryId.Value);
                if (res == null)
                {
                    return BadRequest($"ParentCategoryId={cmd.ParentCategoryId} doesn't exist");
                }
            }

            var maxPos = _db.TaskCategoryList.Max(x => (int?)x.Position) ?? 0;

            var newCategory = new TaskCategory()
            {
                Name = cmd.Name,
                ParentId = cmd.ParentCategoryId,
                Position = maxPos > 0 ? ++maxPos : 0
            };
            _db.TaskCategoryList.Add(newCategory);
            _db.SaveChanges();

            return CreatedAtAction(
                nameof(CreateCategory),
                new TaskCategoryDto
                {
                    Id = newCategory.Id,
                    Name = newCategory.Name,
                    ParentId = newCategory.ParentId,
                    Position = newCategory.Position
                });
        }

        [HttpPut("UpdateCategoryName/{categoryId}")] // TODO: <??>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategoryName(long categoryId, [FromBody] string name)
        {
            var category = _db.TaskCategoryList.Find(categoryId);
            if (category == null)
            {
                return NotFound($"Category with Id={categoryId} doesn't exist");
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
            throw new NotImplementedException();
        }

        [HttpDelete("DeleteCategory/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCategory(long categoryId)
        {
            // TODO: forbid deletion if category contains at least one test
            var category = _db.TaskCategoryList
                .Include(x => x.Tasks)
                .FirstOrDefault(x => x.Id == categoryId);

            if (category == null)
            {
                return NotFound($"Category with Id={categoryId} doesn't exist");
            }

            var hasChildren = _db.TaskCategoryList.Any(x => x.ParentId == category.Id);
            if (hasChildren)
            {
                return BadRequest($"Deletion is forbidden. Category contains nested categories.");
            }

            if (category.Tasks.Count > 0)
            {
                return BadRequest($"Deletion is forbidden. Category contains tasks.");
            }

            _db.TaskCategoryList.Remove(category);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
