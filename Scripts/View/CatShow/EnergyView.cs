using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;
using Yaga;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class EnergyView : ExtendedBehaviour {

    public GameObject energy;
    public Text header_text;
    public Text body_text;
    public Text footer_text;

	// Use this for initialization
	public override void ExtendedStart ()
    {
        header_text.text = TextManager.getText("catshow_fail_header_text");
        body_text.text = TextManager.getText("catshow_fail_body_text");
        footer_text.text = TextManager.getText("catshow_fail_footer_text");

        energy.SetActive(false);
    }

    [Subscribe(CatShow.CatShowMessageType.NOTE_ENOUGH_ENERGY)]
    public void NotEnough(Message msg)
    {
        energy.SetActive(true);
    }

    public void BuyFree()
    {
        if (Time.realtimeSinceStartup - DataController.instance.advEntity.last_rew < 30)
        {
            return;
        }

        GameStatistics.instance.SendStat("require_rewarded_add_energy_catshow", 0);

        AppodealController.instance.InitRewardActions(() =>
        {
            MessageBus.Restore();
            SceneManager.LoadScene("cat_show_game");
        }, () => { });

        AppodealController.instance.showRewarded();
    }

    public void Buy()
    {
        if(DataController.instance.catsPurse.Coins >= 900)
        {
            DataController.instance.catsPurse.Coins -= 900;
            MessageBus.Restore();
            SceneManager.LoadScene("cat_show_game");
        }
    }

    // Update is called once per frame
    public override void ExtendedUpdate() {
		
	}
}
