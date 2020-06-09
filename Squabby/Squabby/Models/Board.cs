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
        
        public List<User> Followers { get; set; }
        
        public List<User> Moderators { get; set; }
        
        public List<Thread> PinnedThreads { get; set; }
        
        public List<Thread> Threads { get; set; }
    }
}