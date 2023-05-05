
using z8l_intranet_be.Infrastructure.Schemas;

namespace z8l_intranet_be.Infrastructure.Repositories
{
    public interface IUserRoleRepository : IGeneralRepository<UserRoleSchema>
    {
    }
    public class UserRoleRepository : GeneralRepository<UserRoleSchema>, IUserRoleRepository
    {
        private readonly DataContext dbContext;
        public UserRoleRepository(DataContext context) : base(context)
        {
            this.dbContext = _context;
        }

    }
}