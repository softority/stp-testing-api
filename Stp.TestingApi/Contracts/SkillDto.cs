using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Stp.TestingApi.Contracts
{
    public class SkillDto
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }

        [Required]
        [StringLength(512, MinimumLength = 1)]
        public string? Name { get; set; }
    }
}
