using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Squabby.Helpers.Authentication;
using Squabby.Models;

namespace Squabby.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// HashSet with PathStrings that can only be accessed by admins
        /// </summary>
        private readonly HashSet<PathString> _adminOnlyUrls = new HashSet<PathString>{ "/admin" };
        
        /// <summary>
        /// HashSet with PathStrings that can only be accessed by users
        /// </summary>
        private readonly HashSet<PathString> _userOnlyUrls = new HashSet<PathString>{  };
        
        public AuthenticationMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext c)
        {
            if (!c.HasLoggedInUser())
            {
                if (_userOnlyUrls.ContainsPath(c.Request.Path) || _adminOnlyUrls.ContainsPath(c.Request.Path))
                    c.Response.Redirect("/login");
                else await _next(c);
                return;
            }
            
            if(_adminOnlyUrls.ContainsPath(c.Request.Path) && c.GetUser().Role != Role.Admin) return;
            else await _next(c);
        }
    }

    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseSquabbyAuthentication(this IApplicationBuilder builder)
            => builder.UseMiddleware<AuthenticationMiddleware>();

        public static bool ContainsPath(this HashSet<PathString> set, PathString pathString) =>
            set.Any(pathString.StartsWithSegments);
    }
}