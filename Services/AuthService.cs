using z8l_intranet_be.Helper;
using z8l_intranet_be.Helper.Exception;
using z8l_intranet_be.Infrastructure.Repositories;
using z8l_intranet_be.Infrastructure.Schemas;
using z8l_intranet_be.Application.User.Dto;

namespace z8l_intranet_be.Services
{
    public interface IAuthService
    {
        UserPresenter Login(string account, string password);
    }
    public class AuthService : IAuthService
    {
        IUserRepository userRepository;
        public AuthService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        public UserPresenter Login(string username, string password)
        {
            UserSchema user = userRepository.GetByUserName(username);
            string pwdHash = Common.GeneratePassword(password).ToLower();
            if (user.Password == null || !user.Password.ToLower().Equals(pwdHash))
            {
                throw new BadRequestException("EXCEPTION.PLEASE_VERIFY_YOUR_LOGIN_INFORMATION", "EXCEPTION.UNAUTHORIZED");
            }

            return new UserPresenter(user);
        }
    }
}