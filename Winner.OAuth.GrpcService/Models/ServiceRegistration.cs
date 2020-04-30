using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winner.OAuth.GrpcService.Models
{
    public static class ServiceRegistration
    {
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app,IApplicationLifetime lifetime,IOptions<object> healthOption,IOptions<object> consulOption)
        {
            //var consulClient = new ConsulClient();
            return app;
        }
    }
}
