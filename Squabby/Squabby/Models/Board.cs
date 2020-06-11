using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Squabby.Models
{
    public class Board
    {
        [Key]
        public string Name { get; set; }
        
        [MaxLength(MaxDescriptionLength)] 
        public string Description { get; set; }
        public const int MaxDescriptionLength = 64;
        
        [NotNull]
        public User Owner { get; set; }
        
        public ICollection<Thread> Threads { get; set; }
    }
}