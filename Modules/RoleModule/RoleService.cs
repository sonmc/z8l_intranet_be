
using z8l_intranet_be.Modules.UserModule.Dto;
using z8l_intranet_be.Repositories.UnitOfWork;

namespace z8l_intranet_be.Modules.UserModule
{
    public interface IRoleService
    {
        List<RoleSchema> GetAll();
        RoleSchema Create(RoleSchema role);
    }

    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _uow;
        public RoleService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public List<RoleSchema> GetAll()
        {
            var list = _uow.RoleRepository.GetAll().ToList();
            return list;
        }

        public RoleSchema Create(RoleSchema role)
        {
            _uow.RoleRepository.Add(role);
            _uow.SaveChanges();
            return role;
        }
    }

}