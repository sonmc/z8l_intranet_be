using z8l_intranet_be.Infrastructure.Schemas;

namespace z8l_intranet_be.Infrastructure.Repositories
{
    public interface IUserRepository : IGeneralRepository<UserSchema>
    {
        UserSchema GetByUserName(string userName);
    }
    public class UserRepository : GeneralRepository<UserSchema>, IUserRepository
    {
        private readonly DataContext dbContext;
        public UserRepository(DataContext context) : base(context)
        {
            this.dbContext = _context;
        }

        public UserSchema GetByUserName(string userName)
        {
            UserSchema userSchema = dbContext.Users.FirstOrDefault(u => u.UserName.Equals(userName));
            return userSchema;
        }
    }
}