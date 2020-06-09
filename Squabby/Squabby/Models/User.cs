using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Squabby.Models
{
    public class User
    {
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [NotNull]
        public string Username { get; set; }
        [NotNull]
        public string Password { get; set; }

        [NotNull]
        public Role Role { get; set; }

        public ICollection<Board> Boards { get; set; }
        
        public ICollection<Thread> Threads { get; set; }
    }

    public enum Role
    {
        Admin, User
    }
}