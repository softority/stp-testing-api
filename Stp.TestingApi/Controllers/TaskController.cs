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

        /* https://localhost:5001/api/Task/GetTasksByCategory */
        [HttpGet(nameof(GetTasksByCategory))]
        public IEnumerable<TaskDto> GetTasksByCategory(long taskCategoryId)
        {
            var res = _db.TaskList
                .Where(x => x.TaskCategoryId == taskCategoryId)
                .Include(x => x.MultichoiceAnswers)
                .Select(x => new TaskDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Points = x.Points,
                    DurationMinutes = x.DurationMinutes, 
                    Type = x.Type,
                    Complexity = x.Complexity,
                    MultichoiceAnswers = x.MultichoiceAnswers.Select(a =>  new MultichoiceAnswerDto() 
                        {
                            Id = a.Id,
                            TaskId = a.TaskId,
                            IsCorrect = a.IsCorrect,
                            Name = a.Name                            
                        }).ToList()
                });       

            return res;
        }

        /* https://localhost:5001/api/Task/AddTask */
        [HttpPost(nameof(AddTask))]
        public TaskDto AddTask(long taskCategoryId, [FromBody]TaskDto task)
        {
            StpTask newTask = new StpTask() 
            {
                TaskCategoryId = taskCategoryId,
                Name = task.Name,
                Description = task.Description,
                Points = task.Points,
                DurationMinutes = task.DurationMinutes,
                Type = task.Type,
                Complexity = task.Complexity
            };

            _db.TaskList.Add(newTask);
            _db.SaveChanges();

            task.Id = task.Id;

            return task;
        }

        [HttpPut(nameof(UpdateTaskName))]
        /* https://localhost:5001/api/Task/UpdateTaskName */
        public IActionResult UpdateTaskName(long taskId, [FromBody]string name)
        {
            StpTask task = _db.TaskList.Find(taskId);

            if (task == null)
            {
                return BadRequest($"Task with id={taskId} not found");
            }

            task.Name = name;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut(nameof(UpdateTaskDuration))]
        /* https://localhost:5001/api/Task/UpdateTaskDuration */
        public IActionResult UpdateTaskDuration(long taskId, [FromBody]int duration)
        {
            StpTask task = _db.TaskList.Find(taskId);

            if (task == null)
            {
                return BadRequest($"Task with id={taskId} not found");
            }

            task.DurationMinutes = duration;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut(nameof(UpdateTaskPoints))]
        /* https://localhost:5001/api/Task/UpdateTaskPoints */
        public IActionResult UpdateTaskPoints(long taskId, [FromBody]int points)
        {
            StpTask task = _db.TaskList.Find(taskId);

            if (task == null)
            {
                return BadRequest($"Task with id={taskId} not found");
            }

            task.Points = points;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut(nameof(UpdateTaskComplexity))]
        /* https://localhost:5001/api/Task/UpdateTaskComplexity */
        public IActionResult UpdateTaskComplexity(long taskId, [FromBody] Data.Entities.TaskComplexity complexity)
        {
            StpTask task = _db.TaskList.Find(taskId);

            if (task == null)
            {
                return BadRequest($"Task with id={taskId} not found");
            }

            task.Complexity = complexity;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut(nameof(UpdateTaskDescription))]
        /* https://localhost:5001/api/Task/UpdateTaskDescription */
        public IActionResult UpdateTaskDescription(long taskId, [FromBody]string description)
        {
            StpTask task = _db.TaskList.Find(taskId);

            if (task == null)
            {
                return BadRequest($"Task with id={taskId} not found");
            }

            task.Description = description;
            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete(nameof(DeleteTask))]
        /* https://localhost:5001/api/Task/DeleteTask */
        public IActionResult DeleteTask(long taskId)
        {
            StpTask task = _db.TaskList.Find(taskId);

            if (task == null)
            {
                return BadRequest($"Task with id={taskId} not found");
            }

            task.IsDeleted = true;
            _db.SaveChanges();

            return Ok();
        }

    }
}
