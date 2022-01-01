using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Marketing
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
            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(15);
            });

            // login with google and facebook
            //services.AddAuthentication()
            //   .AddFacebook(options =>
            //   {
            //       options.AppId = Configuration["App:FacebookClientId"];
            //       options.ClientSecret = Configuration["App:FacebookClientSecret"];
            //   })

            //   .AddGoogle(options =>
            //   {
            //       options.ClientId = Configuration["App:GoogleClientId"];
            //       options.ClientSecret = Configuration["App:GoogleClientSecret"];
            //   });
//=============================================================================================

                   services.AddControllersWithViews();
                  services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);


            services.AddDbContext<MyDbcontext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("SQLConn"));
            });

          

        }
       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();
            //app.UseAuthentication(
                
            //    );
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
