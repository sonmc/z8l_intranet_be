
using Microsoft.AspNetCore.Mvc;
using z8l_intranet_be.Application.User.Dto;
using z8l_intranet_be.Services;

namespace z8l_intranet_be.Application.User
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
        public IActionResult Create(UserPresenter userPresenter)
        {
            var list = userService.Create(userPresenter);
            return Ok(list);
        }

    }
}