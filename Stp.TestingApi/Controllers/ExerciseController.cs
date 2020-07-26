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
                .ToList().Where(x => x.ExerciseCategoryId == exerciseCategoryId)
                .Select(x => new ExerciseDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Points = x.Points,
                    DurationMinutes = x.DurationMinutes, 
                    Complexity = (ExerciseComplexity)x.Complexity
                });       

            return res;
        }

        /* https://localhost:5001/api/Exercise/AddExercise */
        [HttpPost(nameof(AddExercise))]
        public IActionResult AddExercise(long exerciseCategoryId, [FromBody]ExerciseDto exercise)
        {
            Exercise newExercise = new Exercise();
            // maybe in the constructor?
            newExercise.Id = (long)exercise.Id; 
            newExercise.Name = exercise.Name; 
            newExercise.Description = exercise.Description; 
            newExercise.Points = exercise.Points; 
            newExercise.DurationMinutes = exercise.DurationMinutes; 
            newExercise.Complexity = (int)exercise.Complexity; 

            _db.ExerciseList.Add(newExercise);
            //_db.SaveChangesAsync(); // do not work. server settings?
            _db.SaveChanges();

            return Ok();
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

            exercise.Complexity = (int)complexity;
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

        [HttpPut(nameof(DeleteExercise))]
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
            var res = _db.ExerciseList
                .Where(x => (x.Id == id))
                .FirstOrDefault();

            return res;
        }

        private Exercise FindExerciseIgnoringFilters(long id)
        {
            var res = _db.ExerciseList.IgnoreQueryFilters()
                .Where(x => (x.Id == id))                
                .FirstOrDefault();

            return res;
        }
    }
}
