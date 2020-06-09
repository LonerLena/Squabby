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
        
        public ICollection<User> Followers { get; set; }
        
        public ICollection<User> Moderators { get; set; }
        
        public ICollection<Thread> PinnedThreads { get; set; }
        
        public ICollection<Thread> Threads { get; set; }
    }
}