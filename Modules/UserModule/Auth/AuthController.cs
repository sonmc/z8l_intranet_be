
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using z8l_intranet_be.Helper;
using z8l_intranet_be.Modules.UserModule.Dto;

namespace z8l_intranet_be.Modules.UserModule
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;
        private readonly AppSettings appSettings;

        public AuthController(IAuthService _service, IUserService _userService, IOptions<AppSettings> _appSettings)
        {
            authService = _service;
            userService = _userService;
            appSettings = _appSettings.Value;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthEntity model)
        {
            if (model.Username == null || model.Password == null) { return BadRequest(); }
            UserEntity user = authService.Login(model.Username, model.Password);
            string token = Common.GenerateJwtToken(user.Id, appSettings.Secret);
            string refreshToken = Common.GenerateRefreshToken();
            Response.Cookies.Append("Token", token);
            Response.Cookies.Append("RefreshToken", refreshToken);
            return Ok();
        }

        [HttpGet("getCurrentUser")]
        public IActionResult Login()
        {
            string token = Request.Cookies["Token"];
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userCredentialString = jwtToken.Claims.First(x => x.Type == "id").Value;
            int id = 0;
            if (!Int32.TryParse(userCredentialString, out id))
            {
                // throw new UnauthorizedException();
            }
            UserEntity user = userService.GetOne(id);
            if (user == null)
            {
                // throw new UnauthorizedException();
            }
            return Ok(user);
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Token");
            return Ok();
        }

    }
}