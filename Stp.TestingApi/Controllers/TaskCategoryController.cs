using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stp.TestingApi.Contracts;

namespace Stp.TestingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCategoryController : ControllerBase
    {
        /* https://localhost:5001/api/TestCategory/GetCategories */
        [HttpGet(nameof(GetCategories))]
        public List<TaskCategoryDto> GetCategories()
        {
            throw new NotImplementedException();
        }

        /// <returns>Id of just added category</returns>
        [HttpPost(nameof(AddCategory))]
        public long AddCategory(AddCategoryCommand cmd)
        {
            throw new NotImplementedException();
        }

        [HttpPut(nameof(UpdateCategoryName))]
        public IActionResult UpdateCategoryName(long categoryId, [FromBody] string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Changes position of the category in the tree
        /// </summary>
        [HttpPut(nameof(MoveCategory))]
        public IActionResult MoveCategory(MoveCategoryCommand cmd)
        {
            throw new NotImplementedException();
        }

        [HttpDelete(nameof(DeleteCategory))]
        public IActionResult DeleteCategory(long categoryId)
        {
            // TODO: forbid deletion if category contains at least one test

            throw new NotImplementedException();
        }
    }
}
