using System.ComponentModel.DataAnnotations;

namespace Squabby.Models
{
    public class Rating
    {
        [Key]
        public int BoardId { get; set; }
        
        [Key]
        public int ThreadId { get; set; }

        [Key]
        public int UserId { get; set; }

        public short Value { get; set; }
        
        /* Relations */
        public User Owner { get; set; }

        public Thread Thread { get; set; }
    }
}