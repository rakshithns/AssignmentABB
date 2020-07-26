using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentABB.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentationServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ABB Assignment", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                o.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirements = new OpenApiSecurityRequirement{
                    {
                        securitySchema,
                        new [] {"Bearer"}
                    }
                };

                o.AddSecurityRequirement(securityRequirements);
            });
            return services;
        }
    }
}
