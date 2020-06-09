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
        
        public Account Owner { get; set; }
        
        public List<Account> Followers { get; set; }
        
        public List<Account> Moderators { get; set; }
        
        public List<Thread> PinnedThreads { get; set; }
        
        public List<Thread> Threads { get; set; }
    }
}