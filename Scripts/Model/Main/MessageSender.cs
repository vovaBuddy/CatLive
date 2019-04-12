using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;

public class MessageSender : MonoBehaviour {
    public void SendYagaMessage(string msg)
    {
        MessageBus.Instance.SendMessage(msg);
    }
}
