using CoreFlowAPI.Business.Interface;
using CoreFlowSharedLibrary.Domain;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreFlowAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class CaseController : ControllerBase
    {
        private readonly ICaseService _caseService;
        private readonly IPDFService _pdfService;


        public CaseController(ICaseService caseService, IPDFService pdfService)
        {
            _caseService = caseService;
            _pdfService = pdfService;
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
        public async Task<ActionResult> CreateCase (CreateCaseDTO obj)
        {
            
            var createdId = await _caseService.CreateAsync(obj);

            if (createdId == 0)
            {
                return BadRequest(new { Message = "Kunde inte skapa case" });
            }

            return Ok(new
            {
                Id = createdId,
                Message = "Case skapad framgångsrikt"
            });
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> UpdateCase(CaseDTO obj)
        {

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
        [Route("Export/{caseId}")]
        public async Task<ActionResult> ExportToPdf(int caseId)
        {
            var caseObj = await _caseService.GetByIdAsync(caseId);

            if (caseObj == null)
                return NotFound();

            return File(await _pdfService.ExportToPDF(caseObj),"application/pdf","Behörighetsrapport.pdf");
        }
    }
}