using System;
using System.Collections.Generic;

namespace Squabby.Models
{
    public class Thread
    {
        public Thread() => creationDate = DateTime.Now; 
        
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Content { get; set; }

        public DateTime creationDate { get; set; }
        
        public virtual User Owner { get; set; }
        
        public virtual Board Board { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}