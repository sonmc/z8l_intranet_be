using z8l_intranet_be.Modules.ZBaseModel;

namespace z8l_intranet_be.Modules.UserModule.Dto
{
    public class UserSchema : BaseSchema
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string? Password { get; set; }
        public List<RoleSchema>? Roles { get; set; }
        public UserSchema() { }
        public UserSchema(UserEntity entity)
        {
            this.UserName = entity.UserName;
            this.FullName = entity.FullName;
            this.Age = entity.Age;
            this.Email = entity.Email;
            this.Password = null;
        }
    }
}