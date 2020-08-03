using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stp.Data.Entities;

namespace Stp.TestingApi.Contracts
{
    public class MultichoiceAnswerDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsDeleted { get; set; }
        public long TaskId { get; set; }
    }
}
