using System;

namespace Yaga
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Subscribe : Attribute
    {
        public string[] subscribed_types;

        private Subscribe() { }
        public Subscribe(params string[] args)
        {
            subscribed_types = args;
        }
    }
}
