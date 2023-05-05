using System.Collections.Generic;

namespace z8l_intranet_be.Infrastructure.Schemas
{
    public class PermissionSchema : BaseSchema
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public List<RoleSchema>? Roles { get; set; }
    }
}