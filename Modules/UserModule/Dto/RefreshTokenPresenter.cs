

namespace z8l_intranet_be.Modules.UserModule.Dto
{
    public class RefreshTokenPresenter
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public RefreshTokenPresenter()
        {
            Token = "";
            RefreshToken = "";
        }
    }
}