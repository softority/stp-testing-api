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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MultichoiceTaskAnswerController : ControllerBase
    {
        private readonly TestingDbContext _db;
        public MultichoiceTaskAnswerController(TestingDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MultichoiceTaskAnswerDto> AddTaskAnswer([FromBody] AddTaskAnswerCommand cmd)
        {
            StpTask task = _db.Tasks.Find(cmd.TaskId);

            if(task == null)
            {
                return NotFound($"Answer with id={cmd.TaskId} doesn't exist");
            }

            MultichoiceTaskAnswer answer = new MultichoiceTaskAnswer()
            {
                Name = cmd.Name,
                IsCorrect = cmd.IsCorrect,
                TaskId = cmd.TaskId
            };

            _db.MultichoiceTaskAnswers.Add(answer);
            _db.SaveChanges();

            var res = new MultichoiceTaskAnswerDto()
            {
                Id = answer.Id,
                Name = answer.Name,
                IsCorrect = answer.IsCorrect
            };

            return CreatedAtAction(nameof(AddTaskAnswer), res);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTaskAnswer([FromBody]MultichoiceTaskAnswerDto answerDto)
        {
            MultichoiceTaskAnswer answer = _db.MultichoiceTaskAnswers.Find(answerDto.Id);

            if (answer == null)
            {
                return NotFound($"Answer with id={answerDto.Id} doesn't exist");
            }

            answer.IsCorrect = answerDto.IsCorrect;
            answer.Name = answerDto.Name;

            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete("{answerId}")]
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
