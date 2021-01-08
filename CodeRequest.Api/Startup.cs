using AutoMapper;
using Calculator.Api.Domain.MappingProfiles;
using Calculator.Persistence;
using Calculator.Services;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Calculator.Api
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
         services.AddControllers();

         services.AddAutoMapper(typeof(CalculatorJobProfile));

         services.AddTransient<ICalculatorService, CalculatorService>();

         services.AddDbContext<CalculatorDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("CalculatorDBContext")));

         services.AddHangfire(config =>
         {
            config.UseMemoryStorage();
         });

         services.AddSwaggerGen(gen =>
         {
            gen.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Calculator API", Version = "v1.0" });
         });

      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseHangfireDashboard();
         app.UseHangfireServer();

         app.UseHttpsRedirection();
         app.UseRouting();
         app.UseAuthorization();

         app.UseSwagger();
         app.UseSwaggerUI(ui =>
         {
            ui.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Calculator API Endpoint");
         });

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
