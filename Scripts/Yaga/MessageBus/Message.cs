namespace Yaga.MessageBus
{
    public struct Message
    {
        public string Type;
        public MessageParametrs parametrs;

        public Message(string t, MessageParametrs p)
        {
            Type = t;
            parametrs = p;
        }
    }
}
