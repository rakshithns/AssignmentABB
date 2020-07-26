using AssignmentABB.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentABB.Extensions
{
    public static class ApiBehaviorServicesForModelStateError
    {
        public static IServiceCollection AddApiBehaviorServicesOnModelStateError(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(o=> {
                o.InvalidModelStateResponseFactory = actionContext => {
                    var errors = actionContext.ModelState.Where(x => x.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });            
            return services;
        }
    }
}
