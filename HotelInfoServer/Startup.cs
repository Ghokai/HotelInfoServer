using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HotelInfoServer.AssemblyLoaders;
using HotelInfoServer.Managers;
using HotelInfoServer.ModelValidators;
using HotelInfoServer.Models;
using HotelInfoServer.CustomMiddlewares;

namespace HotelInfoServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Enabling Cors because of client app will be served in different port
            //instead of allowing any origin we can set specific client application url
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc();

           
            IDynamicLoader dynamicLoader = new DynamicLoader(Configuration);
            string fileManagerTypeString = Configuration["FileManagerType"];
            Type fileManagerType = dynamicLoader.GetTypeFromName(fileManagerTypeString);

            //dynamicly injecting service (example:HotelInfoFileManagerCSV) from configuration(appsettings.json) at run time 
            services.AddScoped(typeof(IHotelInfoFileManager), fileManagerType);

            //injection dynamicLoader for further runtime assembly loading & injecting
            services.AddScoped<IDynamicLoader, DynamicLoader>();

            //configurable model validator for adding or removing validation types at runtime from appsettings.json
            services.AddScoped<IGenericModelValidator<HotelInfo>, GenericModelValidator<HotelInfo>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");        
            //Middleware for Error Management With Custom Exception Types
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }
    }
}
