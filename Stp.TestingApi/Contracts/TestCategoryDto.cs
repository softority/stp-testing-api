using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stp.TestingApi.Contracts
{
    public class TestCategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ParentId { get; set; }
        public int Position { get; set; }
    }
}
