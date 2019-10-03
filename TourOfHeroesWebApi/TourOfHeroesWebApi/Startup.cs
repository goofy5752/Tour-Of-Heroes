using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TourOfHeroesData;
using TourOfHeroesData.Common;
using TourOfHeroesData.Common.Contracts;
using TourOfHeroesData.Seeder;
using TourOfHeroesData.Seeder.Contracts;
using TourOfHeroesDTOs;
using TourOfHeroesServices;
using TourOfHeroesServices.Contracts;
using TourOfHeroesServices.Mapping;

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

            services.AddDbContext<TourOfHeroesDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddTransient<ISeeder, Seeder>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IHeroService, HeroService>();
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ISeeder seeder)
        {
            AutoMapperConfig.RegisterMappings(typeof(PageResultDTO<>).GetTypeInfo().Assembly);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            seeder.SeedDatabase();

            app.UseHttpsRedirection();
            app.UseCors(options =>
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }
    }
}
