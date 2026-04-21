using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.Domain;
using CoreFlowSharedLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreFlowAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;

        public UserController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAllUsers() { return Ok(await _userService.GetAllAsync()); }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<ActionResult> Get(int id) 
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                var error = new ErrorResponse 
                { 
                    Message = "No User Found",
                    StatusCode = StatusCodes.Status404NotFound,
                    TraceId = Request.HttpContext.TraceIdentifier
                };
                return NotFound(error);
            }

            return Ok(user); 
        }
        [HttpPost]
        [Route("me")]
        public async Task<ActionResult<UserDTO>> Me()
        {
            var user = await _currentUserService.UpsertCurrentUserAsync();

            if (user is null)
                return Unauthorized();

            return Ok(user);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> CreateUser(UserDTO user)
        {
            var created = await _userService.CreateAsync(user);

            if(created is 0)
            {
                return BadRequest();
            }

            return Ok(new { Id=created });
        }

        [HttpPut]
        [Route("{userId}/role/{roleId}")]
        public async Task<ActionResult> UpdateRole(int userId, int roleId)
        {
            var updated = await _userService.UpdateRoleAsync(userId, roleId);

            if (!updated)
            {
                var error = new ErrorResponse
                {
                    Message = "No User Found",
                    StatusCode = StatusCodes.Status404NotFound,
                    TraceId = Request.HttpContext.TraceIdentifier
                };
                return NotFound(error);
            }

            return NoContent();
        }
    }
}

