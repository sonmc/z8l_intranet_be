

using z8l_intranet_be.Infrastructure.Schemas;

namespace z8l_intranet_be.Infrastructure.Repositories
{
    public interface IPermRepository : IGeneralRepository<PermissionSchema>
    {
    }
    public class PermRepository : GeneralRepository<PermissionSchema>, IPermRepository
    {
        private readonly DataContext dbContext;
        public PermRepository(DataContext context) : base(context)
        {
            this.dbContext = _context;
        }

    }
}