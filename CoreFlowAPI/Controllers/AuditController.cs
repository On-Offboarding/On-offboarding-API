using CoreFlowAPI.Business.Interface;
using Microsoft.AspNetCore.Http;
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
    }
}
