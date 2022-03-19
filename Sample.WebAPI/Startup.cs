using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Sample.Business.Abstract;
using Sample.Business.Concrete;
using Sample.DataAccess.DbContexts;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sample.WebAPI.Middlewares;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace Sample.WebAPI
{
    public static class Startup
    {
        public static WebApplication InitializeApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            return app;
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            #region Use Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Token:Issuer"],
                    ValidAudience = builder.Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<SampleDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DebugConnection")));


            #region AutoMapper
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            builder.Services.AddScoped<IUnitOfWorks, UnitOfWorks>();
            builder.Services.AddScoped<IAccessService, AccessManager>();
            builder.Services.AddScoped<IManagmentService, ManagmentManager>();


        }
        private static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            #region Custom ExceptionMiddleware
            app.UseCustomException();
            #endregion

            #region Use Authentications
            app.UseAuthentication();
            #endregion


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
