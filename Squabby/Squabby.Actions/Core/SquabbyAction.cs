using System;

namespace Squabby.Actions.Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SquabbyAction : Attribute
    {
        public int Interval { get; }
        public SquabbyAction(int interval) => Interval = interval;
    }
}