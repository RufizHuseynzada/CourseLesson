using AutoMapper;
using CourseLesson.Contract;
using CourseLesson.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseLesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IAuthManager _repos;
        private readonly IMapper _mapper;

        public UserController(IAuthManager repos, IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }

        // POST: api/User/register
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("register")]
        public async Task<ActionResult> Register(CreateUserMod createUserMod)
        {
            var res = await _repos.Register(createUserMod);

            if (res.Any())
            {
                foreach (var error in res)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }

        // CHECK: api/User/login
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("login")]
        public async Task<ActionResult> LogIn(LogInMod logInMod)
        {
            var res = await _repos.LogIn(logInMod);

            if (res == null)
            {
               return Unauthorized();
            }

            return Ok(res);
        }

        // CHECK: api/User/login
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("refreshToken")]
        public async Task<ActionResult> RefreshToken(AuthResponseDto authResponseDto)
        {
            var res = await _repos.VerifyRefreshToken(authResponseDto);

            if (res == null)
            {
                return Unauthorized();
            }

            return Ok(res);
        }

    }
}
