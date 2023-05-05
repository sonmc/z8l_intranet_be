
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace z8l_intranet_be.Infrastructure.Schemas
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