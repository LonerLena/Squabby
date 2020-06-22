using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Squabby.Actions.Core
{
    public static class ActionManager
    {
        public delegate Task SquabbyActionDelegate();
        public static Dictionary<SquabbyActionDelegate, SquabbyAction> LoadActions(Assembly assembly = null)
            => (assembly ?? Assembly.GetCallingAssembly())
                .GetTypes()
                .SelectMany(x => x.GetMethods())
                .Where(m => m.IsValidSquabbyAction())
                .ToDictionary(k => k.ToSquabbyActionDelegate(),
                    v => v.GetCustomAttributes().OfType<SquabbyAction>().First());
    }
}