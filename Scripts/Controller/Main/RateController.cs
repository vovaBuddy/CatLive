using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga;
using Yaga.MessageBus;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class RateController : ExtendedBehaviour {

    public GameObject rate_panel;
    public GameObject market_req_panel;
    public GameObject panel;

    public List<GameObject> stars;

    public Text rate_title;
    public Text rate_body;
    public Text rate_fail_body;

    public Text market_title;
    public Text market_body;
    public Text market_btn;

    public static class Messages
    {
        public const string OPEN_RATE = "RP_OPEN_RATE";
        public const string CLOSE_RATE = "RP_CLOSE_RATE";
        public const string STAR_CLICK = "RP_STAR_CLICK";
    }

    // Use this for initialization
    public override void ExtendedStart()
    {
        market_req_panel.SetActive(false);

        rate_title.text = TextManager.getText("mm_rate_rate_title_text");
        rate_body.text = TextManager.getText("mm_rate_rate_body_text");
        rate_fail_body.text = TextManager.getText("mm_rate_rate_fail_body_text");
        market_title.text = TextManager.getText("mm_rate_market_title_text");
        market_body.text = TextManager.getText("mm_rate_market_body_text");
        market_btn.text = TextManager.getText("mm_rate_market_btn_text");
    }

    private IEnumerator star_clicked(int mark)
    {
        for (int i = 0; i < mark; ++i)
        {
            yield return new WaitForSeconds(0.015f);
            stars[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }

        yield return new WaitForSeconds(0.4f);

        if (mark >= 4)
        {
            market_req_panel.SetActive(true);
            rate_panel.transform.parent.GetComponent<Animator>().SetBool("market", true);
        }
        else
        {
            rate_body.gameObject.SetActive(false);
            rate_fail_body.gameObject.SetActive(true);
            //rate_panel.transform.parent.GetComponent<Animator>().SetBool("close", true);
        }
    }

    [Subscribe(Messages.STAR_CLICK)]
    public void StarClicked(Message msg)
    {
        var param = Yaga.Helpers.CastHelper.Cast<UpdateInt>(msg.parametrs);
        StartCoroutine(star_clicked(param.value));
    }

    [Subscribe(Messages.OPEN_RATE)]
    public void Open(Message msg)
    {
        panel.SetActive(true);
    }

    [Subscribe(Messages.CLOSE_RATE)]
    public void Close(Message msg)
    {
        panel.SetActive(false);
    }

    public void Close()
    {
        rate_panel.transform.parent.GetComponent<Animator>().SetBool("close", true);
    }

    public void Close2()
    {
        panel.SetActive(false);
        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.TOGGLE_MAIN_MENU_REVIEW_BTN);
    }

    public void OpenMarket()
    {
        panel.SetActive(false);
        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.TOGGLE_MAIN_MENU_REVIEW_BTN);
        Application.OpenURL(TextManager.getOtherText("mm_rate_url"));
    }


}
