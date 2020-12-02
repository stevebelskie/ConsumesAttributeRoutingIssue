using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication9
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class MyController : ControllerBase
    {
        [HttpGet("users")]
        public Task<IActionResult> Get() => Task.FromResult<IActionResult>(Content("Routed correctly to users"));
    }
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallback(async context =>
                {
                    if (context.Request.Path.StartsWithSegments("/users"))
                    {
                        await context.Response.WriteAsync("Why are we routing here?");
                    }
                    else
                    {
                        await context.Response.WriteAsync("Correctly routed to fallback");
                    }
                });
            });
        }
    }

}
