using Autofac;
using Autofac.Extensions.DependencyInjection;
using ClinicaMedica.Context;
using ClinicaMedica.Repositories;
using ClinicaMedica.Service;
using ClinicaMedica.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace ClinicaMedica
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
      services.AddControllersWithViews();
      // In production, the Angular files will be served from this directory
      services.AddSpaStaticFiles(configuration =>
      {
        configuration.RootPath = "ClientApp/dist";
      });

      services.AddEntityFrameworkSqlServer()
        .AddDbContext<BaseContext>(options =>
        {
          options.UseSqlServer("Data Source=localhost\\SQL;Initial Catalog=ClinicaMedicaGrugel;User ID=sa;Password=rm;",
            b =>
            {
              b.MigrationsAssembly("ClinicaMedica");
            });
        });

      services.AddScoped<IProfissionalService, ProfissionalService>();
      services.AddScoped<IProfissionalRepository, ProfissionalRepository>();
      services.AddScoped<IPessoaService, PessoaService>();
      services.AddScoped<IPessoaRepository, PessoaRepository>();

      //var builder = new ContainerBuilder();
      //builder.Populate(services);
      //builder.RegisterType<ProfissionalService>().As<IProfissionalService>();
      //builder.RegisterType<ProfissionalRepository>().As<IProfissionalRepository>();
      //builder.RegisterType<PessoaService>().As<IPessoaService>();
      //builder.RegisterType<PessoaRepository>().As<IPessoaRepository>();
      //builder.RegisterAssemblyTypes(typeof(Repository<>).GetTypeInfo().Assembly)
      //          .AsClosedTypesOf(typeof(IRepository<>));

      //var container = builder.Build();
      //return new AutofacServiceProvider(container);
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
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      if (!env.IsDevelopment())
      {
        app.UseSpaStaticFiles();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller}/{action=Index}/{id?}");
      });

      app.UseSpa(spa =>
      {
              // To learn more about options for serving an Angular SPA from ASP.NET Core,
              // see https://go.microsoft.com/fwlink/?linkid=864501

              spa.Options.SourcePath = "ClientApp";

        if (env.IsDevelopment())
        {
          spa.UseAngularCliServer(npmScript: "start");
        }
      });
    }
  }
}
