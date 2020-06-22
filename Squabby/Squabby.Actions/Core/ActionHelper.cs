using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Squabby.Actions.Core
{
    public static class ActionHelper
    {
        public static bool IsValidSquabbyAction(this MethodInfo methodInfo)
            => methodInfo.GetCustomAttributes().OfType<SquabbyAction>().Any()
               && methodInfo.IsStatic
               && !methodInfo.GetParameters().Any()
               && methodInfo.ReturnType == typeof(Task);

        public static ActionManager.SquabbyActionDelegate ToSquabbyActionDelegate(this MethodInfo methodInfo)
            => (ActionManager.SquabbyActionDelegate) Delegate.CreateDelegate(typeof(ActionManager.SquabbyActionDelegate),null, methodInfo);
    }
}