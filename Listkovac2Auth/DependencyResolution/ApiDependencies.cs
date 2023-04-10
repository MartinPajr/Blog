using Listkovac2Auth.Authentication;
using ListkovacBL.DAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Listkovac2Auth.DependencyResolution
{
    public static class ApiDependencies
    {
        public static IServiceCollection AddApiDependencies(this IServiceCollection services)
        {
            string issuer = "issuer";
            string audience = "audience";
            string secretKey = "\"s3cr3tK3y\"";

            services.AddScoped<IGeneralDAO, GeneralDAO>();

            services.AddSingleton(o => new JwtOptions
            {
                Issuer = issuer,
                Audience = audience,
                SecretKey = secretKey
            });

            services.AddSingleton<IJwtProvider, JwtProvider>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o => o.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secretKey))
                });

            return services;
        }
    }
}