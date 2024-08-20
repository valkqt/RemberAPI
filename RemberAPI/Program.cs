using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using DataAccess.Repositories;
using DataAccess.Repositories.CardsRepository;
using ApplicationCore.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DataAccess.Repositories.UsersRepository;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using ApplicationCore.Authorization;
using ApplicationCore.Authorization.AuthorizeJwtPolicy;
using Microsoft.Extensions.Options;
using ApplicationCore.Authorization.AuthorizeUserPolicy;

namespace RemberAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ICardsRepository, CardsRepository>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<ICardsService, CardsService>();
            builder.Services.AddScoped<IJWTAuthService, JWTAuthService>();
            builder.Services.AddScoped<IAuthCookieService, AuthCookieService>();

            //builder.Services.AddSingleton<IAuthorizationHandler, UserCanAccessHandler>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthorization(options => options.AddPolicy("AuthorizeJWT", policy => policy.Requirements.Add(new AuthorizeJwtRequirement())));
            builder.Services.AddAuthorization(options => options.AddPolicy("AuthorizeUser", policy => policy.Requirements.Add(new AuthorizeUserRequirement())));

            builder.Services.AddSingleton<IAuthorizationHandler, AuthorizeJwtHandler>();
            builder.Services.AddSingleton<IAuthorizationHandler, AuthorizeUserHandler>();


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = (context) =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Append("Token-Expired", "true");
                }
                return Task.CompletedTask;
            }
        };
    }
    );



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();


        }
    }
}
