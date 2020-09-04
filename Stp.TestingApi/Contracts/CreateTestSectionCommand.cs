using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stp.TestingApi.Contracts
{
    public class CreateTestSectionCommand
    {
        public string Name { get; set; }
        public int Position { get; set; }
        public long TestId { get; set; }
    }
}
