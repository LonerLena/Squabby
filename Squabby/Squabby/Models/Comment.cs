using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Squabby.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Key]
        public int BoardId { get; set; }
        
        [Key]
        public int ThreadId { get; set; }
        
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }

        [NotNull]
        [MaxLength(MaxContentLength)]
        public string Content { get; set; }
        public const int MaxContentLength = 5000;
        
        /* Relations */
        [NotNull]
        public User Owner { get; set; }
        
        public Thread Thread { get; set; }
    }
}