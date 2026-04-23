using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Business.Services;
using CoreFlowSharedLibrary.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreFlowAPI.Controllers
{
    [Authorize]
    [Route("Api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;              
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAllEmployees() { return Ok(await _employeeService.GetAllAsync()); }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var user = await _employeeService.GetByIdAsync(id);

            if (user == null)
            {
                var error = new ErrorResponse
                {
                    Message = "No Employee Found",
                    StatusCode = StatusCodes.Status404NotFound,
                    TraceId = Request.HttpContext.TraceIdentifier
                };
                return NotFound(error);
            }

            return Ok(user);
        }
    }
}
