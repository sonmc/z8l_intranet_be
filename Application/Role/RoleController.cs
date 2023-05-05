using Microsoft.AspNetCore.Mvc;
using z8l_intranet_be.Infrastructure.Schemas;
using z8l_intranet_be.Services;

namespace z8l_intranet_be.Application
{
    [Route("roles")]
    public class RoleController : Controller
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService _roleService)
        {
            roleService = _roleService;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            var result = roleService.GetAll();
            return Ok(result);
        }

        [HttpPost()]
        public IActionResult Create(RoleSchema role)
        {
            roleService.Create(role);
            return Ok();
        }

    }
}