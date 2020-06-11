using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Squabby.Models
{
    public class Comment
    {
        public Comment() => CreationDate = DateTime.Now;

        [Key]
        [Column(Order=1)]
        public Thread Thread { get; set; }
        
        [Key]
        [Column(Order=2)]
        public int Id { get; set; }

        [NotNull]
        public User Owner { get; set; }
        
        [NotNull]
        public DateTime CreationDate { get; set; }

        [NotNull] 
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }
        public const int MaxTitleLength = 64;
        
        [NotNull]
        [MaxLength(MaxContentLength)]
        public string Content { get; set; }
        public const int MaxContentLength = 5000;
    }
}