
using AdventureServer.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using AdventureServer.Services.AdventureFramework;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AdventureServer.Services;
using Microsoft.OpenApi.Models;

namespace AdventureServer
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

            services.AddCors(o => o.AddPolicy("CORSPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                      
            }));

            services.AddMvc(options => options.EnableEndpointRouting = false)
                .AddControllersAsServices(); // this adds the controllers as services to all for DI to resolve them 

            services.AddSession();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Adventure Server", Version = "v1" });
            });

            


            // adventure sever framework
            services.AddSingleton<IPlayAdventure, AdventureFrameworkService>();
            services.AddTransient<IAppVersionService, AppVersionService>();
            services.AddSingleton<IGetFortune, GetFortuneService>();
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
  
            app.UseDeveloperExceptionPage(); // Since this is learning experince - expose this page
            app.UseHttpsRedirection(); // Encourage HTTPS
            
            app.UseCors("CORSPolicy");

            app.UseSwagger();
            app.UseSwaggerUI(c => {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Adventure Server API v1");

                });

            // app.UseRouting(); // Future
            // app.UseAuthorization();

            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc();
        }
    }
}
