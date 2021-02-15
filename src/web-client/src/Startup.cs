using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebClient
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
      services.AddControllersWithViews()
        .AddRazorRuntimeCompilation();

      services.AddAuthentication(options =>
      {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
      })
        .AddCookie("Cookies")
        .AddOpenIdConnect("oidc", options =>
        {
          options.Authority = Configuration["IdentityServerProjectUrl"];

          options.ClientId = "dexframewebclient";
          options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
          options.ResponseType = $"code";

          options.SaveTokens = true;
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
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}"
        )
        /*.RequireAuthorization()*/;
      });
    }
  }
}
