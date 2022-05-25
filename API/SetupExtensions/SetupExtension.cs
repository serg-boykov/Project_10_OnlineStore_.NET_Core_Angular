using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using System.IO;
using System;

namespace API.SetupExtensions
{
    public static class SetupExtension
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services) => services
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

        public static IApplicationBuilder UseSwaggerApp(this IApplicationBuilder app) => app
            .UseSwagger()
            .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
    }
}
