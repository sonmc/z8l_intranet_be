

using z8l_intranet_be.Infrastructure.Schemas;

namespace z8l_intranet_be.Application.User.Dto
{
    public class UserPresenter
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string RefreshToken { get; set; }
        public UserPresenter() { }
        public UserPresenter(UserSchema userSchema)
        {
            this.Id = userSchema.Id;
            this.UserName = userSchema.UserName;
            this.FullName = userSchema.FullName;
            this.Email = userSchema.Email;
            this.Age = userSchema.Age;
        }
    }
}