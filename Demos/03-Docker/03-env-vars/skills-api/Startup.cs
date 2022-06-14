using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace SkillsApi {
    public class Startup {
        private readonly IWebHostEnvironment env;
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Configuration = configuration;
            env = environment;
        }

        public void ConfigureServices (IServiceCollection services) {
            //Config
            services.AddSingleton<IConfiguration>(Configuration);
            var cfg = Configuration.Get<SkillsConfig>();

            //EF
            if (cfg.UseSQLite)
            {
                services.AddDbContext<SkillDBContext>(opts => opts.UseSqlite(cfg.ConnectionStrings.SQLiteDBConnection));
            }
            else
            {
                Console.WriteLine("using con: " + cfg.ConnectionStrings.SQLServerDBConnection);
                services.AddDbContext<SkillDBContext>(opts => opts.UseSqlServer(cfg.ConnectionStrings.SQLServerDBConnection));
            }

            // Cors
            // Cors
            services.AddCors(o => o.AddPolicy("nocors", builder =>
                {
                    builder
                        .SetIsOriginAllowed(host => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                }));

            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new OpenApiInfo { Title = "Skills API", Version = "v1" });
            });

            services.AddControllers ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, ILoggerFactory loggerFactory) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            if (env.IsDevelopment ()) {
                app.UseStaticFiles (new StaticFileOptions {
                    OnPrepareResponse = context => {
                        context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                        context.Context.Response.Headers["Pragma"] = "no-cache";
                        context.Context.Response.Headers["Expires"] = "-1";
                    }
                });
            } else { app.UseStaticFiles (); }

            //Swagger
            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Skills API");
                c.RoutePrefix = string.Empty;
            });

            //Cors
            app.UseCors ("nocors");
            app.UseHttpsRedirection ();
            app.UseRouting ();

            // app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}