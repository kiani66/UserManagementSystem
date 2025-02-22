using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UserManagementSystem.Application.CQRS.Handlers.Roles;
using UserManagementSystem.Application.Interfaces;
using UserManagementSystem.Application.Interfcaces;
using UserManagementSystem.Application.Services;
using UserManagementSystem.Infrastructure.Data;
using UserManagementSystem.Infrastructure.Interfaces;
using UserManagementSystem.Infrastructure.Persistence;
using UserManagementSystem.Infrastructure.Repositories;

namespace UserManagementSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateRoleCommandHandler).Assembly));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //ثبت سرویس ها
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserProfileService, UserProfileService>();
            
            // تنظیم دیتابیس
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ثبت Repositoryها
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();

            // اضافه کردن Swagger به DI
            builder.Services.AddEndpointsApiExplorer();

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            #region SwaggerToken

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Management API", Version = "v1" });

                // 🔹 افزودن پیکربندی برای Authorization Header
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Enter 'Bearer YOUR_TOKEN' in the field below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
            });



            #endregion  SwaggerToken
            #region Authorization_Jwt
            // کلید امنیتی برای امضای توکن‌ها
            var key = Encoding.UTF8.GetBytes("ThisIsASecretKeyForJWTWithAtLeast32Char");

            // تنظیمات احراز هویت JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            builder.Services.AddAuthorization();
            #endregion

            var app = builder.Build();

            app.UseAuthentication(); // فعال‌سازی احراز هویت
            app.UseAuthorization();  // فعال‌سازی مجوزها

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API v1");
                });
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // حذف دیتابیس
                context.Database.EnsureDeleted();

                //// ایجاد مجدد دیتابیس
                //context.Database.Migrate();

                // ایجاد دیتابیس بدون نیاز به مایگریشن
                context.Database.EnsureCreated();

                // مقداردهی اولیه دیتابیس
                SeedData.Initialize(context);
            }
            app.Run();
        }
    }
}
