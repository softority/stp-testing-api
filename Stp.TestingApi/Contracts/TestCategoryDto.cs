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
        public long Id { get; set; }

        public string? Name { get; set; }

        public long? ParentId { get; set; }

        public int Position { get; set; }

        public List<TestSummaryDto>? Tests { get; set; }
    }

    public class TestSummaryDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }

        public TestStatus Status { get; set; }

        public List<string>? Skills { get; set; }

        public int DurationMinutes { get; set; }

        public int TasksCount { get; set; }
    }
}
