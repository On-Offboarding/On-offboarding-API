using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CoreFlowAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
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
                return NotFound("No User Found");
            }

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


    }
}

