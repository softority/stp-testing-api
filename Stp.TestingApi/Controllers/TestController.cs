using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Validation;
using Stp.Data;
using Stp.Data.Entities;
using Stp.Data.Enums;
using Stp.TestingApi.Contracts;

namespace Stp.TestingApi.Controllers
{
    public class TestDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public TestStatus Status { get; set; }
        public List<string>? Skills { get; set; }
        public int DurationMinutes { get; set; }
        public int TasksCount { get; set; }
        public List<TestSectionDto>? Sections { get; set; }
    }
    public class TestSectionDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public int Position { get; set; }
        public int DurationMinutes { get; set; }
        public int TasksCount { get; set; }
        public List<TaskDto>? Tasks { get; set; }
    }
    public class CreateTestCommand
    {
        /// <summary>
        /// Id of the test category to which the test is being added
        /// </summary>
        public long TestCategoryId { get; set; }
        public string? Name { get; set; }
        public int Position { get; set; }
        public string Description { get; set; }
    }
    public class AddTaskCommand
    {
        /// <summary>
        /// Id of the existing task which is about to add
        /// </summary>
        public long TaskId { get; set; }

        /// <summary>
        /// Id of the test section to which task should be added
        /// </summary>
        public int SectionId { get; set; }
    }
    public class UpdateTaskPositionCommand
    {
        public long TaskId { get; set; }
        public int SectionId { get; set; }
        public int TaskPosition { get; set; }        
    }

    public class RemoveTaskCommand
    {
        public long TaskId { get; set; }
        public int SectionId { get; set; }
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestingDbContext _db;

        public TestController(TestingDbContext db)
        {
            _db = db;
        }

        [HttpGet(nameof(GetTestById))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TestDto> GetTestById(long testId)
        {
            var test = _db.Tests.Find(testId);

            if (test == null)
            {
                return NotFound($"Test with id={testId} doesn't exist");
            }

            var res = new TestDto()
            {
                Id = test.Id,
                Name = test.Name,
                Status = test.Status,
                Sections = _db.TestSections.Where(x => x.TestId == testId).Select(x => new TestSectionDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    //DurationMinutes
                    Position = x.Position,
                    //Tasks
                    //TasksCount
                }).ToList()
            };

            return res;            
        }

        /// <returns>Id of the added test</returns>
        [HttpPost(nameof(CreateTest))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public long CreateTest(CreateTestCommand cmd)
        {
            var category = _db.TaskCategories.Find(cmd.TestCategoryId);

            if (category == null)
            {
                //return NotFound($"Test category with id={cmd.TestCategoryId} doesn't exist");
            }

            var newTest = new Test()
            {
                Name = cmd.Name,
                Description = cmd.Description,
                Status = TestStatus.Created                                
            };

            _db.Tests.Add(newTest);
            _db.SaveChanges();

            return newTest.Id;
        }

        [HttpDelete(nameof(DeleteTest))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTest(long testId)
        {
            var test = _db.Tests.Find(testId);

            if (test == null)
            {
                return NotFound($"Test with id={testId} doesn't exist");
            }

            test.IsDeleted = true;
            _db.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Add existing task to the test
        /// </summary>
        [HttpPut(nameof(AddTask))]
        public IActionResult AddTask(AddTaskCommand cmd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the task from specified test section (the task itself is not deletes)
        /// </summary>
        [HttpPut(nameof(RemoveTask))]
        public IActionResult RemoveTask(RemoveTaskCommand cmd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates position of the task within specified test section
        /// </summary>
        [HttpPut(nameof(UpdateTaskPosition))]
        public IActionResult UpdateTaskPosition(UpdateTaskPositionCommand cmd)
        {
            throw new NotImplementedException();
        }

        [HttpPut(nameof(UpdateTestName))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTestName(long testId, [FromBody]string name)
        {
            var test = _db.Tests.Find(testId);

            if (test == null)
            {
                return NotFound($"Test with id={testId} doesn't exist");
            }

            test.Name = name;
            _db.SaveChanges();

            return Ok();
        }        
    }
}
