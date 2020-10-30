using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stp.TestingApi.Contracts
{
    public class TaskCategoryDto
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public long? ParentId { get; set; }

        public int Position { get; set; }
     
    }
}
