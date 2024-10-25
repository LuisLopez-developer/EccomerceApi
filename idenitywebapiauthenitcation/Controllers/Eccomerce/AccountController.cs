using EccomerceApi.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace EccomerceApi.Controllers.Eccomerce
{
    [ApiController]
    [Route("api/eccomerce/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IRoleService _roleService;

        public AccountController(
            UserManager<UserModel> userManager,
            SignInManager<UserModel> signInManager,
            IRoleService roleService,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleService = roleService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new UserModel
            {
                UserName = model.UserName,
                Email = model.Email,
                StateId = 1 // Activo
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _roleService.AddUserRoleAsync(model.Email, ["Customer"]);
                return Ok(new { Message = "Usuario registrado exitosamente." });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }
    }

    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
