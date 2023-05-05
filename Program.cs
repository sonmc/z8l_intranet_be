using Microsoft.EntityFrameworkCore;
using z8l_intranet_be.Infrastructure;
using z8l_intranet_be.Helper;
using z8l_intranet_be.Middleware;
using z8l_intranet_be.Infrastructure.Config;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
{
    var services = builder.Services;
    services.AddDbContext<DataContext>(x => x.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql")));

    services.AddAuthentication(x =>
           {
               x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Secret"])),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });
    services.AddCors(options =>
           {
               options.AddDefaultPolicy(builder =>
                   builder.SetIsOriginAllowed(_ => true)
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials());
           });
    services.AddControllers();
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    DIConfig.AddDependency(services);
}

var app = builder.Build();
{
    app.UseCors();
    app.UseMiddleware<JwtMiddleware>();
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();
}

app.Run();
