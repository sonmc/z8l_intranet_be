
using z8l_intranet_be.Infrastructure.Schemas;
using z8l_intranet_be.Repositories.UnitOfWork;

namespace z8l_intranet_be.Services
{
    public interface IRolePermService
    {
        RolePermSchema Create(RolePermSchema rolePerm);
    }

    public class RolePermService : IRolePermService
    {
        private readonly IUnitOfWork _uow;
        public RolePermService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public RolePermSchema Create(RolePermSchema rolePerm)
        {
            _uow.RolePermRepository.Add(rolePerm);
            _uow.SaveChanges();
            return rolePerm;
        }
    }


}