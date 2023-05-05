using z8l_intranet_be.Helper;
using z8l_intranet_be.Modules.UserModule.Dto;
using z8l_intranet_be.Repositories.UnitOfWork;

namespace z8l_intranet_be.Modules.UserModule
{
    public interface IUserService
    {
        List<UserEntity> GetAll();
        UserEntity GetOne(int id);
        UserEntity Create(UserEntity user);
    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public UserEntity GetOne(int id)
        {
            UserSchema user = _uow.UserRepository.GetById(id);
            var obj = new UserEntity(user);
            return obj;
        }

        public List<UserEntity> GetAll()
        {
            var list = _uow.UserRepository.GetAll();
            var converted = list.Select(item => new UserEntity(item)).ToList();
            return converted;
        }

        public UserEntity Create(UserEntity user)
        {
            UserSchema userSchema = new UserSchema(user);
            userSchema.Password = Common.GeneratePassword(Common.DEFAULT_PASSWORD);
            _uow.UserRepository.Add(userSchema);
            _uow.SaveChanges();
            return new UserEntity(userSchema);
        }
    }


}