using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Squabby.Actions.Core;
using Squabby.Database;
using Squabby.Models;

namespace Squabby.Actions.Actions
{
    public static class RatingAction
    {
        private const int RatingDecrease = 10;
        
        [SquabbyAction(1_800_000)]
        public static async Task RatingUpdate()
        {
            await using var context = new SquabbyContext();
            await context.Database.ExecuteSqlRawAsync($"UPDATE Threads SET Rating = GREATEST(Rating - {RatingDecrease}, {Thread.MinRating});");
        }
    }
}