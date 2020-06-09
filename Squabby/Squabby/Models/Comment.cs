using System.Collections.Generic;

namespace Squabby.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public List<Comment> Reactions { get; set; }
        
        public List<Rating> Ratings { get; set; }
    }
}