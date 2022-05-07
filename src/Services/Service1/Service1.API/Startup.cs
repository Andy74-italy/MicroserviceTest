using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Service1.API.Controllers;
using Service1.API.Data;
using Service1.API.Repositories;
using Services.Contracts.Data;
using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

namespace Service1.API
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service1.API", Version = "v1" });
            });

            services.AddScoped<IDBContext, DBContext>();
            foreach (string file in Directory.EnumerateFiles(".\\bin\\Entities\\", "*.dll"))
            {
                var assembly = Assembly.LoadFrom(file);
                foreach (Type type in assembly.GetExportedTypes())
                {
                    if (type.IsAssignableTo(typeof(IEntity)))
                    {
                        services.AddScoped(typeof(IEntity), type);

                        var inttype = typeof(IEntityRepository<>).MakeGenericType(type);
                        var enttype = typeof(EntityRepository<>).MakeGenericType(type);
                        services.AddScoped(inttype, enttype);
                        var servicecontroltype = typeof(Service1Controller<>).MakeGenericType(type);
                        var registerclass = ClassCreation(servicecontroltype, inttype, services, type.Name);
                        // services.AddScoped(servicecontroltype, registerclass);
                    }
                }
            }
        }

        private Type ClassCreation(Type t, Type arg1, IServiceCollection services, string name)
        {
            var moduleName = $"Service{name}";
            var newTypeName = $"Service1.API.Controllers.{moduleName}";
            var assemblyName = new AssemblyName(newTypeName);
            var dynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var dynamicModule = dynamicAssembly.DefineDynamicModule(moduleName);
            var dynamicType = dynamicModule.DefineType(newTypeName, TypeAttributes.Public | TypeAttributes.Class, t);

            var constructor = dynamicType.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { arg1, typeof(ILogger<>).MakeGenericType(t) });
            var ilGenerator = constructor.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Ldarg_2);
            ilGenerator.Emit(OpCodes.Call, t.GetConstructors()[0]);
            ilGenerator.Emit(OpCodes.Nop);
            ilGenerator.Emit(OpCodes.Nop);
            ilGenerator.Emit(OpCodes.Ret);

            var created = dynamicType.CreateType();
            services.AddMvc().AddApplicationPart(dynamicAssembly).AddControllersAsServices();
            return created;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service1.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
