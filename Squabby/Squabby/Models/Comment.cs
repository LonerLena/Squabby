using System.Collections.Generic;

namespace Squabby.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public ICollection<Comment> Reactions { get; set; }
        
        public ICollection<Rating> Ratings { get; set; }
    }
}