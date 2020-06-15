using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Squabby.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(MaxUsernameLength)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Username { get; set; }
        public const int MaxUsernameLength = 32;
        
        public string Password { get; set; }

        [NotNull]
        public UserRole UserRole { get; set; }
        
        [StringLength(TokenLength)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public String Token { get; set; }
        public const int TokenLength = 64;
        
        /* Relations */
        public ICollection<Rating> Ratings { get; set; }
    }

    public enum UserRole
    {
        Admin, User, Anonymous
    }
}