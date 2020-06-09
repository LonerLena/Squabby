using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Squabby.Models;

namespace Squabby.Helpers.Authentication
{
    public static class AuthenticationHelper
    {
        /// <summary>
        /// Get current logged in account from session
        /// </summary>
        /// <param name="session"></param>
        /// <returns>account or null when not logged in</returns>
        public static Account GetAccount(this ISession session)
        {
            var accountBytes = session.Get("account"); 
            if (accountBytes != null) return JsonSerializer.Deserialize<Account>(accountBytes);
            else return null;
        }

        /// <summary>
        /// Set account for current session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="account"></param>
        public static void SetAccount(this ISession session, Account account) =>
            session.Set("account", JsonSerializer.SerializeToUtf8Bytes(account));
        
        /// <summary>
        /// Determines whether current session has a logged in user 
        /// </summary>
        /// <param name="session"></param>
        /// <returns>account or null when not logged in</returns>
        public static bool IsLoggedIn(this ISession session) => session.Get("account") != null;

        /// <summary>
        /// Logout current account
        /// </summary>
        /// <param name="session"></param>
        public static void LogoutAccount(this ISession session) =>
            session.Remove("account");
    }
}