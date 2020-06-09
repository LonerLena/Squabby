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
        
        public Board Board { get; set; }

        public List<Rating> Ratings { get; set; }

        public List<Comment> Comments { get; set; }
    }
}