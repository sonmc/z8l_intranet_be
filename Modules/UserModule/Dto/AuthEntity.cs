using System.ComponentModel.DataAnnotations;

namespace z8l_intranet_be.Modules.UserModule.Dto
{
    public class AuthEntity
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
