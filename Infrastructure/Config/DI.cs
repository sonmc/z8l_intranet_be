using z8l_intranet_be.Infrastructure.Repositories;
using z8l_intranet_be.Repositories.UnitOfWork;
using z8l_intranet_be.Services;
namespace z8l_intranet_be.Infrastructure.Config
{
    public class DIConfig
    {
        public static void AddDependency(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGeneralRepository<>), (typeof(GeneralRepository<>)));
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IPermRepository, PermRepository>();

            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();

            services.AddScoped<IRolePermService, RolePermService>();
            services.AddScoped<IRolePermRepository, RolePermRepository>();
        }
    }
}
