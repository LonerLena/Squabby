namespace Squabby.Models
{
    public class Rating
    {
        public int Id { get; set; }

        public Account Account { get; set; }
        
        public int UserRating { get; set; }
    }
}