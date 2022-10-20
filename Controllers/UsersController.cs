using IndividualProject.DAL;
using IndividualProject.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndividualProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _user;

        public UsersController(IUser user)
        {
            _user = user;
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registration(AddUserDTO user)
        {
            try
            {
                await _user.Registration(user);
                return Ok($"Registration User {user.Username} successed");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate(AddUserDTO userDto)
        {
            try
            {
                var user = await _user.Authenticate(userDto);
                if (user == null)
                    return BadRequest("Username or Password doesn't match");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
