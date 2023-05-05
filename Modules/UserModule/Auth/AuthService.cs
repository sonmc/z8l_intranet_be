using z8l_intranet_be.Helper;
using z8l_intranet_be.Helper.Exception;
using z8l_intranet_be.Infrastructure.Repositories;
using z8l_intranet_be.Modules.UserModule.Dto;

namespace z8l_intranet_be.Modules.UserModule
{
    public interface IAuthService
    {
        UserEntity Login(string account, string password);
    }
    public class AuthService : IAuthService
    {
        IUserRepository userRepository;
        public AuthService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        public UserEntity Login(string username, string password)
        {
            UserSchema user = userRepository.GetByUserName(username);
            string pwdHash = Common.GeneratePassword(password).ToLower();
            if (user.Password == null || !user.Password.ToLower().Equals(pwdHash))
            {
                throw new BadRequestException("EXCEPTION.PLEASE_VERIFY_YOUR_LOGIN_INFORMATION", "EXCEPTION.UNAUTHORIZED");
            }

            return new UserEntity(user);
        }
    }
}