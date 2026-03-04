using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreFlowAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class SystemAccessController : ControllerBase
    {
        private readonly ISystemAccessService _systemAccessService;

        public SystemAccessController(ISystemAccessService systemAccessService)
        {
            _systemAccessService = systemAccessService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAllSystemAccesses() { return Ok(await _systemAccessService.GetAllAsync()); }

        [HttpGet]
        [Route("Profiles/GetAll")]
        public async Task<ActionResult> GetAllProfilesAsync() { return Ok(await _systemAccessService.GetAllProfilesAsync()); }
    }
}
