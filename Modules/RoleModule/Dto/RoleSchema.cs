
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using z8l_intranet_be.Modules.PermissionModule.Dto;

namespace z8l_intranet_be.Modules.UserModule.Dto
{
    public class RoleSchema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<PermissionSchema>? Permissions { get; set; }
        public List<UserSchema>? Users { get; set; }
    }
}