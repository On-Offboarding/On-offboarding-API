using CoreFlowAPI.Business.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreFlowAPI.Controllers
{
    [Authorize]
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
    }
}
