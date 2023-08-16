using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using Repositories.Common;
using System.Text;

namespace ContactBookApi.Extensions
{
    public static class IdentityServiceExtension
    {        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
        {

            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            }).AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddSignInManager<SignInManager<User>>()
            .AddEntityFrameworkStores<DataContext>();

            //For claims token check and validation
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding
                        .UTF8.GetBytes(config["TokenKey"])),
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                });
            return services;
        }
    }
}
