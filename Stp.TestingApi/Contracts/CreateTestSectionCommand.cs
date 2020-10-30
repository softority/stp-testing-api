using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Stp.TestingApi.Contracts
{
    public class CreateTestSectionCommand
    {
        [Required]
        [StringLength(512, MinimumLength = 1)]
        public string? Name { get; set; }

        [Range(0, int.MaxValue)]
        public int Position { get; set; }

        [Range(1, long.MaxValue)]
        public long TestId { get; set; }
    }
}
