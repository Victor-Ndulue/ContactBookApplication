using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repositories.Common;
using RepositoryUnitOfWork.Contract;
using RepositoryUnitOfWork.Repository;
using Service.Contracts;
using Service.Contracts.IEntityServices;
using Service.Repositories.EntityServices;
using Shared.Cloudinary;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ContactBookApi.ServiceExtension
{
    public static class ServiceExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            services.Configure<IISOptions>(options => { }); 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IUserService , UserService>();
            services.AddScoped<IContactServices, ContactServices>();
            services.AddDbContext<DataContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddScoped<Cloudinary>(provider =>
            {
                var cloudinarySettings = configuration.GetSection("CloudinarySettings")
                                                      .Get<CloudinarySettings>();

                return new Cloudinary(new Account(
                    cloudinarySettings.CloudName,
                    cloudinarySettings.ApiKey,
                    cloudinarySettings.ApiSecret));
            });
        }

        public static void ConfigureSwagger(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

            // Configure Swagger to include the authorization token field
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "JWT Authorization header using the Bearer scheme",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            options.AddSecurityDefinition("Bearer", securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                { securityScheme, new[] { "Bearer" } }
            };

            options.AddSecurityRequirement(securityRequirement);
        }

    }
}
