using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PJira.API.Services;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace PJira.API
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder AddAPIServices(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                };
            });

            builder.Services.AddTransient<ITokenService, TokenService>();

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddAuthorization(options => options.DefaultPolicy =
                new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());

            return builder;
        }
    }
}
