using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Grpc.Core;
using Winner.OAuth.GrpcService.Controllers;
using Microsoft.AspNetCore.Http;

namespace Winner.OAuth.GrpcService
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
            //services.AddControllers();
            services.AddGrpc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.Map("/health", app =>
            {
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("OK");
                });
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<TokenService>();
                endpoints.MapGrpcService<AuthorizeService>();
                endpoints.MapGrpcService<LoginService>();
                endpoints.MapGrpcService<PasswordService>();
                endpoints.MapGrpcService<RegisterService>();
                endpoints.MapGrpcService<SmsService>();
                //endpoints.MapControllers();
            });
        }
    }
}
