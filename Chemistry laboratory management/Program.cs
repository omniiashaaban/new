﻿using Chemistry_laboratory_management.Helper;
using laboratory.BLL.Services;
using laboratory.BLL.Services.laboratory.BLL.Services;
using laboratory.DAL.Data.context;
using laboratory.DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // 🔹 إضافة LaboratoryDbContext لإدارة البيانات
        builder.Services.AddDbContext<LaboratoryDbContext>(options =>
            options.UseSqlServer(connectionString));
       // builder.Services.AddScoped<IExperimentService, ExperimentService>();

        // 🔹 إضافة AppIdentityDbContext لإدارة المستخدمين و الأدوار
        builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(connectionString));

        // 🔹 إعداد الهوية (Identity) باستخدام AppIdentityDbContext
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<MaterialService>();


        builder.Services.AddScoped(typeof(GenericRepository<>));
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
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
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // 🔹 إنشاء الأدوار عند بدء التشغيل
        //async Task CreateRoles(IServiceProvider serviceProvider)
        //{
        //    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //    string[] roleNames = { "Doctor", "Student", "Admin" };

        //    foreach (var roleName in roleNames)
        //    {
        //        var roleExists = await roleManager.RoleExistsAsync(roleName);
        //        if (!roleExists)
        //        {
        //            await roleManager.CreateAsync(new IdentityRole(roleName));
        //        }
        //    }
        //}

        //using (var scope = app.Services.CreateScope())
        //{
        //    var services = scope.ServiceProvider;
        //    await CreateRoles(services);
        //}

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
