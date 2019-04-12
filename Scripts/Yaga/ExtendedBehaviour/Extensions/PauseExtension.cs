using System;
using System.Collections.Generic;
using System.Linq;
using Yaga.MessageBus;

namespace Yaga
{
    class PauseExtension : IExtension
    {
        object obj;

        private PauseExtension() { }
        public PauseExtension(object obj)
        {
            this.obj = obj;
        }

        void StartAction(Message msg)
        {
            ((ExtendedBehaviour)obj).update_action = 
                ((ExtendedBehaviour)obj).EmptyUpdate;
        }

        void EndAction(Message msg)
        {
            ((ExtendedBehaviour)obj).update_action =
                ((ExtendedBehaviour)obj).YagaUpdate;
        }

        public void Start()
        {
            MessageSubscriber subscriber = new MessageSubscriber();
            subscriber.MessageTypes = new string[] 
            { YagaMessageType.PAUSE_START };
            subscriber.action = StartAction;

            MessageBus.MessageBus.Instance.AddSubscriber(subscriber);

            subscriber.MessageTypes = new string[]
            { YagaMessageType.PAUSE_END };
            subscriber.action = EndAction;

            MessageBus.MessageBus.Instance.AddSubscriber(subscriber);
        }

        public void Update()
        {
        }
    }
}
