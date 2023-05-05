
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using z8l_intranet_be.Modules.UserModule.Dto;

namespace z8l_intranet_be.Modules.UserRoleModule.Dto
{
    public class UserRoleSchema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? UserId { get; set; }

        [ForeignKey("RoleId")]
        public RoleSchema? Role { get; set; }

        [ForeignKey("UserId")]
        public UserSchema? User { get; set; }

    }
}