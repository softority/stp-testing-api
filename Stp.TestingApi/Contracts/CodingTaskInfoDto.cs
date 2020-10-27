using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Stp.TestingApi.Contracts
{
    public class CodingTaskInfoDto
    {
        [StringLength(512, MinimumLength = 1)]
        public string? Question { get; set; }

        [StringLength(2048, MinimumLength = 1)]
        public string? CodingToolUrl { get; set; }
    }
}
