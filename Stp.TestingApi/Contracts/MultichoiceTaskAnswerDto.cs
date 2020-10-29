using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stp.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Stp.TestingApi.Contracts
{
    public class MultichoiceTaskAnswerDto
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }

        [Required]
        [StringLength(512, MinimumLength = 1)]
        public string? Name { get; set; }

        public bool IsCorrect { get; set; }
    }

    public class AddTaskAnswerCommand
    {
        [Range(1, long.MaxValue)]
        public long TaskId { get; set; }

        [Required]
        [StringLength(512, MinimumLength = 1)]
        public string? Name { get; set; }

        public bool IsCorrect { get; set; }
    }
}