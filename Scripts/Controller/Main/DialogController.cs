using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga.MessageBus;

public enum DialogType
{
    One, 
    Main, 
    Black,
    Deliver,
    Call_Worker,
    Djeki,
}

public class DialogEntity
{
    public static int get_id(int mission_id, int text_index)
    {
        return mission_id * 100 + text_index;
    }

    public string text;
    public DialogType d_left;
    public DialogType d_right;
    public int id;

    public DialogEntity(string t, DialogType left, DialogType right, int identifier)
    {
        text = t;
        d_left = left;
        d_right = right;
        id = identifier;
    }
}

public class DialogControllerAPI
{
    public class Message
    {
        public const string CLOSED = "DialogControllerAPI.CLOSED";
    }

}

public class DialogController : MonoBehaviour {

    public Image logo;
    public GameObject DialogWindow;
    public Text text;
    public GameObject left_person;
    public GameObject right_person;
    public Button next_btn;

    public GameObject mission_area_container;
    public GameObject mission_area_prefub;
    public GameObject target;
    List<GameObject> areas;

    List<DialogEntity> dialogs;

    Action btn_action;

    public Dictionary<int, List<Message>> messages;

    int dialog_index = 0;

    public void SetMessagesByIndex(int index, List<Message> msgs)
    {
        messages[index] = msgs;
    }

    public void SetMissionIcon(string name)
    {
        var go = Instantiate(mission_area_prefub, mission_area_container.transform, false);
        go.SetActive(false);
        go.GetComponent<MissionArea>().Init(Resources.Load<Sprite>(name), target);
        areas.Add(go);
    }

    void SetSprites(DialogType t, GameObject sprite)
    {
        switch (t)
        {
            case DialogType.Main:
                sprite.GetComponent<Image>().sprite = Resources.Load<Sprite>("av_cat_001");
                break;
            case DialogType.Black:
                sprite.GetComponent<Image>().sprite = Resources.Load<Sprite>("av_cat_002");
                break;
            case DialogType.Djeki:
                sprite.GetComponent<Image>().sprite = Resources.Load<Sprite>("av_cat_003");
                break;
            case DialogType.Call_Worker:
                sprite.GetComponent<Image>().sprite = Resources.Load<Sprite>("av_cat_004");
                break;
            case DialogType.Deliver:
                sprite.GetComponent<Image>().sprite = Resources.Load<Sprite>("av_cat_005");
                break;

            case DialogType.One:
                sprite.SetActive(false);
                break;
        }

    }

    IEnumerator OpenCoorutine()
    {
        foreach (var go in areas)
        {
            go.SetActive(true);

            yield return new WaitForSeconds(0.55f);
        }
    }

    IEnumerator MoveCoorutine()
    {
        foreach (var go in areas)
        {
            go.transform.parent = go.transform.parent.parent;
            go.SetActive(true);
            go.GetComponent<MissionArea>().MoveToPanel();           

            yield return new WaitForSeconds(0.25f);
        }

        areas.Clear();
    }

    public void SetDialogs(List<DialogEntity> d)
    {
        dialogs = d;
        dialog_index = 0;

        text.text = dialogs[0].text;
        SetSprites(dialogs[0].d_left, left_person);
        SetSprites(dialogs[0].d_right, right_person);

        next_btn.onClick.RemoveAllListeners();
        next_btn.onClick.AddListener(() =>
        {
            GameStatistics.instance.SendStat("dialog_item_showed", dialogs[dialog_index].id);

            dialog_index++;

            if(messages.ContainsKey(dialog_index))
            {
                foreach(var m in messages[dialog_index])
                {
                    MessageBus.Instance.SendMessage(m);
                }
            }

            if (dialog_index < dialogs.Count)
            {
                text.text = dialogs[dialog_index].text;
                SetSprites(dialogs[dialog_index].d_left, left_person);
                SetSprites(dialogs[dialog_index].d_right, right_person);

                if(dialog_index >= dialogs.Count - 1 && areas.Count > 0)
                {
                    StartCoroutine(OpenCoorutine());
                }
            }
            else
            {
                StartCoroutine(MoveCoorutine());
                messages.Clear();

                btn_action();
                //mission_area.GetComponent<Animator>().SetBool("close", true);
            }

        });
    }


    public static DialogController GetController()
    {
        return GameObject.Find("Controllers").transform.Find("DialogController")
            .GetComponent<DialogController>();
    }

    public void SetBtnAction(Action action)
    {
        btn_action = action;
    }

    public void ShowDialog()
    {
        DialogWindow.SetActive(true);
        DialogWindow.GetComponent<Animator>().SetBool("close", false);
        CameraMoveController.GetController().SetTouchMove(false);

        if (dialogs.Count == 1 && areas.Count > 0)
        {
            StartCoroutine(OpenCoorutine());
        }

        GameStatistics.instance.SendStat("start_show_dialog", 0);
    }

    public void CloseDialog()
    {
        //DialogWindow.SetActive(false);
        DialogWindow.GetComponent<Animator>().SetBool("close", true);
        CameraMoveController.GetController().SetTouchMove(true);
        MessageBus.Instance.SendMessage(DialogControllerAPI.Message.CLOSED);

        GameStatistics.instance.SendStat("close_dialog", 0);
    }

	// Use this for initialization
	void Start () {
        areas = new List<GameObject>();
        messages = new Dictionary<int, List<Message>>();
        next_btn.transform.GetChild(0).GetComponent<Text>().text =
            TextManager.getText("dialog_next_btn_text");

        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            logo.sprite = Resources.Load<Sprite>("logo/Russian");
        }
        else
        {
            logo.sprite = Resources.Load<Sprite>("logo/English");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
