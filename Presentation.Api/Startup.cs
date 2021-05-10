using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using CyberErp.CoreSetting.Core.Service;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Presentation.Api.Providers.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api
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
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc(formatter => formatter.InputFormatters.Insert(0, new RawRequestBodyFormatter()));         

            services.AddMvc().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
                o.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });

            services.AddDbContext<BaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CyberERP")));
            services.AddDbContext<FinancialContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CyberERP")));
            
            services.AddScoped<IRepository, GenericRepository>();
            services.AddScoped<IFinancialRepository, FinancialRepository>();

            services.AddTransient<IFinancialSetupManager, FinancialSetupManager>();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CyberErp - IFMS",
                    Description = "IFMS Application",
                    License = new OpenApiLicense
                    {
                        Name = "Cybersoft PLC",
                        Url = new Uri("http://cybersoft-intl.com"),
                    }
                });
                c.CustomSchemaIds(s => s.FullName);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CyberErp");
                c.EnableValidator();
                c.InjectStylesheet("/swagger-ui-themes/themes/3.x/theme-flattop.css");
            });
            app.UseCors(builder => {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
