using FakeXiecheng.API.Database;
using FakeXiecheng.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using FakeXiecheng.API.Models;

namespace FakeXiecheng.API
{
    /// <summary>
    /// 项目启动配置
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // 此方法由运行时调用。使用此方法向容器添加服务。管理组件依赖
        public void ConfigureServices(IServiceCollection services)
        {
            //添加服务依赖,连接数据库上下文对象
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            //JWT验证依赖服务注入
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var secretByte = Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]);
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //验证token发布者
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    //验证token持有者
                    ValidateAudience = true,
                    ValidAudience = Configuration["Authentication:Audience"],
                    //token过期
                    ValidateLifetime = true,

                    IssuerSigningKey = new SymmetricSecurityKey(secretByte)
                };
            });
            services.AddControllers(setupAction =>
            {
                //获取或设置一个标志，该标志决定是否在未选择任何格式化程序来设置响应的格式时，将返回 HTTP 406 不可接受的响应 

                /*Accept:text/xml；客户端
                  Content-Type:text/html 发送端
          即代表希望接受的数据类型是xml格式，本次发送端请求发送的数据的数据格式是html。*/
                setupAction.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();

            //services.AddTransient<ITouristRouteRepository, MockTouristRouteRepository>();
            services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();

            //把AppDbContext注入到IOC容器
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(Configuration["DbContext:ConnectionString"]);
            });

            //注入自动映射服务
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// 此方法由运行时调用。使用此方法配置 HTTP 请求管道。
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();//验证

            app.UseAuthorization();//授权

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
