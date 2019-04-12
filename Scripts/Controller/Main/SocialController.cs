using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga;
using Yaga.MessageBus;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class SocialController : ExtendedBehaviour {

    public static class Messages
    {
        public const string OPEN_PANEL = "SN_OPEN_PANEL";
        public const string CLOSE_PANEL = "SN_CLOSE_PANEL";
        public const string SUBSCRIBE = "SN_SUBSCRIBE";
        public const string CANCEL_SUBS = "SN_CANCEL_SUBS";
    }
    public Text title_text;
    public Text body_text;
    public Text no_btn_text;
    public Text later_btn_text;
    public Text subs_btn_text;

    public Image img;
    public GameObject panel;

	// Use this for initialization
	public override void ExtendedStart () {
        title_text.text = TextManager.getText("mm_sn_title_text");
        body_text.text = TextManager.getText("mm_sn_body_text");
        no_btn_text.text = TextManager.getText("mm_sn_no_btn_text");
        later_btn_text.text = TextManager.getText("mm_sn_later_btn_text");
        subs_btn_text.text = TextManager.getText("mm_sn_subs_btn_text");

        img.sprite = Resources.Load<Sprite>(TextManager.getOtherText("mm_sn_icon_name"));
    }
	
    [Subscribe(Messages.OPEN_PANEL)]
    public void Open(Message msg)
    {
        panel.SetActive(true);
    }

    [Subscribe(Messages.CLOSE_PANEL)]
    public void Close(Message msg)
    {
        panel.SetActive(false);
    }

    public void Close()
    {
        panel.GetComponent<Animator>().SetBool("close", true);
    }

    public void NoBtn()
    {
        panel.GetComponent<Animator>().SetBool("close", true);
        //MessageBus.Instance.SendMessage(Messages.CANCEL_SUBS);
        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.TOGGLE_MAIN_MENU_SOCIAL_BTN);
    }

    public void LaterBtn()
    {
        panel.GetComponent<Animator>().SetBool("close", true);
    }

    public void SubsBtn()
    {
        panel.GetComponent<Animator>().SetBool("close", true);
        //MessageBus.Instance.SendMessage(Messages.SUBSCRIBE);
        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.TOGGLE_MAIN_MENU_SOCIAL_BTN);

        Application.OpenURL(TextManager.getOtherText("social_url"));
    }
}
