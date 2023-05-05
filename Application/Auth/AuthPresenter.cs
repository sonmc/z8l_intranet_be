using System.ComponentModel.DataAnnotations;

namespace z8l_intranet_be.Application.User.Dto
{
    public class AuthPresenter
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
