using EmployeeManagement.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
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
      services.AddMvc(options => options.EnableEndpointRouting = false);
      services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>() ;
      services.AddRazorPages();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> Logger)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseStaticFiles();
      //app.UseMvcWithDefaultRoute();
      app.UseMvc(route =>
      {
        route.MapRoute("default","/{controller=Home}/{action=Index}/{id?}");
      });      


      /*app.Use(async (context, Next) =>
      {
        Logger.LogInformation("From 1st Log");
        await Next();
        Logger.LogInformation("From 2nd");
      });*/

      app.Run(async (context) =>
      {
        await context.Response.WriteAsync(" Please Check URL");
      });
    }
  }
}
