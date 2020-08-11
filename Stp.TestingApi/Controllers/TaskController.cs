using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stp.Data;
using Stp.Data.Entities;
using Stp.TestingApi.Contracts;
using Microsoft.EntityFrameworkCore;
using Stp.Data.Enums;
using System.Net.Mime;

namespace Stp.TestingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TestingDbContext _db;
        public TaskController(TestingDbContext db)
        {
            _db = db;
        }

        [HttpGet(nameof(GetTasksByCategory))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<TaskDto> GetTasksByCategory(long taskCategoryId)
        {
            var res = _db.TaskList
                .Where(x => x.CategoryId == taskCategoryId)
                .Include(x => x.MultichoiceAnswers)
                .Include(x => x.TaskAndSkills)
                .ThenInclude(x => x.Skill)
                .Select(x => new TaskDto()
                {
                    TaskSummary = new TaskSummaryDto()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Type = x.Type,
                        Points = x.Points,
                        DurationMinutes = x.DurationMinutes,
                        Complexity = x.Complexity,
                        Position = x.Position,
                        Skills = x.TaskAndSkills.Select(ts => new SkillDto()
                        {
                            Id = ts.Skill.Id,
                            Name = ts.Skill.Name
                        }).ToList()
                    },

                    MultichoiceTaskInfo = new MultichoiceTaskInfoDto()
                    {
                        Question = String.Empty,
                        Answers = x.MultichoiceAnswers.Select(a => new MultichoiceTaskAnswerDto()
                        {
                            Id = a.Id,
                            Name = a.Name,
                            IsCorrect = a.IsCorrect
                        }).ToList()
                    },                    

                    CodingTaskInfo = new CodingTaskInfoDto()
                });     

            return res;
        }

        [HttpPost(nameof(AddTask))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        public ActionResult<TaskDto> AddTask(long taskCategoryId, [FromBody] TaskDto task)
        {

            if (task.TaskSummary == null)
            {
                return BadRequest($"Invalid request body");
            }

            StpTask newTask = new StpTask() 
            {
                CategoryId = taskCategoryId,
                Name = task.TaskSummary.Name,          
                Points = task.TaskSummary.Points,
                DurationMinutes = task.TaskSummary.DurationMinutes,
                Type = task.TaskSummary.Type,
                Complexity = task.TaskSummary.Complexity
            };

            _db.TaskList.Add(newTask);
            _db.SaveChanges();

            task.TaskSummary.Id = newTask.Id;

            return Created(nameof(AddTask), task);
        }

        [HttpPut(nameof(UpdateTaskName))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTaskName(long taskId, [FromBody]string name)
        {
            StpTask task = _db.TaskList.Find(taskId);

            if (task == null)
            {
                return NotFound($"Task with id={taskId} doesn't exist");
            }

            task.Name = name;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut(nameof(UpdateTaskDuration))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTaskDuration(long taskId, [FromBody]int duration)
        {
            StpTask task = _db.TaskList.Find(taskId);

            if (task == null)
            {
                return NotFound($"Task with id={taskId} doesn't exist");
            }

            task.DurationMinutes = duration;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut(nameof(UpdateTaskPoints))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTaskPoints(long taskId, [FromBody]int points)
        {
            StpTask task = _db.TaskList.Find(taskId);

            if (task == null)
            {
                return NotFound($"Task with id={taskId} doesn't exist");
            }

            task.Points = points;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut(nameof(UpdateTaskComplexity))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTaskComplexity(long taskId, [FromBody] TaskComplexity complexity)
        {
            StpTask task = _db.TaskList.Find(taskId);

            if (task == null)
            {
                return NotFound($"Task with id={taskId} doesn't exist");
            }

            task.Complexity = complexity;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut(nameof(UpdateTaskDescription))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTaskDescription(long taskId, [FromBody]string description)
        {
            StpTask task = _db.TaskList.Find(taskId);

            if (task == null)
            {
                return NotFound($"Task with id={taskId} doesn't exist");
            }

            task.Description = description;
            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete(nameof(DeleteTask))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTask(long taskId)
        {
            StpTask task = _db.TaskList.Find(taskId);

            if (task == null)
            {
                return NotFound($"Task with id={taskId} doesn't exist");
            }

            task.IsDeleted = true;
            _db.SaveChanges();

            return NoContent();
        }

    }
}
