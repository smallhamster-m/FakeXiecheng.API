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
    /// ��Ŀ��������
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // �˷���������ʱ���á�ʹ�ô˷�����������ӷ��񡣹����������
        public void ConfigureServices(IServiceCollection services)
        {
            //��ӷ�������,�������ݿ������Ķ���
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            //JWT��֤��������ע��
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var secretByte = Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]);
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //��֤token������
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    //��֤token������
                    ValidateAudience = true,
                    ValidAudience = Configuration["Authentication:Audience"],
                    //token����
                    ValidateLifetime = true,

                    IssuerSigningKey = new SymmetricSecurityKey(secretByte)
                };
            });
            services.AddControllers(setupAction =>
            {
                //��ȡ������һ����־���ñ�־�����Ƿ���δѡ���κθ�ʽ��������������Ӧ�ĸ�ʽʱ�������� HTTP 406 ���ɽ��ܵ���Ӧ 

                /*Accept:text/xml���ͻ���
                  Content-Type:text/html ���Ͷ�
          ������ϣ�����ܵ�����������xml��ʽ�����η��Ͷ������͵����ݵ����ݸ�ʽ��html��*/
                setupAction.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();

            //services.AddTransient<ITouristRouteRepository, MockTouristRouteRepository>();
            services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();

            //��AppDbContextע�뵽IOC����
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(Configuration["DbContext:ConnectionString"]);
            });

            //ע���Զ�ӳ�����
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// �˷���������ʱ���á�ʹ�ô˷������� HTTP ����ܵ���
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

            app.UseAuthentication();//��֤

            app.UseAuthorization();//��Ȩ

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
