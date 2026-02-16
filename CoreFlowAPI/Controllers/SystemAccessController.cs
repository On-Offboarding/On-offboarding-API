using CoreFlowAPI.Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreFlowAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class SystemAccessController : ControllerBase
    {
        private readonly ISystemAccessRepository _systemAccessContext;

        public SystemAccessController(ISystemAccessRepository systemAccessContext)
        {
            _systemAccessContext = systemAccessContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAllSystemAccesses() { return Ok(await _systemAccessContext.GetAllAsync()); }

        [HttpGet]
        [Route("Profiles/GetAll")]
        public async Task<ActionResult> GetAllProfilesAsync() { return Ok(await _systemAccessContext.GetAllProfilesAsync()); }
    }
}
