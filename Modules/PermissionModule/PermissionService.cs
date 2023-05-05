
using z8l_intranet_be.Modules.PermissionModule.Dto;
using z8l_intranet_be.Repositories.UnitOfWork;

namespace z8l_intranet_be.Modules.UserModule
{
    public interface IPermissionService
    {
        List<PermissionSchema> GetAll();
        PermissionSchema Create(PermissionSchema role);
    }

    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _uow;
        public PermissionService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public List<PermissionSchema> GetAll()
        {
            var list = _uow.PermissionRepository.GetAll().ToList();
            return list;
        }

        public PermissionSchema Create(PermissionSchema perm)
        {
            _uow.PermissionRepository.Add(perm);
            _uow.SaveChanges();
            return perm;
        }
    }


}