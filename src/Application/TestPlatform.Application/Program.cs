namespace TestPlatform.Application
{
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using AutoMapper;

    using Infrastructures.ExtensionMethods;
    using TestPlatform.Database;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Database.Repositories;
    using TestPlatform.Services.Mapper;
    using TestPlatform.Services.Database.Authorization;
    using TestPlatform.Services.Database.Authorization.Interfaces;
    using TestPlatform.Database.Entities;
    using TestPlatform.DTOs.ViewModels;
    using TestPlatform.Database.Seed.BindingModels;
    using TestPlatform.Services.Managers.Interfaces;
    using TestPlatform.Services.Managers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();
            Configure(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestPlatformDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = "/Forbidden/";
                });

            services.AddControllersWithViews();

            //services.AddSession(options =>
            //{
            //    options.Cookie.Name = "TestPlatform";
            //    options.IdleTimeout = TimeSpan.FromHours(8);
            //    options.Cookie.IsEssential = true;
            //    options.Cookie.SameSite = SameSiteMode.Strict;
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            //});

            services.AddSingleton(configuration);

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            RegisterAutoMapper(services);
            RegisterDatabaseServices(services);
            RegisterManagers(services);
        }

        private static void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.MigrateDatabaseAsync().GetAwaiter().GetResult();

                app.SeedDatabaseAsync().GetAwaiter().GetResult();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.None,
            };
            app.UseCookiePolicy(cookiePolicyOptions);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
        }

        private static void RegisterAutoMapper(IServiceCollection services)
        {
            List<Assembly> assemblies = new List<Assembly>()
            {
                Assembly.GetAssembly(typeof(BaseEntity)),
                Assembly.GetAssembly(typeof(ViewModel)),
                Assembly.GetAssembly(typeof(BindingModel)),
                Assembly.GetAssembly(typeof(SeedBindingModel)),
            };

            AutoMapperConfig.RegisterMappings(assemblies.ToArray());

            services.AddSingleton<IMapper>(AutoMapperConfig.MapperInstance);
        }

        private static void RegisterDatabaseServices(IServiceCollection services)
        {
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRoleMapService, UserRoleMapService>();
        }

        private static void RegisterManagers(IServiceCollection services)
        {
            services.AddTransient<IUserManager, UserManager>();
        }
    }
}
