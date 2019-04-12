using System;
using System.Linq;
using Yaga.MessageBus;

namespace Yaga
{
    class SubscribeExtention : IExtension
    {
        object obj;

        private SubscribeExtention() { }
        public SubscribeExtention(object obj)
        {
            this.obj = obj;
        }

        public void Start()
        {
            var methods = obj.GetType().GetMethods().Where(
                prop => Attribute.IsDefined(prop, typeof(Subscribe)));

            foreach (System.Reflection.MethodInfo m in methods)
            {
                SubscriberAction method = (SubscriberAction)Delegate.CreateDelegate(typeof(SubscriberAction), obj, m.Name);         
                Subscribe attr = (Subscribe)m.GetCustomAttributes(typeof(Subscribe), true)[0];

                MessageBus.MessageSubscriber subs = new MessageBus.MessageSubscriber();
                subs.action = method;
                subs.MessageTypes = attr.subscribed_types;

                MessageBus.MessageBus.Instance.AddSubscriber(subs);
            }
        }

        public void Update()
        {
            
        }
    }
}
