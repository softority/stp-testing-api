using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stp.TestingApi.Contracts
{
    public class MultichoiceTaskInfoDto
    {
        public string Question { get; set; }
        public List<MultichoiceTaskAnswerDto> Answers { get; set; }
    }
}
