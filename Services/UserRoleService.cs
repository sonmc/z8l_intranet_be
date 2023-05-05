using z8l_intranet_be.Infrastructure.Schemas;
using z8l_intranet_be.Repositories.UnitOfWork;

namespace z8l_intranet_be.Application.RolePermModule
{
    public interface IUserRoleService
    {
        UserRoleSchema Create(UserRoleSchema userRole);
    }

    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork _uow;
        public UserRoleService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public UserRoleSchema Create(UserRoleSchema userRole)
        {
            _uow.UserRoleRepository.Add(userRole);
            _uow.SaveChanges();
            return userRole;
        }
    }


}