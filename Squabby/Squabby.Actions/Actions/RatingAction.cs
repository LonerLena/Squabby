using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Squabby.Actions.Core;
using Squabby.Database;
using Squabby.Models;

namespace Squabby.Actions.Actions
{
    public static class RatingAction
    {
        private const int MinRating = -10000;
        [SquabbyAction(1_00)]
        public static async Task RatingUpdate()
        {
            await using var context = new SquabbyContext();
            await context.Database.ExecuteSqlRawAsync($"UPDATE Threads SET Rating = GREATEST(Rating - 100, {Thread.MinRating});");
        }
    }
}