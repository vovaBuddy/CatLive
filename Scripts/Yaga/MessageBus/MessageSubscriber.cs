namespace Yaga.MessageBus {
    public struct MessageSubscriber {
        public string[] MessageTypes;
        public SubscriberAction action;
    }    
}
