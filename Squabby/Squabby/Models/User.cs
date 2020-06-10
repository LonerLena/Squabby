using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Squabby.Models
{
    public class User
    {
        /// <summary>
        /// Primary key of user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique username of user
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Username { get; set; }
        
        /// <summary>
        /// Hash of user password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        [NotNull]
        public Role Role { get; set; }
        
        /// <summary>
        /// User token, used for login by anonymous users
        /// </summary>
        public const int TokenLength = 64;
        [StringLength(TokenLength)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public String Token { get; set; }

    }

    /// <summary>
    /// All user roles
    /// </summary>
    public enum Role
    {
        Admin, User, Anonymous
    }
}