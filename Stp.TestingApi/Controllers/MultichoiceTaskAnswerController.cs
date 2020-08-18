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
using System.Net.Mime;

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

        [HttpPost(nameof(AddTaskAnswer))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MultichoiceTaskAnswerDto> AddTaskAnswer(long taskId, [FromBody]MultichoiceTaskAnswerDto answerDto)
        {
            StpTask task = _db.Tasks.Find(taskId);

            if(task == null)
            {
                return NotFound($"Answer with id={taskId} doesn't exist");
            }

            MultichoiceTaskAnswer answer = new MultichoiceTaskAnswer()
            {
                Name = answerDto.Name,
                IsCorrect = answerDto.IsCorrect,
                TaskId = taskId
            };

            _db.MultichoiceTaskAnswers.Add(answer);
            _db.SaveChanges();

            answerDto.Id = answer.Id;

            return CreatedAtAction(nameof(AddTaskAnswer), answerDto);
        }

        [HttpPut(nameof(UpdateTaskAnswer))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTaskAnswer(long answerId, [FromBody]MultichoiceTaskAnswerDto answerDto)
        {
            MultichoiceTaskAnswer answer = _db.MultichoiceTaskAnswers.Find(answerId);

            if (answer == null)
            {
                return NotFound($"Answer with id={answerId} doesn't exist");
            }

            answer.IsCorrect = answerDto.IsCorrect;
            answer.Name = answerDto.Name;

            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete(nameof(DeleteTaskAnswer))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTaskAnswer(long answerId)
        {
            MultichoiceTaskAnswer answer = _db.MultichoiceTaskAnswers.Find(answerId);

            if (answer == null)
            {
                return NotFound($"Answer with id={answerId} doesn't exist");
            }

            answer.IsDeleted = true;

            _db.SaveChanges();

            return NoContent();
        }

    }
    
}
