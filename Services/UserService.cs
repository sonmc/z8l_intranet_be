using z8l_intranet_be.Helper;
using z8l_intranet_be.Repositories.UnitOfWork;
using z8l_intranet_be.Infrastructure.Schemas;
using z8l_intranet_be.Application.User.Dto;
using System.Collections.Generic;
using System.Linq;

namespace z8l_intranet_be.Services
{
    public interface IUserService
    {
        List<UserPresenter> GetAll();
        UserPresenter GetOne(int id);
        UserPresenter Create(UserPresenter user);
    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public UserPresenter GetOne(int id)
        {
            UserSchema user = _uow.UserRepository.GetById(id);
            var obj = new UserPresenter(user);
            return obj;
        }

        public List<UserPresenter> GetAll()
        {
            var list = _uow.UserRepository.GetAll();
            var converted = list.Select(item => new UserPresenter(item)).ToList();
            return converted;
        }

        public UserPresenter Create(UserPresenter user)
        {
            UserSchema userSchema = new UserSchema(user);
            userSchema.Password = Common.GeneratePassword(Common.DEFAULT_PASSWORD);
            _uow.UserRepository.Add(userSchema);
            _uow.SaveChanges();
            return new UserPresenter(userSchema);
        }
    }


}