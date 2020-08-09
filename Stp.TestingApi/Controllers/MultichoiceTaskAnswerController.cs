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
    public class MultichoiceTaskAnswerController : ControllerBase
    {
        private readonly TestingDbContext _db;
        public MultichoiceTaskAnswerController(TestingDbContext db)
        {
            _db = db;
        }

        /* https://localhost:5001/api/MultichoiceTaskAnswer/AddTaskAnswer */
        [HttpPost(nameof(AddTaskAnswer))]
        public ActionResult<MultichoiceTaskAnswerDto> AddTaskAnswer(long taskId, [FromBody]MultichoiceTaskAnswerDto answerDto)
        {
            StpTask task = _db.TaskList.Find(taskId);

            if(task == null)
            {
                return null;
            }

            MultichoiceTaskAnswer answer = new MultichoiceTaskAnswer()
            {
                Name = answerDto.Name,
                IsCorrect = answerDto.IsCorrect,
                TaskId = taskId
            };

            _db.MultichoiceAnswerList.Add(answer);
            _db.SaveChanges();

            answerDto.Id = answer.Id;

            return CreatedAtAction(nameof(AddTaskAnswer), answerDto);
        }

        /* https://localhost:5001/api/MultichoiceTaskAnswer/UpdateTaskAnswer */
        [HttpPut(nameof(UpdateTaskAnswer))]
        public IActionResult UpdateTaskAnswer(long answerId, [FromBody]MultichoiceTaskAnswerDto answerDto)
        {
            MultichoiceTaskAnswer answer = _db.MultichoiceAnswerList.Find(answerId);

            if (answer == null)
            {
                return BadRequest($"Answer with id={answerId} not found");
            }

            answer.IsCorrect = answerDto.IsCorrect;
            answer.Name = answerDto.Name;

            _db.SaveChanges();

            return Ok();
        }

        /* https://localhost:5001/api/MultichoiceTaskAnswer/DeleteTaskAnswer */
        [HttpDelete(nameof(DeleteTaskAnswer))]
        public IActionResult DeleteTaskAnswer(long answerId)
        {
            MultichoiceTaskAnswer answer = _db.MultichoiceAnswerList.Find(answerId);

            if (answer == null)
            {
                return BadRequest($"Answer with id={answerId} not found");
            }

            answer.IsDeleted = true;

            _db.SaveChanges();

            return NoContent();
        }

    }
    
}
