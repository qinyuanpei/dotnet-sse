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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace DotNet_SSE
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //CORS策略：简单粗暴一刀流
            services.AddCors(opt=>{
                opt.AddPolicy("AllowAll", builder => {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });

            //CORS策略：允许指定域
            services.AddCors(opt=>{
                opt.AddPolicy("AllowOne", builder => {
                    builder.WithOrigins("http://localhost:8888")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("X-ASP-NET-Core","X-UserName")
                        .AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

             //CORS策略：简单粗暴一刀流
            //app.UseCors("AllowAll");

            //CORS策略：允许指定域
            //app.UseCors("AllowOne");

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
