using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using Destiny.Core.Aop.Aop;
using Destiny.Core.Flow.AspNetCore;
using Destiny.Core.Flow.IServices;
using Destiny.Core.Flow.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Destiny.Core.Flow.API
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


            services.AddAppModuleManager<AspNetCoreAppModuleManager>();
            //services.AddScoped<ITest1, Test1>();
            //services.ConfigureDynamicProxy(
            //   config =>
            //   {
            //       List<Type> types = new List<Type>();
            //       //types.Add(typeof(GlobalAopTran));
            //       types.Add(typeof(AopTran));
            //       //config.NonAspectPredicates.AddService("IUnitofWork");//��Ҫ���˵�����Ҫ����ķ����
            //       ///��������ȫ��AOP  ��Ϊ�Ͼ�AOP�õıȽ���  ��ȫ�ֵĻ���̫��
            //       foreach (var item in types)
            //       {
            //           config.Interceptors.AddTyped(item, Predicates.ForNameSpace("Destiny.Core.Flow.Services"),Predicates.ForNameSpace("Destiny.Core.Flow.IServices"));
            //           //һ����������Ҫ����Ĳ���߲���Ҫ����Ĳ㣨����ѡ��һ�ͺã�
            //           //config.Interceptors.AddTyped(item, Predicates.ForNameSpace("TestServices"),Predicates.ForNameSpace(""));//����������ֻ��Ҫ����Ĳ�
            //           //config.NonAspectPredicates.AddService("IUnitofWork");//��Ҫ���˵�����Ҫ����ķ����  
            //           //config.Interceptors.AddTyped(item);
            //       }
            //   });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseErrorHandling();
            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            app.UseAppModule<AspNetCoreAppModuleManager>();

        }
    }
}
