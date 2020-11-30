using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SqlSugarDemo.Api.JwtAuth;
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

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //var assemblysServices = Assembly.Load("SqlSugarDemo.Service");
            //builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();

            //var assemblysRepository = Assembly.Load("SqlSugarDemo.Repository");
            //builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(x => x.Name.EndsWith("service",StringComparison.OrdinalIgnoreCase)).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(x => x.Name.EndsWith("repository", StringComparison.OrdinalIgnoreCase)).AsImplementedInterfaces();
        }

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
                var xmlModelPath = Path.Combine(basePath, "SqlSugarDemo.Model.xml");
                c.IncludeXmlComments(xmlModelPath);
            });
            #endregion

            #region jwt

            services.Configure<JwtOptions>(Configuration.GetSection(nameof(JwtOptions)));

            services.AddJwtBearer(Configuration.GetValue<string>("JwtOptions:SecurityKey"));
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options=> 
            //{
            //    options.SaveToken = true;
            //    options.RequireHttpsMetadata = false;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidAudience="",
            //        ValidIssuer="",
            //        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(""))
            //    };
            //});
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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
            });
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
