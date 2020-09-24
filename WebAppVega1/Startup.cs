using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using WebAppVega1.Controllers;
using WebAppVega1.Models;
using WebAppVega1.Persistance;
using WebAppVega1.Persistance.Interfaces;
using WebAppVega1.PhotoStorage;
using WebAppVega1.Services;

namespace WebAppVega1
{
    public class Startup
    {
        
        private readonly string _allowSpecificOrigins = "AllowOrigin";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<PhotoSettings>(Configuration.GetSection("PhotoSettings"));
            services.AddScoped(typeof(IRepositoryInterface<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<IPhotoStorage, FileSystemPhotoStorage>();

            services.AddAuthorization(option =>
            {
                option.AddPolicy(AppPolicies.RequireAdminRole, policy => {
                    policy.RequireClaim("https://vega.com/roles","Admin");
                });
            });

            // services.AddDbContext<VegaDbContext>(options=>options.UseSqlServer(Configuration["ConnectionStrings:Default"]));
            services.AddDbContext<VegaDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddCors(c =>
            {
                c.AddPolicy(_allowSpecificOrigins, options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            });



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://vegaexample.auth0.com/";
                options.Audience = "https://api.vega.com";
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(_allowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "uploads")),
                RequestPath = new PathString("/uploads")
            });

            app.UseMvc();
        }

        //public static void Register(HttpConfiguration config)
        //{
        //    EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:4200/fetch", "*", "GET,POST");
        //    config.EnableCors(cors);
        //}
    }
}
