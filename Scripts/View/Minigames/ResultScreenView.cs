using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class ResultScreenView : ExtendedBehaviour {

    public Text coins;
    public Text points;
    public Text header_text;
    public Text btn_text;
    public Text increase_header_text;

    public GameObject result_screen;

    public GameObject continue_btn;

    public GameObject adv_res_screen;

    [Subscribe(Minigames.MiniGameMessageType.SHOW_ADV_MENU_RESULT)]
    public void AdvShow(Message msg)
    {
        adv_res_screen.SetActive(true);
    }

    [Subscribe(Minigames.MiniGameMessageType.OPEN_RESULT)]
    public void Show(Message msg)
    {
        var param = CastHelper.Cast<CommonMessageParametr>(msg.parametrs);
        var dic = param.obj as Dictionary<string, int>;
        result_screen.SetActive(true);
        continue_btn.SetActive((bool)DataController.instance.other_storage["ShowContinue"]);

        coins.text = dic["catched_coins"].ToString();
        points.text = dic["points"].ToString();
    }
    
	// Use this for initialization
	override public void ExtendedStart () {

        header_text.text = TextManager.getText("minigame_result_header_text");
        btn_text.text = TextManager.getText("minigame_result_btn_text");
        increase_header_text.text = TextManager.getText("minigame_increase_header_text");

        result_screen.SetActive(false);
    }

    // Update is called once per frame
    override public void ExtendedUpdate() {
		
	}
}
