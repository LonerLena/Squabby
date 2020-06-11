using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Squabby.Helpers.Authentication;
using Squabby.Models;

namespace Squabby.Authorization
{
    public class SquabbyAuthorize : Attribute, IAuthorizationFilter
    {
        private UserRole[] _allowedRoles;

        /// <summary>
        /// Accept any logged in user 
        /// </summary>
        public SquabbyAuthorize() => _allowedRoles = new[] {UserRole.Anonymous, UserRole.User, UserRole.Admin}; 
        
        /// <summary>
        /// Accept user with a specific role
        /// </summary>
        /// <param name="role">allowed roles</param>
        public SquabbyAuthorize(params UserRole[] role) => _allowedRoles = role;
        
        /// <summary>
        /// Determines whether user has access to this action
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext.HttpContext.HasLoggedInUser())
            {
                var userRole = filterContext.HttpContext.GetUser().UserRole;
                if(_allowedRoles.All(x => x != userRole)) filterContext.Result = new RedirectResult("/AccessDenied"); 
            }
            else filterContext.Result = new RedirectResult("/login");
        }
    }
}