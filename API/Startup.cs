using System.IO;
using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // Конфигурация Sqlite
            services.AddDbContext<StoreContext>(x =>
                x.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));
            

            services.AddDbContext<AppIdentityDbContext>(x => {
                x.UseSqlite(_configuration.GetConnectionString("IdentityConnection"));
            });

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            // Конфигурация MySql
            services.AddDbContext<StoreContext>(x =>
                x.UseMySql(_configuration.GetConnectionString("DefaultConnection")));
            

            services.AddDbContext<AppIdentityDbContext>(x => {
                x.UseMySql(_configuration.GetConnectionString("IdentityConnection"));
            });

            ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddControllers();

            // Setting up Redis
            services.AddSingleton<IConnectionMultiplexer>(c => {
                var configuration = ConfigurationOptions.Parse(_configuration
                    .GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });


            services.AddApplicationServices();

            services.AddIdentityServices(_configuration);

            services.AddSwaggerDocumentation();

            // Чтобы выполнять запросы из приложения с одного адреса (или домена) к приложению, 
            // которое размещено по другому адресу.
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app/*, IWebHostEnvironment env*/)
        {
            // Собственная реализацию Middleware вместо app.UseDeveloperExceptionPage()
            app.UseMiddleware<ExceptionMiddleware>();

            // So in the event that request comes into our API server
            // but we don't have an endpoint that matches particular request.
            // It's going to redirect to our errors controller and pass in the status code.
            // Our errors controller in route [Route("errors/{code}")]
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();
            // Новое расположение StaticFiles
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Content")
                ), RequestPath = "/content"
            });

            // Используется до авторизации:
            app.UseCors("CorsPolicy");

            app.UseAuthentication(); // Добавили после настройки Token и ДО авторизации
            app.UseAuthorization();

            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
