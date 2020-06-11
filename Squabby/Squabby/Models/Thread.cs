using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Squabby.Models
{
    public class Thread
    {
        public Thread() => CreationDate = DateTime.Now;

        [Key]
        [Column(Order=1)]
        public Board Board { get; set; }
        
        [Key]
        [Column(Order=2)]
        public int Id { get; set; }

        [NotNull]
        public DateTime CreationDate { get; set; }
        
        [NotNull]
        public User Owner { get; set; }
        
        [NotNull]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }
        public const int MaxTitleLength = 64; 

        [MaxLength(MaxContentLength)]
        public string Content { get; set; }
        public const int MaxContentLength = 5000; 
        
        public ICollection<Comment> Comments { get; set; }
    }
}