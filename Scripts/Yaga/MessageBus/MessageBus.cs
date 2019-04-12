using System.Collections.Generic;

namespace Yaga.MessageBus
{
    public class MessageBus
    {
        private List<Message> queue;
        private List<Message> other_tread_queue;

        Dictionary<string, List<Message>> disposable_subscibers = 
            new Dictionary<string, List<Message>>();

        Dictionary<string, List<MessageSubscriber>> subscriberLists =
            new Dictionary<string, List<MessageSubscriber>>();

        static MessageBus instance;

        public static void Restore()
        {
            instance = null;
        }

        public static MessageBus Instance
        {
            get
            {
                if (instance == null)
                    instance = new MessageBus();

                return instance;
            }
        }

        private MessageBus() {
            queue = new List<Message>();
            other_tread_queue = new List<Message>();
            subscriberLists.Clear();
        }

        public void SendMessageAfterEvent(string msg_event, Message msg)
        {
            if(!disposable_subscibers.ContainsKey(msg_event))
            {
                disposable_subscibers[msg_event] = new List<Message>();
            }

            disposable_subscibers[msg_event].Add(msg);
        }

        public void AddSubscriber(MessageSubscriber subscriber)
        {
            string[] messageTypes = subscriber.MessageTypes;
            for (int i = 0; i < messageTypes.Length; i++)
                AddSubscriberToMessage(messageTypes[i], subscriber);

            //UnityEngine.Debug.Log("AddSubscriber: " + subscriber.MessageTypes.ToString());
            //UnityEngine.Debug.Log("queue.Count: " + queue.Count.ToString());

            for (int i = 0; i < queue.Count; i++)
            {
                bool finded = false;
                foreach(string type in subscriber.MessageTypes)
                {
                    if(queue[i].Type == type)
                    {
                        subscriber.action(queue[i]);


                        finded = true;
                        break;
                    }
                }

                //if(finded)
                //{
                //    queue.Remove(queue[i]);
                //}
            }
        }

        void AddSubscriberToMessage(string messageType,
                                     MessageSubscriber subscriber)
        {
            if (!subscriberLists.ContainsKey(messageType))
                subscriberLists[messageType] =
                    new List<MessageSubscriber>();

            subscriberLists[messageType].Add(subscriber);
        }

        public void SendMessageToQueue(string msg_type)
        {
            Message message = new Message();
            message.Type = msg_type;

            queue.Add(message);
        }

        public void SendMessage(string msg_type)
        {
            CheckOtherTread();

            Message message = new Message();
            message.Type = msg_type;

            if (!subscriberLists.ContainsKey(message.Type))
            {
                UnityEngine.Debug.Log("There are now subscribers for: " + message.Type);
                queue.Add(message);
                return;
            }

            List<MessageSubscriber> subscriberList =
                subscriberLists[message.Type];

            for (int i = 0; i < subscriberList.Count; i++)
                SendMessageToSubscriber(message, subscriberList[i]);
        }

        public void CheckOtherTread()
        {
            if (other_tread_queue.Count >= 0)
            {
                foreach (var m in other_tread_queue)
                {
                    try
                    {
                        List<MessageSubscriber> subscriberList =
                            subscriberLists[m.Type];

                        for (int i = 0; i < subscriberList.Count; i++)
                            SendMessageToSubscriber(m, subscriberList[i]);
                    }
                    catch
                    {
                        //
                    }
                }

                other_tread_queue.Clear();
            }
        }

        public void SendMessage(Message message, bool to_queue = false)
        {
            if(!to_queue)
                CheckOtherTread();

            if (to_queue)
            {
                other_tread_queue.Add(message);
                return;
            }

            if (!subscriberLists.ContainsKey(message.Type))
            {
                UnityEngine.Debug.Log("There are now subscribers for: " + message.Type);
                queue.Add(message);
                return;
            }

            List<MessageSubscriber> subscriberList =
                subscriberLists[message.Type];

            for (int i = 0; i < subscriberList.Count; i++)
                SendMessageToSubscriber(message, subscriberList[i]);
        }

        void SendMessageToSubscriber(Message message,
                                     MessageSubscriber subscriber)
        {
            if(disposable_subscibers.ContainsKey(message.Type))
            {
                foreach(var m in disposable_subscibers[message.Type])
                {
                    SendMessage(m);
                }

                disposable_subscibers[message.Type].Clear();
            }
            //UnityEngine.Debug.Log(message.Type);
            subscriber.action(message);
        }
    }
}
