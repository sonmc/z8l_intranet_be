
using z8l_intranet_be.Infrastructure.Schemas;
namespace z8l_intranet_be.Infrastructure.Repositories
{
    public interface IRoleRepository : IGeneralRepository<RoleSchema>
    {
    }
    public class RoleRepository : GeneralRepository<RoleSchema>, IRoleRepository
    {
        private readonly DataContext dbContext;
        public RoleRepository(DataContext context) : base(context)
        {
            this.dbContext = _context;
        }

    }
}