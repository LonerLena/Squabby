using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Squabby.Models
{
    public class Board
    {
        [Key]
        public string Name { get; set; }
        
        public string Rules { get; set; }
        
        public string Description { get; set; }
        
        public User Owner { get; set; }
        
        public ICollection<Thread> Threads { get; set; }
    }
}