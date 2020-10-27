using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stp.TestingApi.Contracts
{
    public class TaskCategoryDto
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }

        [StringLength(512, MinimumLength = 1)]
        public string? Name { get; set; }

        [Range(1, long.MaxValue)]
        public long? ParentId { get; set; }

        [Range(1, int.MaxValue)]
        public int Position { get; set; }
     
    }
}
