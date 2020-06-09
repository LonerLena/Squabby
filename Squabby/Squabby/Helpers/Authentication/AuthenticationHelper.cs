using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Squabby.Models;

namespace Squabby.Helpers.Authentication
{
    public static class AuthenticationHelper
    {
        /// <summary>
        /// Get current logged in user from session
        /// </summary>
        /// <param name="context"></param>
        /// <returns>user or null when not logged in</returns>
        public static User GetUser(this HttpContext context)
        {
            var accountBytes = context.Session.Get("user"); 
            if (accountBytes != null) return JsonSerializer.Deserialize<User>(accountBytes);
            else return null;
        }

        /// <summary>
        /// Set user for current session
        /// </summary>
        /// <param name="context"></param>
        /// <param name="user"></param>
        public static void SetUser(this HttpContext context, User user) =>
            context.Session.Set("user", JsonSerializer.SerializeToUtf8Bytes(user));

        /// <summary>
        /// Determines whether current session has a logged in user 
        /// </summary>
        /// <param name="context"></param>
        /// <returns>user or null when not logged in</returns>
        public static bool HasLoggedInUser(this HttpContext context) => context.Session.Get("user") != null;

        /// <summary>
        /// Logout current user
        /// </summary>
        /// <param name="context"></param>
        public static void LogoutUser(this HttpContext context) =>
            context.Session.Remove("user");
    }
}