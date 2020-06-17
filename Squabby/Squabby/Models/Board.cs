using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Squabby.Models
{
    public class Board
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Name { get; set; }
        
        [MaxLength(MaxDescriptionLength)] 
        public string Description { get; set; }
        public const int MaxDescriptionLength = 64;
        
        /* Relations */
        [NotNull]
        public User Owner { get; set; }
        
        public ICollection<Thread> Threads { get; set; }
    }
}