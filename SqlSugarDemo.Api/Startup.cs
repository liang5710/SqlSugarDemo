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

                #region xml注释
                //注释显示
                //var file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "SqlSugarDemo.Api.xml");
                c.IncludeXmlComments(xmlPath, true);
                //实体注释显示
                var xmlModelPath = Path.Combine(basePath, "SqlSugarDemo.Model.xml");
                c.IncludeXmlComments(xmlModelPath);
                #endregion

                #region 启用Swagger Jwt验证
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
                {
                    In=ParameterLocation.Header,
                    Type=SecuritySchemeType.ApiKey,
                    Description="在下框中输入请求头中需要添加Jwt授权Token:Bearer Token",
                    Name="Authorization",
                    BearerFormat="JWT",
                    Scheme="Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement 
                {
                    { 
                        new OpenApiSecurityScheme{ 
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },new string[]{ }
                    }
                });
                #endregion
            });
            #endregion

            #region JWT 认证

            JwtSettings jwtSettings = new JwtSettings();
            services.Configure<JwtSettings>(Configuration.GetSection(nameof(JwtSettings)));
            Configuration.GetSection("JwtSettings").Bind(jwtSettings);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        ValidateLifetime=true,
                        //用于签名验证
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecurityKey)),
                        ValidateIssuer = false,
                        ValidateAudience=false
                            
                    };
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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
            });
            #endregion

            app.UseRouting();
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
