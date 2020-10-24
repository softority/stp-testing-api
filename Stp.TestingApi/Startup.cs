using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using Stp.Data;
using System.Text.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Stp.TestingApi
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
            services.AddCors(); // Make sure you call this previous to AddMvc

            services.AddControllers();

            services.AddDbContext<TestingDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), (opt) => opt.CommandTimeout(10)));
                services.AddControllers();
                services.AddOpenApiDocument(document =>
                    document.PostProcess = d => d.Info.Title = "STP API");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        var exception = exceptionHandlerPathFeature.Error;

                        var problemDetails = new ProblemDetails
                        {
                            Detail = exception.Message,
                            Instance = context.Request.GetEncodedPathAndQuery(),
                            Status = 500,
                            Type = exception.GetType().FullName,
                        };

                        problemDetails.Extensions.Add("stack", exception.ToString());

                        var result = JsonSerializer.Serialize(problemDetails);

                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 500;

                        await context.Response.WriteAsync(result);
                    });
                });
                app.UseHsts();
            }
            app.UseCors(new Action<Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder>((x) => { x.AllowAnyOrigin(); x.AllowAnyMethod();x.AllowAnyHeader(); }));

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
