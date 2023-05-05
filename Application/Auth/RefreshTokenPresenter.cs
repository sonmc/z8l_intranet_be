

namespace z8l_intranet_be.Application.User.Dto
{
    public class RefreshTokenPresenter
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public RefreshTokenPresenter() { }
        public RefreshTokenPresenter(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}