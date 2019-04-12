using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Yaga;
using Yaga.MessageBus;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class NameController : ExtendedBehaviour {

    public static class Messages
    {
        public const string OPEN_PANEL = "NC_OPEN_PANEL";
        public const string CLOSE_PANEL = "NC_CLOSE_PANEL";
    }

    public GameObject panel;
    public InputField mainInputField;

    public Text title_text;
    public Text placeholder_text;

    public GameObject ok_btn;

    // Checks if there is anything entered into the input field.
    void LockInput(InputField input)
    {
        if (input.text.Length > 0)
        {
            DataController.instance.catsPurse.Name = input.text;
        }
        else if (input.text.Length == 0)
        {

        }
    }
    // Use this for initialization
    public override void ExtendedStart()
    {
        mainInputField.onEndEdit.AddListener(delegate { LockInput(mainInputField); });

        title_text.text = TextManager.getText("mm_namepanel_title_text");
        placeholder_text.text = TextManager.getText("mm_namepanel_placeholder_text");
    }

    public override void ExtendedUpdate()
    {
        ok_btn.SetActive(mainInputField.text.Length > 0);
    }

    [Subscribe(Messages.OPEN_PANEL)]
    public void Open(Message msg)
    {
        panel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(mainInputField.gameObject, null);
        mainInputField.OnPointerClick(null);
    }

    [Subscribe(Messages.CLOSE_PANEL)]
    public void Close(Message msg)
    {
        panel.SetActive(false);

        GameStatistics.instance.SendStat("name_entered", 0);
        //
        //DialogController dialog = DialogController.GetController();
        //dialog.ShowDialog();

        //List<DialogEntity> deList = new List<DialogEntity>();
        //deList.Add(new DialogEntity(DataController.instance.catsPurse.Name + TextManager.getDialogsText(1, 2), DialogType.Main, DialogType.One));
        //deList.Add(new DialogEntity(TextManager.getDialogsText(1, 3), DialogType.Main, DialogType.One));
        
        //dialog.SetDialogs(deList);
        //dialog.SetBtnAction(() =>
        //{
        //    dialog.CloseDialog();
        //    MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);

        //    msg = new Message();
        //    msg.Type = MainScene.MainMenuMessageType.CUT_SCENE_SHOWED;
        //    msg.parametrs = new UpdateInt(0);
        //    MessageBus.Instance.SendMessage(msg);
        //});

        //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.ZOOM_CAMERA);
    }
}
