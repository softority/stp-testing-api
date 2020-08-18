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
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Stp.TestingApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TestingDbContext _db;
        public TaskController(TestingDbContext db)
        {
            _db = db;
        }

        // TODO: <??> Move taskCategoryId param to query string
        [HttpGet("{taskCategoryId}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<TaskDto> GetTasksByCategory(long taskCategoryId)
        {
            var res = _db.Tasks
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
                }); ;     
            
            return res;
        }

        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        public ActionResult<TaskDto> CreateTask([FromBody] CreateTaskCommand cmd)
        {
            var category = _db.TaskCategories.Find(cmd.TaskCategoryId);
            if (category == null) 
            {
                return BadRequest($"Task category with Id={cmd.TaskCategoryId} not found");
            }

            StpTask newTask = new StpTask() 
            {
                CategoryId = category.Id,
                Name = cmd.Name,          
                Points = cmd.Points,
                DurationMinutes = cmd.DurationMinutes,
                Type = cmd.Type,
                Complexity = cmd.Complexity
            };

            _db.Tasks.Add(newTask);
            _db.SaveChanges();

            var result = new TaskDto()
            {
                TaskSummary = new TaskSummaryDto()
                {
                    Id = newTask.Id,
                    Complexity = newTask.Complexity,
                    DurationMinutes = newTask.DurationMinutes,
                    Name = newTask.Name,
                    Points = newTask.Points,
                    Position = newTask.Position,
                    Skills = new List<SkillDto>(),
                    Type = newTask.Type
                },
                MultichoiceTaskInfo = new MultichoiceTaskInfoDto()
                {
                    Question = "",
                    Answers = new List<MultichoiceTaskAnswerDto>()
                }
            };

            return Created(nameof(CreateTask), result);
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTaskName(long taskId, [FromBody]string name)
        {
            StpTask task = _db.Tasks.Find(taskId);

            if (task == null)
            {
                return NotFound($"Task with id={taskId} doesn't exist");
            }

            task.Name = name;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTaskDuration(long taskId, [FromBody]int duration)
        {
            StpTask task = _db.Tasks.Find(taskId);

            if (task == null)
            {
                return NotFound($"Task with id={taskId} doesn't exist");
            }

            task.DurationMinutes = duration;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTaskPoints(long taskId, [FromBody]int points)
        {
            StpTask task = _db.Tasks.Find(taskId);

            if (task == null)
            {
                return NotFound($"Task with id={taskId} doesn't exist");
            }

            task.Points = points;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTaskPosition(long taskId, [FromBody] int position)
        {
            StpTask task = _db.Tasks.Find(taskId);

            if (task == null)
            {
                return NotFound($"Task with id={taskId} doesn't exist");
            }

            var tasks = _db.Tasks.Where(x => x.CategoryId == task.CategoryId).ToList();

            var maxPosition = Math.Max(task.Position, position);
            var minPosition = Math.Min(task.Position, position);

            task.Position = position;

            int shift = position == minPosition ? 1 : -1;
            _db.Tasks
                .Where(x => (x.CategoryId == task.CategoryId && x.Position >= minPosition && x.Position <= maxPosition))
                .ToList()
                .ForEach(c => c.Position += shift);

            _db.SaveChanges();

            return Ok();
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTaskComplexity(long taskId, [FromBody] TaskComplexity complexity)
        {
            StpTask task = _db.Tasks.Find(taskId);

            if (task == null)
            {
                return NotFound($"Task with id={taskId} doesn't exist");
            }

            task.Complexity = complexity;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTaskDescription(long taskId, [FromBody]string description)
        {
            StpTask task = _db.Tasks.Find(taskId);

            if (task == null)
            {
                return NotFound($"Task with id={taskId} doesn't exist");
            }

            task.Description = description;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTask(long taskId)
        {
            StpTask task = _db.Tasks.Find(taskId);

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
