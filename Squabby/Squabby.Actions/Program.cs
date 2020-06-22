using System.Threading.Tasks;
using Squabby.Actions.Core;

namespace Squabby.Actions
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var action in ActionManager.LoadActions())
            {
                var timer = new System.Timers.Timer(action.Value.Interval);
                timer.Elapsed += async (e,args)=> await action.Key(); 
                timer.Start(); 
            }

            Task.Delay(-1).Wait();
        }
    }
}