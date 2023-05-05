
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace z8l_intranet_be.Infrastructure.Schemas
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