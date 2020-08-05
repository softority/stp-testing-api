using System.Collections.Generic;

namespace Stp.TestingApi.Contracts
{
    public class TaskCategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public int Position { get; set; }
     
    }
}
