using System.Collections.Generic;
using z8l_intranet_be.Application.User.Dto;

namespace z8l_intranet_be.Infrastructure.Schemas
{
    public class UserSchema : BaseSchema
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string? Password { get; set; }
        public string? RefreshToken { get; set; }
        public List<RoleSchema>? Roles { get; set; }
        public UserSchema() { }
        public UserSchema(UserPresenter entity)
        {
            this.UserName = entity.UserName;
            this.FullName = entity.FullName;
            this.Age = entity.Age;
            this.Email = entity.Email;
            this.Password = null;
        }
    }
}