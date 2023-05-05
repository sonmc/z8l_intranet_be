
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using z8l_intranet_be.Helper;
using z8l_intranet_be.Helper.Exception;
using z8l_intranet_be.Helpers;
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
            string token = JwtService.GenerateJwtToken(user.Id, appSettings.Secret);
            string refreshToken = JwtService.GenerateRefreshToken();
            Response.Cookies.Append("Token", token);
            Response.Cookies.Append("RefreshToken", refreshToken);
            return Ok();
        }

        [HttpGet("get-current-user")]
        public IActionResult GetCurrentUser()
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
                throw new UnauthorizedException();
            }
            UserEntity user = userService.GetOne(id);
            if (user == null)
            {
                throw new UnauthorizedException();
            }
            return Ok(user);
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Token");
            return Ok();
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(RefreshTokenPresenter request)
        {
            string token = request.Token;
            string refreshToken = request.RefreshToken;
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var userCredentialString = jwtToken.Claims.First(x => x.Type == "id").Value;
            int userId = Int32.Parse(userCredentialString);
            UserEntity user = userService.GetOne(userId);
            if (user == null || user.RefreshToken != request.RefreshToken)
            {
                throw new BadRequestException("EXCEPTION.UNAUTHORIZED");
            }
            var accessToken = JwtService.GenerateJwtToken(userId, appSettings.Secret);
            return Ok(new
            {
                AccessToken = accessToken,
                ExpiresIn = (int)JwtService.GetRefreshTokenExpiryTime()
            });
        }

    }
}