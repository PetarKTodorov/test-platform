namespace TestPlatform.Application
{
    using System.Reflection;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.CookiePolicy;
    using Microsoft.AspNetCore.Mvc;

    using TestPlatform.Application.Infrastructures.ExtensionMethods;
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
    using TestPlatform.Services.Database.Test.Interfaces;
    using TestPlatform.Services.Database.Test;
    using TestPlatform.Services.Database.Subjects.Interfaces;
    using TestPlatform.Services.Database.Subjects;
    using TestPlatform.Application.Infrastructures.Searcher.MVC;
    using TestPlatform.Services.Database.Questions.Interfaces;
    using TestPlatform.Services.Database.Questions;
    using TestPlatform.Services.Database.Rooms.Interfaces;
    using TestPlatform.Services.Database.Rooms;
    using TestPlatform.Application.Hubs;

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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews(options =>
            {
                // To escape the global filter [IgnoreAntiforgeryToken]
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                // add custom binder to beginning of collection
                options.ModelBinderProviders.Insert(0, new AbstractSearchModelBinderProvider());
            });

            services.AddSignalR();

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours(8);
                    options.SlidingExpiration = true;
                });

            services.AddSingleton(configuration);

            RegisterRepositories(services);
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always,
            };
            app.UseCookiePolicy(cookiePolicyOptions);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            app.MapHub<ChatHub>("/test-chat");
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IQuestionAnswerMapRepository), typeof(QuestionAnswerMapRepository));
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

            services.AddTransient<ISubjectTagService, SubjectTagService>();
            services.AddTransient<IUserSubjectTagMapService, UserSubjectTagMapService>();
            services.AddTransient<ITestSubjectTagMapService, TestSubjectTagMapService>();

            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IQuestionCopyService, QuestionCopyService>();
            services.AddTransient<IQuestionTypeService, QuestionTypeService>();
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<IQuestionAnswerMapService, QuestionAnswerMapService>();
            services.AddTransient<IQuestionTestMapService, QuestionTestMapService>();

            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<ITestApprovalMapService, TestApprovalMapService>();
            services.AddTransient<ITestUserMapService, TestUserMapService>();

            services.AddTransient<IGradeScaleService, GradeScaleService>();
            services.AddTransient<IGradeScaleTestEvaluationMapService, GradeScaleTestEvaluationMapService>();
            services.AddTransient<ITestEvaluationService, TestEvaluationService>();

            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IRoomParticipantMapService, RoomParticipantMapService>();
            services.AddTransient<IChatMessageService, ChatMessageService>();
        }

        private static void RegisterManagers(IServiceCollection services)
        {
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IQuestionAnswerMananger, QuestionAnswerMananger>();
            services.AddTransient<ISearchPageableMananager, SearchPageableMananager>();
            services.AddTransient<ITestGradeScaleManager, TestGradeScaleManager>();
        }
    }
}
