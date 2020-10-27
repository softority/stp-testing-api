using Stp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Stp.TestingApi.Validation;

namespace Stp.TestingApi.Contracts
{
    public class TestCategoryDto
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }

        [StringLength(512, MinimumLength = 1)]
        public string? Name { get; set; }

        [Range(1, long.MaxValue)]
        public long? ParentId { get; set; }

        [Range(1, int.MaxValue)]
        public int Position { get; set; }

        public List<TestSummaryDto>? Tests { get; set; }
    }

    public class TestSummaryDto
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }

        [StringLength(512, MinimumLength = 1)]
        public string? Name { get; set; }

        [EnumMemberValue(typeof(TestStatus))]
        public TestStatus Status { get; set; }

        public List<string>? Skills { get; set; }

        [Range(1, int.MaxValue)]
        public int DurationMinutes { get; set; }

        [Range(1, int.MaxValue)]
        public int TasksCount { get; set; }
    }
}
