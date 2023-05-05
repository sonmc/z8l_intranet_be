
using z8l_intranet_be.Infrastructure.Schemas;
namespace z8l_intranet_be.Infrastructure.Repositories
{
    public interface IRolePermRepository : IGeneralRepository<RolePermSchema>
    {
    }
    public class RolePermRepository : GeneralRepository<RolePermSchema>, IRolePermRepository
    {
        private readonly DataContext dbContext;
        public RolePermRepository(DataContext context) : base(context)
        {
            this.dbContext = _context;
        }

    }
}