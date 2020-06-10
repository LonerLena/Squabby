using System;

namespace Squabby.Models
{
    public class Comment
    {
        public Comment() => CreationDate = DateTime.Now; 
        
        public int Id { get; set; }

        public string Content { get; set; }
        
        public DateTime CreationDate { get; set; }
    }
}