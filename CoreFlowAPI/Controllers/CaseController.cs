using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CoreFlowAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CaseController : ControllerBase
    {
        private readonly ICaseRepository _caseContext;
        public CaseController(ICaseRepository caseContext)
        {
            _caseContext = caseContext;  
        }

        [HttpGet]
        [Route("GetAllCases")]
        public async Task<ActionResult> GetAllCases() { return Ok(await _caseContext.GetAllAsync()); }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var caseObj = await _caseContext.GetByIdAsync(id);

            if (caseObj == null)
            {
                return NotFound("No Case Found");
            }

            return Ok(caseObj);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> CreateCase(CaseDTO obj)
        {
            var created = await _caseContext.CreateAsync(obj);

            if (created is 0)
            {
                return BadRequest();
            }

            return Ok(new { Id = created });
        }



    }
}
