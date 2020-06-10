using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.OpenApi.Models;
using SqlSugarDemo.ORM;

namespace SqlSugarDemo.Api
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
            services.AddControllers();

            BaseDBConfig.ConnectionString = Configuration.GetSection("ConnectionString").Value;

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "帮助文档",
                    Version = "v1",
                    Description = "API文档描述",
                    Contact = new OpenApiContact
                    {
                        Email = "sss",
                        Name = "ddd",
                        Url = new Uri("http://www.netcore.pub")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "许可证",
                        Url = new Uri("http://www.netcore.pub")
                    }
                });
                //注释显示
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "SqlSugarDemo.Api.xml");
                c.IncludeXmlComments(xmlPath, true);
                //实体注释显示
                var xmlModelPath = Path.Combine(basePath,"SqlSugarDemo.Model.xml");
                c.IncludeXmlComments(xmlModelPath);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
            });
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
