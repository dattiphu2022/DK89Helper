using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http.Abstractions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK89.EntityFramework
{
    public static class DI
    {
        public static void AddGetUserInfo(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
        }
    }
}
