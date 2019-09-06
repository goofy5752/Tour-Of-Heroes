using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TourOfHeroesWebApi.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TourOfHeroesWebApi.Data.Models;

namespace TourOfHeroesWebApi
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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<TourOfHeroesDbContext>(options => options.UseSqlServer("Server=DESKTOP-RC4CRFC\\SQLEXPRESS;Database=Cars;Integrated Security=True;"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            var dbContext = new TourOfHeroesDbContext();
            dbContext.Database.EnsureCreated();

            if (!dbContext.Heroes.Any())
            {
                dbContext.Heroes.Add(new Hero { Name = "Dr Nice" });
                dbContext.Heroes.Add(new Hero { Name = "Narco" });
                dbContext.Heroes.Add(new Hero { Name = "Bombasto" });
                dbContext.Heroes.Add(new Hero { Name = "Celeritas" });
                dbContext.Heroes.Add(new Hero { Name = "Magneta" });
                dbContext.Heroes.Add(new Hero { Name = "RubberMan" });
                dbContext.Heroes.Add(new Hero { Name = "Dynama" });
                dbContext.Heroes.Add(new Hero { Name = "Magma" });
                dbContext.Heroes.Add(new Hero { Name = "Tornado" });
            }

            dbContext.SaveChanges();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(options =>
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
        }
    }
}
