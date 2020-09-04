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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestSectionController : ControllerBase
    {
        private readonly TestingDbContext _db;
        public TestSectionController(TestingDbContext db)
        {
            _db = db;
        }

        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TestSectionDto> AddTestSection([FromBody]CreateTestSectionCommand cmd)
        {
            // TODO: Check presences the section in specified test
            TestSection testSection = new TestSection()
            {
                Name = cmd.Name,
                Position = cmd.Position,
                TestId = cmd.TestId                
            };

            _db.TestSections.Add(testSection);
            _db.SaveChanges();

            var result = new TestSectionDto()
            {
                Id = testSection.Id,
                Name = testSection.Name,
                Position = testSection.Position,                
            };

            return Created(nameof(AddTestSection), result);
        }

        [HttpPut("{sectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateSectionPosition(long sectionId, [FromBody]int newPosition)
        {
            TestSection section = _db.TestSections.Find(sectionId);

            if (section == null)
            {
                return NotFound($"TestSection with id={sectionId} doesn't exist");
            }

            var sections = _db.TestSections.Where(s => s.TestId == section.TestId).ToList().OrderBy(s => s.Position);

            if (sections.Count() == 0)
            {
                return NotFound($"TestSection with id={sectionId} and TestId={section.TestId} doesn't exist");
            }

            if (newPosition > sections.Max(s => s.Position) || newPosition < sections.Min(s => s.Position))
            {
                return BadRequest($"New position newPosition={newPosition} is out of positions range");
            }

            var maxPosition = Math.Max(section.Position, newPosition);
            var minPosition = Math.Min(section.Position, newPosition);
            section.Position = newPosition;
            int shift = newPosition == minPosition ? 1 : -1;
            sections.Where(s => (s.Position >= minPosition && s.Position <= maxPosition && s.Id != section.Id))
                .ToList()
                .ForEach(c => c.Position += shift);

            _db.SaveChanges();

            return Ok();
        }

        [HttpPut("{sectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddTask(long sectionId, [FromBody]long taskId)
        {
            TestSection section = _db.TestSections.Where(s => s.Id == sectionId)
                                    .Include(s => s.TestSectionsAndTasks).FirstOrDefault();

            if (section == null)
            {
                return NotFound($"TestSection with id={sectionId} doesn't exist");
            }

            StpTask task = _db.Tasks.Find(taskId);

            if (task == null)
            {
                return NotFound($"Task with id={taskId} doesn't exist");
            }

            if(section.TestSectionsAndTasks.FirstOrDefault(t => t.TaskId == task.Id) != null)
            {
                return BadRequest($"Task with id={taskId} is already present in TestSection with id={sectionId}");
            }

            _db.TestSectionAndTasks.Add(new TestSectionAndTask()
            {
                TaskId = taskId,
                TaskPosition = section.TestSectionsAndTasks.Count(),
                TestSectionId = sectionId
            });

            _db.SaveChanges();

            return Ok();
        }

        [HttpPut("{sectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateSectionName(long sectionId, [FromBody]string name)
        {
            TestSection section = _db.TestSections.Find(sectionId);

            if (section == null)
            {
                return NotFound($"TestSection with id={sectionId} doesn't exist");
            }

            section.Name = name;
            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete("{sectionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteSection(long sectionId)
        {
            TestSection section = _db.TestSections.Find(sectionId);

            if (section == null)
            {
                return NotFound($"TestSection with id={sectionId} doesn't exist");
            }

            section.IsDeleted = true;
            _db.SaveChanges();

            return NoContent();
        }
    }
}
