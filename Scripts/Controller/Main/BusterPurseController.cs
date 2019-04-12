using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class BusterPurseController : ExtendedBehaviour {

    [Subscribe(MainScene.MainMenuMessageType.BOUGHT_BUSTER)]
    public void BoughtBuster(Message msg)
    {
        var p = Yaga.Helpers.CastHelper.Cast<BuyBusterParametr>(msg.parametrs);

        DataController.instance.buster_entity.BougthBuster(p.type);

        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.UPDATE_BUSTER_VALUES);
    }

    [Subscribe(MainScene.MainMenuMessageType.BOUGHT_BUSTER_UPGRATE)]
    public void BoughtBusterUpgrate(Message msg)
    {
        var p = Yaga.Helpers.CastHelper.Cast<BuyBusterParametr>(msg.parametrs);

        DataController.instance.buster_entity.UpgrateBuster(p.type);

        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.UPDATE_BUSTER_VALUES);
    }

    // Use this for initialization
    override public void ExtendedStart () {
		
	}
	

}
