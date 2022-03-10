using EmployeeManagement.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
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
      services.AddDbContextPool<AppDbContext>(
             options => options.UseSqlServer(Configuration.GetConnectionString("EmployeeDBConnection")));
      
      services.AddIdentity<ExtendIdentity, IdentityRole>(options => { 
      options.Password.RequiredLength = 8;
      options.Password.RequiredUniqueChars = 2;
      }).AddEntityFrameworkStores<AppDbContext>();

      services.AddMvc(options => {
        options.EnableEndpointRouting = false;
        var policy = new AuthorizationPolicyBuilder()
          .RequireAuthenticatedUser()
          .Build();
        options.Filters.Add(new AuthorizeFilter(policy));
        });

      services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>() ;
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
        app.UseStatusCodePagesWithReExecute("/Error/{0}");
      }
      app.UseStaticFiles();
      app.UseAuthentication();
      app.UseMvc(route =>
        route.MapRoute("default","{controller=Home}/{action=Index}/{id?}")
      );
      //app.Run(Route => Route.Response.WriteAsync("Please Check URL"));
    }
  }
}
