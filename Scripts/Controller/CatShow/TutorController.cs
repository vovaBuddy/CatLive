using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using UnityEngine.UI;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class TutorController : ExtendedBehaviour {

    public GameObject tutor_obstacles;
    public GameObject spawner;
    public GameObject goal_view;

    public GameObject tutor1;
    public GameObject tutor2;
    public GameObject tutor3;
    public GameObject tutor4;

    public GameObject panel_tutor;

    int cur_step = 0;

    bool sw_up;
    bool sw_down;
    bool sw_left;
    bool sw_right;

    public Text message;
    public Text header_text;

    [Subscribe(CatShow.CatShowMessageType.TUTOR_ITEM)]
    public void Tutor(Message msg)
    {
        tutor1.SetActive(false);
        tutor2.SetActive(false);
        tutor3.SetActive(false);
        tutor4.SetActive(false);

        panel_tutor.SetActive(true);


        switch (cur_step)
        {
            case 0:
                message.text = TextManager.getText("catshow_tutor_right_text"); 
                tutor1.SetActive(true);
                break;
            case 1:
                message.text = TextManager.getText("catshow_tutor_left_text");
                tutor2.SetActive(true);
                break;
            case 2:
                message.text = TextManager.getText("catshow_tutor_up_text");
                tutor3.SetActive(true);
                break;
            case 3:
                message.text = TextManager.getText("catshow_tutor_down_text");
                tutor4.SetActive(true);
                break;
        }

        cur_step++;
    }

    [Subscribe(CatShow.CatShowMessageType.END_TUTOR)]
    public void EndTutor(Message msg)
    {
        spawner.SetActive(true);
    }

    bool need_stop = true;

	// Use this for initialization
	public override void ExtendedStart () {

        header_text.text = TextManager.getText("catshow_tutor_header_text");

        try
        {
            if ((bool)DataController.instance.tasks_storage.content["catshow_first_played"] == true)
            {
                tutor_obstacles.SetActive(false);
                spawner.SetActive(true);
                goal_view.SetActive(true);
            }
            else
            {
                tutor_obstacles.SetActive(true);
                spawner.SetActive(false);
                goal_view.SetActive(false);

                need_stop = false;

                sw_up = false;
                sw_down = false;
                sw_left = false;
                sw_right = false;
            }
        }
        catch
        {
            tutor_obstacles.SetActive(true);
            spawner.SetActive(false);
            goal_view.SetActive(false);

            sw_up = false;
            sw_down = false;
            sw_left = false;
            sw_right = false;
        }
	}

    [Subscribe(MainScene.MainMenuMessageType.SWIPE_LEFT)]
    public void swl(Message msg)
    {
        sw_left = true;
    }

    [Subscribe(MainScene.MainMenuMessageType.SWIPE_RIGHT)]
    public void swr(Message msg)
    {
        sw_right = true;
    }

    [Subscribe(MainScene.MainMenuMessageType.SWIPE_UP)]
    public void swu(Message msg)
    {
        sw_up = true;
    }

    [Subscribe(MainScene.MainMenuMessageType.SWIPE_DOWN)]
    public void swd(Message msg)
    {
        sw_down = true;
    }

    // Update is called once per frame
    public override void ExtendedUpdate()
    {
        if(need_stop)
        {
            need_stop = false;

            MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_START);
            gameObject.SetActive(false);
        }

		if (Input.GetKeyUp(KeyCode.A) || sw_left)
        {
            MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_END);
            panel_tutor.SetActive(false);
            sw_left = false;
        }

        if (Input.GetKeyUp(KeyCode.W) || sw_up)
        {
            MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_END);
            panel_tutor.SetActive(false);
            sw_up = false;
        }

        if (Input.GetKeyUp(KeyCode.S) || sw_down)
        {
            MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_END);
            panel_tutor.SetActive(false);

            sw_down = false;
        }

        if (Input.GetKeyUp(KeyCode.D) || sw_right)
        {
            MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_END);
            panel_tutor.SetActive(false);

            sw_right = false;
        }

        if (cur_step >= 3)
        {
            spawner.SetActive(true);
        }
    }
}
