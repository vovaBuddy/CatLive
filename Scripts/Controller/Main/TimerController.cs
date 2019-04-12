using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;
using Yaga;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class TimerController : ExtendedBehaviour {

    public static TimerController GetController()
    {
        return GameObject.Find("Controllers").transform.Find("TimerController")
            .GetComponent<TimerController>();
    }

    public GameObject task7_timer;
    public GameObject task9_timer;
    public GameObject task12_timer;
    public GameObject task13_timer;
    public GameObject task15_timer;
    public GameObject task16_timer;
    public GameObject task18_timer;
    public GameObject task22_timer;
    public GameObject task24_timer;

    // Use this for initialization
    public override void ExtendedStart() {
        task7_timer.SetActive(false);
        task9_timer.SetActive(false);
        task12_timer.SetActive(false);
        task13_timer.SetActive(false);
        task15_timer.SetActive(false);
        task16_timer.SetActive(false);
        task18_timer.SetActive(false);
        task22_timer.SetActive(false);
        task24_timer.SetActive(false);
    }

    [Subscribe(MainScene.MainMenuMessageType.TASK_TIME_UPDATE)]
    public void SetTimer(Message msg)
    {
        var param = Yaga.Helpers.CastHelper.Cast<CommonMessageParametr>(msg.parametrs);

        var timer_obj = (GameObject)(((object[])param.obj)[0]);
        var time = (int)(((object[])param.obj)[1]);

        if (time <= 0)
        {
            timer_obj.SetActive(false);
        }
        else
        {
            timer_obj.SetActive(true);
            timer_obj.GetComponent<TextMesh>().text = Helper.TextHelper.TimeFormatMinutes(time);
        }
    }
	
	// Update is called once per frame
	public override void ExtendedUpdate () {
		
	}
}
