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
            const string BINDING_MODELS_ASSEMBLY_NAME = "TestPlatform.DTOs.BindingModels";
            const string VIEW_MODELS_ASSEMBLY_NAME = "TestPlatform.DTOs.ViewModels";
            const string DATABASE_ENTITIES_ASSEMBLY_NAME = "TestPlatform.Database.Entities";

            List<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(Assembly.Load(VIEW_MODELS_ASSEMBLY_NAME));
            assemblies.Add(Assembly.Load(BINDING_MODELS_ASSEMBLY_NAME));
            assemblies.Add(Assembly.Load(DATABASE_ENTITIES_ASSEMBLY_NAME));

            AutoMapperConfig.RegisterMappings(assemblies.ToArray());

            services.AddSingleton<IMapper>(AutoMapperConfig.MapperInstance);
        }

        private static void RegisterDatabaseServices(IServiceCollection services)
        {
            services.AddTransient<IRoleService, RoleService>();
        }
    }
}
