
using z8l_intranet_be.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace z8l_intranet_be.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRolePermRepository RolePermRepository { get; }
        IRoleRepository RoleRepository { get; }
        IPermRepository PermissionRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }

        int SaveChanges();
        Task SaveChangesAsync();
        IExecutionStrategy CreateExecutionStrategy();
        IDbContextTransaction BeginTransaction();
    }
}

