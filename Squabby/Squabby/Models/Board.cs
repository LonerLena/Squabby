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
        
        public virtual User Owner { get; set; }
        
        public virtual ICollection<Thread> Threads { get; set; }
    }
}