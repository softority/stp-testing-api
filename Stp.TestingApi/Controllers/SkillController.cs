using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stp.Data;
using Stp.Data.Entities;

namespace Stp.TestingApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly TestingDbContext _db;
        public SkillController(TestingDbContext db)
        {
            _db = db;
        }
        public class SkillDto
        {
            public long Id { get; set; }
            public string Name { get; set; }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public List<SkillDto> GetAllSkills()
        {
            var res = _db.Skills
                .Select(x => new SkillDto() { Id = x.Id, Name = x.Name })
                .ToList();

            return res;
        }

        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SkillDto> CreateSkill([FromBody]string name) 
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest($"Skill's name shouldn't be empty!");
            }

            bool nameExists = _db.Skills.Any(x => x.Name.ToUpper() == name.ToUpper());
            if (nameExists)
            {
                return BadRequest($"Skill with name {name} already exists!");
            }

            var newSkill = new Skill()
            {
                Name = name
            };
            _db.Skills.Add(newSkill);
            _db.SaveChanges();

            return CreatedAtAction(nameof(CreateSkill), newSkill);
        }

        [HttpDelete("{skillId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteSkill(long skillId)
        {
            var skillLinked = _db.TaskAndSkills.Any(x => x.SkillId == skillId);
            if (skillLinked)
            {
                return BadRequest($"Deletion is forbidden. Skill is linked to the task(s)");
            }

            var skill = _db.Skills.Find(skillId);
            if (skill == null)
            {
                return NotFound($"Skill with id={skillId} not found");
            }

            _db.Skills.Remove(skill);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
