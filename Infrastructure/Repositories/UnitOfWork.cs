
using z8l_intranet_be.Infrastructure;
using z8l_intranet_be.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using System;

namespace z8l_intranet_be.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dbContext;

        public IUserRepository UserRepository { get; }
        public IRolePermRepository RolePermRepository { get; }

        public IRoleRepository RoleRepository { get; }

        public IPermRepository PermissionRepository { get; }

        public IUserRoleRepository UserRoleRepository { get; }

        public UnitOfWork(DataContext _dbContext,
                          IUserRepository userRepository,
                          IRolePermRepository rolePermRepository,
                          IRoleRepository roleRepository,
                          IPermRepository permRepository,
                          IUserRoleRepository userRoleRepository
                          )
        {
            this.dbContext = _dbContext;
            this.UserRepository = userRepository;
            this.RolePermRepository = rolePermRepository;
            this.RoleRepository = roleRepository;
            this.PermissionRepository = permRepository;
            this.UserRoleRepository = userRoleRepository;
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return this.dbContext.Database.CreateExecutionStrategy();
        }

        public IDbContextTransaction BeginTransaction()
        {
            IDbContextTransaction dbContextTransaction = this.dbContext.Database.BeginTransaction();
            return dbContextTransaction;
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return this.dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dbContext.Dispose();
            }
        }

        Task IUnitOfWork.SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}

