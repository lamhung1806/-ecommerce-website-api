using API.Service.Users;
using API.ViewModels.UserRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountsController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVm request)
        {
            var result = await this.userService.GenerateToken(request);

            if (result.Token != null)
                return Ok(result.Token);

            return BadRequest(result.Message);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(CreateUserVm request)
        {
            var result = await this.userService.CreateAsync(request);

            if (result.IsSuccessed)
                return Ok();

            return BadRequest(result.Message);
        }

        [HttpGet]
        [Authorize(Roles ="SuperAdmin")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await this.userService.GetUsers());
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetRoles(string userId)
        {
            var result = await this.userService.GetRoles(userId);

            if(result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateRoles(IList<ManageUserRolesViewModel> model, string userId)
        {
            var result = await this.userService.UpdateRoleUser(model, userId);

            if (result)
                return Ok(result);

            return BadRequest();
        }
    }
}
