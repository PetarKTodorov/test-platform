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

            services.AddControllersWithViews();

            services.AddSingleton(configuration);

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            RegisterAutoMapper(services);
            RegisterDatabaseServices(services);
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

            app.UseAuthentication();
            app.UseAuthorization();

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
        }
    }
}
