using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExampleDemo.Core.AuthHelp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using SqlSugarDemo.ORM;

namespace ExampleDemo.Core
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

            #region Swagger
            services.AddSwaggerGen(c=> 
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Version="V1.0",
                    Title="ExampleDemo.Api",
                    Description="实例演示"
                });

                //读取注释
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "ExampleDemo.Core.xml");
                c.IncludeXmlComments(xmlPath,true);

                //添加header验证信息
                //var security = new Dictionary<string, IEnumerable<string>> { { "liang", new string[] { } }, };
                ////添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是liang
                //c.AddSecurityRequirement(security);

                //c.AddSecurityDefinition("liang", new OpenApiSecurityScheme {
                //    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: liang {token}\"",
                //    Name = "Authorization",//jwt默认的参数名称
                //    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                //    Type =SecuritySchemeType.ApiKey,
                //    BearerFormat="JWT",
                //    Scheme="liang"
                //});
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme{
                //           Reference=new OpenApiReference {
                //           Type=ReferenceType.SecurityScheme,
                //           Id="liang" }},new string[]{ }
                //    }
                //});
            });

            #endregion

            #region MemoryCache
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
            #endregion

            #region JWT认证
            //services.AddAuthorization(x => 
            //{
            //    //options.AddPolicy("System", policy => policy.RequireClaim("SystemType").Build());
            //    //options.AddPolicy("Client", policy => policy.RequireClaim("ClientType").Build());
            //    //options.AddPolicy("Admin", policy => policy.RequireClaim("AdminType").Build());


            //});
            #endregion

            services.AddControllers();

            SqlSugarBase._connectionString = Configuration["connectionString"];
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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "帮助文档");
            });
            #endregion

            //app.UseMiddleware<TokenAuth>();

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
