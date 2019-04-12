using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yaga.MessageBus;

public class RateStar : MonoBehaviour, IPointerDownHandler
{
    public int mark;

    public void OnPointerDown(PointerEventData evd)
    {
        Message msg = new Message();
        msg.Type = RateController.Messages.STAR_CLICK;
        msg.parametrs = new UpdateInt(mark);
        MessageBus.Instance.SendMessage(msg);
    }
}
