using CoreFlowAPI.Business.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CoreFlowAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        IAuditLogService auditLogService;
        public AuditController(IAuditLogService service) 
        {
            auditLogService = service;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAllAudits()
        {
            return Ok(await auditLogService.GetAllAuditsAsync());
        }

        [HttpGet("claims")]
        public IActionResult Claims()
        {
            return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
        }
    }
}
