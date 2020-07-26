using AssignmentABB.Extensions;
using AssignmentABB.Helpers;
using AssignmentABB.Middlewares;
using AutoMapper;
using Core.Interface;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssignmentABB
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
            services.AddDbContext<Repository>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddControllers();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddApiBehaviorServicesOnModelStateError();
            services.AddAuthenticationServices(Configuration);
            services.AddSwaggerDocumentationServices();
            services.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy", x =>
                {
                    x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "ABB Assignment API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
