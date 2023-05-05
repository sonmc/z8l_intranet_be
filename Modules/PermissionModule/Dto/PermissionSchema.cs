using z8l_intranet_be.Modules.UserModule.Dto;
using z8l_intranet_be.Modules.ZBaseModel;

namespace z8l_intranet_be.Modules.PermissionModule.Dto
{
    public class PermissionSchema : BaseSchema
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public List<RoleSchema>? Roles { get; set; }
    }
}