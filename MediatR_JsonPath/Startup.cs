using MediatR;
using MediatR_JsonPath.Data;
using MediatR_JsonPath.Domain.Contracts;
using MediatR_JsonPath.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MediatR_JsonPath
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); 
            services.AddRazorPages();
            services.AddMediatR(typeof(Startup));
            services.AddMediatorHandlers(typeof(Startup).Assembly);
            services.AddSingleton(AutoMapperConfiguration.RegisterMappings().CreateMapper());
            services.AddSwaggerDoc();
            services.AddSingleton<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IJsonMapper, JsonMapper>();
            services.AddControllersWithViews().AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Sample Api");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
    public static class MediatorExtensions
    {
        public static IServiceCollection AddMediatorHandlers(this IServiceCollection services, Assembly assembly)
        {
            var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);
            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                    services.AddScoped(handlerType.AsType(), type.AsType());
            }
            return services;
        }
    }
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "Sample Api", Version = "v1" });
                var definition = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor, insira no campo a palavra 'Bearer', seguida por espaço e JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                };
                c.AddSecurityDefinition("Bearer", definition);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                      },
                      new List<string>()
                    }
                });
            });
            return services;
        }
    }
}
