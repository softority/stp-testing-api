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
    public class ExerciseController : ControllerBase
    {
        private readonly TestingDbContext _db;
        public ExerciseController(TestingDbContext db)
        {
            _db = db;
        }

        /* https://localhost:5001/api/Exercise/GetExercisesByCategory */
        [HttpGet(nameof(GetExercisesByCategory))]
        public IEnumerable<ExerciseDto> GetExercisesByCategory(long exerciseCategoryId)
        {
            var res = _db.ExerciseList
                .Where(x => x.ExerciseCategoryId == exerciseCategoryId)
                .Include(x => x.MultichoiceAnswers)
                .Select(x => new ExerciseDto()
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
                            ExerciseId = a.ExerciseId,
                            IsCorrect = a.IsCorrect,
                            Name = a.Name
                            
                        }).ToList()
                });       

            return res;
        }

        /* https://localhost:5001/api/Exercise/AddExercise */
        [HttpPost(nameof(AddExercise))]
        public ExerciseDto AddExercise(long exerciseCategoryId, [FromBody]ExerciseDto exerciseDto)
        {
            Exercise exercise = new Exercise() 
            {
                ExerciseCategoryId = exerciseCategoryId,
                Name = exerciseDto.Name,
                Description = exerciseDto.Description,
                Points = exerciseDto.Points,
                DurationMinutes = exerciseDto.DurationMinutes,
                Type = exerciseDto.Type,
                Complexity = exerciseDto.Complexity
            };

            _db.ExerciseList.Add(exercise);
            _db.SaveChanges();

            exerciseDto.Id = exercise.Id;

            return exerciseDto;
        }

        [HttpPut(nameof(UpdateExerciseName))]
        /* https://localhost:5001/api/Exercise/UpdateExerciseName */
        public IActionResult UpdateExerciseName(long exerciseId, [FromBody]string name)
        {
            Exercise exercise = FindExercise(exerciseId);

            if (exercise == null)
            {
                return BadRequest();
            }

            exercise.Name = name;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut(nameof(UpdateExerciseDuration))]
        /* https://localhost:5001/api/Exercise/UpdateExerciseDuration */
        public IActionResult UpdateExerciseDuration(long exerciseId, [FromBody]int duration)
        {
            Exercise exercise = FindExercise(exerciseId);

            if (exercise == null)
            {
                return BadRequest();
            }

            exercise.DurationMinutes = duration;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut(nameof(UpdateExercisePoints))]
        /* https://localhost:5001/api/Exercise/UpdateExercisePoints */
        public IActionResult UpdateExercisePoints(long exerciseId, [FromBody]int points)
        {
            Exercise exercise = FindExercise(exerciseId);

            if (exercise == null)
            {
                return BadRequest();
            }

            exercise.Points = points;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut(nameof(UpdateExerciseComplexity))]
        /* https://localhost:5001/api/Exercise/UpdateExerciseComplexity */
        public IActionResult UpdateExerciseComplexity(long exerciseId, [FromBody]ExerciseComplexity complexity)
        {
            Exercise exercise = FindExercise(exerciseId);

            if (exercise == null)
            {
                return BadRequest();
            }

            exercise.Complexity = complexity;
            _db.SaveChanges();

            return Ok();
        }

        [HttpPut(nameof(UpdateExerciseDescription))]
        /* https://localhost:5001/api/Exercise/UpdateExerciseDescription */
        public IActionResult UpdateExerciseDescription(long exerciseId, [FromBody]string description)
        {
            Exercise exercise = FindExercise(exerciseId);

            if (exercise == null)
            {
                return BadRequest();
            }

            exercise.Description = description;
            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete(nameof(DeleteExercise))]
        /* https://localhost:5001/api/Exercise/DeleteExercise */
        public IActionResult DeleteExercise(long exerciseId)
        {
            Exercise exercise = FindExercise(exerciseId);

            if (exercise == null)
            {
                return BadRequest();
            }

            exercise.IsDeleted = true;
            _db.SaveChanges();

            return Ok();
        }

        private Exercise FindExercise(long id)
        {
            return _db.ExerciseList.Find(id);
        }

    }
}
