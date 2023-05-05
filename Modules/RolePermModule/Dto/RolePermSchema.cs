
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using z8l_intranet_be.Modules.PermissionModule.Dto;
using z8l_intranet_be.Modules.UserModule.Dto;

namespace z8l_intranet_be.Modules.RolePermModule.Dto
{
    public class RolePermSchema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? PermId { get; set; }

        [ForeignKey("RoleId")]
        public RoleSchema? Role { get; set; }

        [ForeignKey("PermId")]
        public PermissionSchema? Permission { get; set; }

    }
}