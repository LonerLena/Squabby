using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Squabby.Models
{
    public class Thread
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Key]
        public short BoardId { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
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

        public int Rating { get; set; }

        /* Relations */
        public Board Board { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
        
        public ICollection<Rating> Ratings { get; set; }
    }
}