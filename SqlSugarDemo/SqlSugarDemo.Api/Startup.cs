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
                    Description="ʵ����ʾ"
                });

                //��ȡע��
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "ExampleDemo.Core.xml");
                c.IncludeXmlComments(xmlPath,true);

                //���header��֤��Ϣ
                //var security = new Dictionary<string, IEnumerable<string>> { { "liang", new string[] { } }, };
                ////���һ�������ȫ�ְ�ȫ��Ϣ����AddSecurityDefinition����ָ���ķ�������Ҫһ�£�������liang
                //c.AddSecurityRequirement(security);

                //c.AddSecurityDefinition("liang", new OpenApiSecurityScheme {
                //    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) �����ṹ: \"Authorization: liang {token}\"",
                //    Name = "Authorization",//jwtĬ�ϵĲ�������
                //    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
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

            #region JWT��֤
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "�����ĵ�");
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
