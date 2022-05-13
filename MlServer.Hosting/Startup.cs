using Microsoft.EntityFrameworkCore;
using MlServer.Database;
using MlServer.Hosting.Services;
using MlServer.Services.Extensions;

namespace MlServer.Hosting {
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddDbConnection(Configuration);
            services.AddApplicationServices();
            services.AddRetrainBackgroundService();
        }
     
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
     
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();
            });
        }

        
    }
}