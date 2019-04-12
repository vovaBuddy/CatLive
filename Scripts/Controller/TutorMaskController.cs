using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class TutorMaskController : ExtendedBehaviour
{
    //API SECTION
    public static class Messages
    {
        public const string SHOW_TUTOR_MASK = "SHOW_TUTOR_MASK";
        public const string CLOSE_TUTOR_MASK = "CLOSE_TUTOR_MASK";
        public const string CHECK_QUEUE = "TutorMaskController.CHECK_QUEUE";
        public const string ADD_TO_QUEUE = "TutorMaskController.ADD_TO_QUEUE";
    }

    public class TutorMaskParametr : MessageParametrs
    {
        public Vector2 pos;
        public bool arrow_is_up;
        public bool ancherPos;
        public string name;

        public TutorMaskParametr(Vector2 p, bool ancher_pos, bool arrow, string n)
        {
            pos = p;
            arrow_is_up = arrow;
            ancherPos = ancher_pos;
            name = n;
        }
    }
    //END API SECTION

    private Queue<Message> messages_queue = new Queue<Message>();
    bool is_busy;

    public GameObject parent;
    public GameObject mask_prefub;
    public GameObject prefub_arrow;
    GameObject cur_arrow;
    GameObject mask;
    bool first = true;

    [Subscribe(Messages.ADD_TO_QUEUE)]
    public void AddToQueue(Message msg)
    {
        msg.Type = Messages.SHOW_TUTOR_MASK;
        messages_queue.Enqueue(msg);
    }

    [Subscribe(Messages.CHECK_QUEUE)]
    public void CheckQueue(Message msg)
    {
        if (messages_queue.Count != 0 && !is_busy)
        {
            MessageBus.Instance.SendMessage(messages_queue.Dequeue());
        }
    }

    [Subscribe(Messages.CLOSE_TUTOR_MASK)]
    public void Close(Message msg)
    {
        if (cur_arrow == null)
            return;

        cur_arrow.SetActive(false);
        mask.GetComponent<Animator>().SetBool("close", true);
        is_busy = false;

        GameStatistics.instance.SendStat("tutor_pressed_" + mask.GetComponent<UIMaskController>().tutor_event_name, 0);
    }

    [Subscribe(Messages.SHOW_TUTOR_MASK)]
    public void Show(Message msg)
    {
        if(is_busy)
        {
            messages_queue.Enqueue(msg);
            return;
        }

        var p = Yaga.Helpers.CastHelper.Cast<TutorMaskParametr>(msg.parametrs);

        is_busy = true;

        mask = Instantiate(mask_prefub, parent.transform, false);
        mask.name = "mask_" + UnityEngine.Random.Range(1000, 9999).ToString();
        mask.transform.localScale = new Vector3(1, 1, 1);
        mask.SetActive(true);
        mask.GetComponent<UIMaskController>().tutor_event_name = p.name;

        if(p.ancherPos)
            mask.GetComponent<RectTransform>().anchoredPosition = p.pos;
        else
            mask.transform.position = p.pos;

        cur_arrow = Instantiate(prefub_arrow, mask.transform, false);
        cur_arrow.transform.localScale = new Vector3(1, 1, 1);

        if (p.arrow_is_up)
        {
            cur_arrow.transform.localEulerAngles = new Vector3(0, 0, -90);
            cur_arrow.transform.localPosition = new Vector3(0, 350, 0);
        }
        else
        {
            cur_arrow.transform.localEulerAngles = new Vector3(0, 0, 90);
            cur_arrow.transform.localPosition = new Vector3(0, -350, 0);
        }
    }
}
