
//using Chemistry_laboratory_management.Dtos;
//using laboratory.BLL.Services;
//using LinkDev.Facial_Recognition.BLL.Helper.Errors;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace Chemistry_laboratory_management.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AccountController : ControllerBase
//    {
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly SignInManager<IdentityUser> _signInManager;
//        private readonly IAuthService _authService;

//        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAuthService authService)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _authService = authService;
//        }

//        [HttpPost("login")]
//        public async Task<ActionResult<UserDto>> Login(LoginDto model)
//        {
//            var user = await _userManager.FindByEmailAsync(model.Email);
//            if (user == null)
//                return Unauthorized(new ApiResponse(401, "Invalid Email or Password"));

//            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
//            if (!result.Succeeded)
//                return Unauthorized(new ApiResponse(401, "Invalid Email or Password"));

//            var roles = await _userManager.GetRolesAsync(user);

//            return Ok(new UserDto
//            {
//                Token = await _authService.CreateTokenAsync(user.Email, user.UserName, roles),
//                Email = user.Email,
//                DisplayName = user.UserName,
//                Role = roles.FirstOrDefault()  // إرسال الدور للمستخدم
//            });
//        }

//        [HttpPost("register")]
//        public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterDto model)
//        {
//            var user = new IdentityUser
//            {
//                UserName = model.Email.Split("@")[0],
//                Email = model.Email,
//                PhoneNumber = model.PhoneNumber
//            };

//            var result = await _userManager.CreateAsync(user, model.Password);
//            if (!result.Succeeded)
//                return BadRequest(new ApiResponse(400, "User registration failed"));

//            // التحقق من صحة الدور قبل التعيين
//            string[] validRoles = { "Doctor", "Student", "Admin" };
//            if (!validRoles.Contains(model.Role))
//                return BadRequest(new ApiResponse(400, "Invalid role. Choose from Doctor, Student, or Admin."));

//            // تعيين الدور للمستخدم
//            await _userManager.AddToRoleAsync(user, model.Role);

//            return Ok(new ApiResponse(200, $"{model.Role} registered successfully."));
//        }

//    }
//}
