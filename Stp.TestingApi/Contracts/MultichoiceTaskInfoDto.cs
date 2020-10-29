using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Stp.TestingApi.Contracts
{
    public class MultichoiceTaskInfoDto
    {
        [StringLength(512, MinimumLength = 1)]
        public string? Question { get; set; }

        public List<MultichoiceTaskAnswerDto>? Answers { get; set; }
    }
}
