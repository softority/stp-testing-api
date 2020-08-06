using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stp.Data.Entities;

namespace Stp.TestingApi.Contracts
{
    public class MultichoiceTaskAnswerDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsCorrect { get; set; }
    }
}
