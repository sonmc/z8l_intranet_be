using z8l_intranet_be.Modules.UserModule.Dto;
using Microsoft.AspNetCore.Mvc;

namespace z8l_intranet_be.Modules.UserModule
{
    [Route("roles")]
    public class RoleController : Controller
    {
        public readonly IRoleService roleService;
        public RoleController(IRoleService _roleService)
        {
            roleService = _roleService;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            Object result = roleService.GetAll();
            return Ok(result);
        }

        [HttpPost()]
        public IActionResult Create(RoleSchema role)
        {
            var list = roleService.Create(role);
            return Ok(list);
        }

    }
}