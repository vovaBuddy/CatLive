using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class GoalView : ExtendedBehaviour {

    public GameObject goal;
    public Text clothes_beauty;

    public Text header_text;
    public Text btn_text;
    public Text body_text;
    public Text footer_text;

	// Use this for initialization
	public override void ExtendedStart () {
        goal.SetActive(true);

        header_text.text = TextManager.getText("catshow_goal_header_text");
        btn_text.text = TextManager.getText("catshow_goal_btn_text");
        body_text.text = TextManager.getText("catshow_goal_body_text");
        footer_text.text = TextManager.getText("catshow_goal_footer_text");

        clothes_beauty.text = DataController.instance.catsPurse.WearBeauty.ToString();
    }

    // Update is called once per frame
    public override void ExtendedUpdate() {
		
	}

    public void Close()
    {
        MessageBus.Instance.SendMessage(new Message(
            Common.LoadingScreen.API.LoadingScreenAPI.OPEN_OPEN_ANIM,
            new Common.LoadingScreen.API.SceneNameParametr(
                Common.LoadingScreen.API.SceneNames.MAIN, false)));
    }

    public void StartGame()
    {
        goal.SetActive(false);
        MessageBus.Instance.SendMessage(YagaMessageType.PAUSE_END);
    }
}
