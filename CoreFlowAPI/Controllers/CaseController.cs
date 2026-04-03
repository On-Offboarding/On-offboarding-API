using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.Domain;
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
        private readonly IExcelService _excelService;
        public CaseController(ICaseService caseService, IExcelService excelService)
        {
            _caseService = caseService;
            _excelService = excelService;
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
                var error = new ErrorResponse 
                { 
                    Message = "No Case Found", 
                    StatusCode = StatusCodes.Status404NotFound, 
                    TraceId = Request.HttpContext.TraceIdentifier 
                };
                return NotFound(error);
            }

            return Ok(caseObj);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> CreateCase(CreateCaseDTO obj)
        {
            var created = await _caseService.CreateAsync(obj);

            if (created is 0)
            {
                return BadRequest();
            }

            return Ok(new { Id = created });
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> UpdateCase(CaseDTO obj)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _caseService.UpdateAsync(obj);

            if (!result)
            {
                var error = new ErrorResponse
                {
                    Message = "No Case Found",
                    StatusCode = StatusCodes.Status404NotFound,
                    TraceId = Request.HttpContext.TraceIdentifier
                };
                return NotFound(error);
            }
            
            return Ok(obj);
        }


        [HttpGet]
        [Route("Export")]
        public async Task<ActionResult> Export(int id) 
        {
            var export = await _caseService.GetAllAsync();
            var file = _excelService.ExportToExcel(export);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Cases.xlsx");
        }

    }
}
