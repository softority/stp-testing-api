using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Stp.TestingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestSectionController : ControllerBase
    {
        [HttpPost(nameof(AddTestSection))]
        public TestSectionDto AddTestSection(long testId)
        {
            throw new NotImplementedException();
        }

        [HttpPut(nameof(UpdateSectionPosition))]
        public IActionResult UpdateSectionPosition(long sectionId, [FromBody] int newPosition)
        {
            throw new NotImplementedException();
        }

        [HttpPut(nameof(UpdateSectionName))]
        public IActionResult UpdateSectionName(long sectionId, [FromBody] string name)
        {
            throw new NotImplementedException();
        }

        [HttpDelete(nameof(DeleteSection))]
        public IActionResult DeleteSection(long sectionId)
        {
            throw new NotImplementedException();
        }
    }
}
