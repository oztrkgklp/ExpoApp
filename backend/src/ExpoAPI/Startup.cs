using ExpoAPI.Api.Infrastructure.Repositories;
using ExpoAPI.Infrastructure.Behaviors;
using ExpoAPI.Infrastructure.Middlewares;
using ExpoAPI.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;

namespace ExpoAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning((config) =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            services.AddMvc().AddMvcOptions((opt) => opt.EnableEndpointRouting=false);
            services.AddScoped<IExpoAPIQueryRepository, ExpoAPIQueryRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ExpoAPI", Version = "v1.0"});
            });

            services.AddMediatR(typeof(Startup));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddDapperPolly();
            services.AddCrypto();
        }

        public void Configure(IApplicationBuilder app)
        {            
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExpoAPI v1"); });

            app.UseHttpsRedirection();
            app.UseMiddleware<SecurityHeadersMiddleware>();
            app.UseRouting();
            app.UseMvc();
            app.UseSerilogRequestLogging();
        }
    }
}