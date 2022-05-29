using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddControllers();

            services.AddDbContext<StoreContext>(x =>
                x.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));

            services.AddApplicationServices();

            services.AddSwaggerDocumentation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app/*, IWebHostEnvironment env*/)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            // Вместо этого нашу реализацию Middleware:
            app.UseMiddleware<ExceptionMiddleware>();

            // So in the event that request comes into our API server
            // but we don't have an endpoint that matches particular request.
            // It's going to redirect to our errors controller and pass in the status code.
            // Our errors controller in route [Route("errors/{code}")]
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
