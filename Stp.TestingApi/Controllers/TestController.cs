using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stp.Data.Enums;

namespace Stp.TestingApi.Controllers
{
    public class TestDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public TestStatus Status { get; set; }
        public List<string> Skills { get; set; }
        public int DurationMinutes { get; set; }
        public int TasksCount { get; set; }
        public List<TestSectionDto> Sections { get; set; }
    }
    public class TestSectionDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public int DurationMinutes { get; set; }
        public int TasksCount { get; set; }
        public List<TaskDto> Tasks { get; set; }
    }
    public class AddTestCommand
    {
        public long TestCategoryId { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
    }
    public class AddTaskCommand
    {
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

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet(nameof(GetTestById))]
        public List<TestDto> GetTestById(long testId)
        {
            throw new NotImplementedException();
        }

        [HttpPost(nameof(AddTest))]
        public long AddTest(AddTestCommand cmd)
        {
            throw new NotImplementedException();
        }

        [HttpPost(nameof(AddTest))]
        public long AddTask(AddTaskCommand cmd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a task from specified test section (a task itself is not deletes)
        /// </summary>
        [HttpPut(nameof(AddTest))]
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
        public IActionResult UpdateTestName(long testId, [FromBody]string name)
        {
            throw new NotImplementedException();
        }

        [HttpDelete(nameof(DeleteTest))]
        public IActionResult DeleteTest(long testId)
        {
            throw new NotImplementedException();
        }
    }
}
