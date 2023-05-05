using z8l_intranet_be.Modules.UserModule.Dto;
using Microsoft.AspNetCore.Mvc;

namespace z8l_intranet_be.Modules.UserModule
{
    [Route("users")]
    public class UserController : Controller
    {
        public readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpGet()]
        public IActionResult Get(int? id)
        {
            Object result = id != null ? userService.GetOne((int)id) : userService.GetAll();
            return Ok(result);
        }

        [HttpPost()]
        public IActionResult Create(UserEntity userEntity)
        {
            var list = userService.Create(userEntity);
            return Ok(list);
        }

    }
}