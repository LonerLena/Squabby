using System.Collections.Generic;

namespace Squabby.Models
{
    public class Thread
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Content { get; set; }

        public Board Board { get; set; }

        public List<Rating> Ratings { get; set; }

        public List<Comment> Comments { get; set; }
    }
}