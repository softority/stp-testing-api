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
    public class MultichoiceExerciseAnswerController : ControllerBase
    {
        private readonly TestingDbContext _db;
        public MultichoiceExerciseAnswerController(TestingDbContext db)
        {
            _db = db;
        }

        /* https://localhost:5001/api/MultichoiceExerciseAnswer/AddExerciseAnswer */
        [HttpPost(nameof(AddExerciseAnswer))]
        public MultichoiceAnswerDto AddExerciseAnswer(long exerciseId, [FromBody]MultichoiceAnswerDto answerDto)
        {
            Exercise exercise = _db.ExerciseList.Find(exerciseId);

            if(exercise == null)
            {
                return null;
            }

            MultichoiceExerciseAnswer answer = new MultichoiceExerciseAnswer()
            {
                Name = answerDto.Name,
                IsCorrect = answerDto.IsCorrect,
                ExerciseId = exerciseId
            };

            _db.MultichoiceAnswerList.Add(answer);
            _db.SaveChanges();

            answerDto.Id = answer.Id;
            answerDto.ExerciseId = exerciseId;

            return answerDto;
        }

        /* https://localhost:5001/api/MultichoiceExerciseAnswer/UpdateExerciseAnswer */
        [HttpPut(nameof(UpdateExerciseAnswer))]
        public IActionResult UpdateExerciseAnswer(long answerId, [FromBody]MultichoiceAnswerDto answerDto)
        {
            MultichoiceExerciseAnswer answer = _db.MultichoiceAnswerList.Find(answerId);

            if (answer == null)
            {
                return BadRequest($"Answer with id={answerId} not found");
            }

            answer.IsCorrect = answerDto.IsCorrect;
            answer.Name = answerDto.Name;

            _db.SaveChanges();

            return Ok();
        }

        /* https://localhost:5001/api/MultichoiceExerciseAnswer/DeleteExerciseAnswer */
        [HttpDelete(nameof(DeleteExerciseAnswer))]
        public IActionResult DeleteExerciseAnswer(long answerId)
        {
            MultichoiceExerciseAnswer answer = _db.MultichoiceAnswerList.Find(answerId);

            if (answer == null)
            {
                return BadRequest($"Answer with id={answerId} not found");
            }

            answer.IsDeleted = true;

            _db.SaveChanges();

            return Ok();
        }

    }
    
}
