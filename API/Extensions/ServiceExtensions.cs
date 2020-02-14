using System;
using System.Text;
using System.Threading.Tasks;
using Application.User;
using Domain.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<DataContext>(opt =>
                {
                    opt.UseLazyLoadingProxies();
                    opt.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
                });
        }

        public static void ConfigureCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("WWW-Authenticate")
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials();
                });
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services
                .AddIdentityCore<AppUser>(opt =>
                {
                    opt.Password.RequireUppercase = false;
                })
                .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<Persistence.DataContext>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/chat")))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(opt =>
                   {
                       var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                       opt.Filters.Add(new AuthorizeFilter(policy));
                   })
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Login>());
        }
    }
}