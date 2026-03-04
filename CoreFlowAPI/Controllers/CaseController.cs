using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CoreFlowAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CaseController : ControllerBase
    {
        private readonly ICaseService _caseService;
        public CaseController(ICaseService caseService)
        {
            _caseService = caseService;  
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAllCases() 
        { 
            return Ok(await _caseService.GetAllAsync()); 
        }

        [HttpGet]
        [Route("GetAllByStatus")]
        public async Task<ActionResult> GetAllCases(StatusOfCase status) 
        { 
            return Ok(await _caseService.GetAllAsync(status)); 
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var caseObj = await _caseService.GetByIdAsync(id);

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
            var created = await _caseService.CreateAsync(obj);

            if (created is 0)
            {
                return BadRequest();
            }

            return Ok(new { Id = created });
        }



    }
}
