using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Squabby.Models
{
    public class Account
    {
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [NotNull]
        public string Username { get; set; }
        [NotNull]
        public string Password { get; set; }

        [NotNull]
        public Role Role { get; set; }

        public List<Board> Boards { get; set; }
        
        public List<Thread> Threads { get; set; }
    }

    public enum Role
    {
        Admin, User
    }
}