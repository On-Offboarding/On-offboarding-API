using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreFlowAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAllAudits()
        {
            return NotFound("ok");
        }
    }
}
