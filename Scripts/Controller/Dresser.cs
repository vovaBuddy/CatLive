using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class Dresser : ExtendedBehaviour {

    public GameObject head_cat;
    public GameObject head_bow;
    public GameObject collar;
    public GameObject glasses;
    public GameObject eye_1;
    public GameObject eye_2;
    public GameObject skin;

    [Subscribe(MainScene.MainMenuMessageType.TAKEOFF_ITEM)]
    public void TakeOff(Message msg)
    {
        var p = CastHelper.Cast<MainScene.BuyItemParametr>(msg.parametrs);

        switch (p.type)
        {
            case MainScene.ShopItemType.SKIN:
                DataController.instance.catsPurse.skin_beauty = 10;
                skin.SetActive(false);
                break;
            case MainScene.ShopItemType.HEADDRESS_CAP:
                DataController.instance.catsPurse.head_beauty = 0;
                head_cat.SetActive(false);
                break;
            case MainScene.ShopItemType.HEADDRESS_BOW:
                DataController.instance.catsPurse.head_beauty = 0;
                head_bow.SetActive(false);
                break;
            case MainScene.ShopItemType.COLLAR:
                DataController.instance.catsPurse.collar_beauty = 0;
                collar.SetActive(false);
                break;
            case MainScene.ShopItemType.GLASSE:
                DataController.instance.catsPurse.glasses_beauty = 0;
                glasses.SetActive(false);
                break;
        }

    }

    [Subscribe(MainScene.MainMenuMessageType.DRESS_ITEM, 
        MainScene.MainMenuMessageType.PREVIEW_ITEM)]
    public void ChangeWear(Message msg)
    {
        var p = CastHelper.Cast<MainScene.BuyItemParametr>(msg.parametrs);

        switch (p.type)
        {
            case MainScene.ShopItemType.SKIN:
                skin.SetActive(true);
                if(msg.Type != MainScene.MainMenuMessageType.PREVIEW_ITEM)
                    DataController.instance.catsPurse.skin_beauty = p.beauty_value;
                skin.GetComponent<Renderer>().material
                    .SetTexture("_MainTex", p.item_texture);
                break;
            case MainScene.ShopItemType.HEADDRESS_CAP:
                head_cat.SetActive(true);
                head_bow.SetActive(false);
                if (msg.Type != MainScene.MainMenuMessageType.PREVIEW_ITEM)
                    DataController.instance.catsPurse.head_beauty = p.beauty_value;
                head_cat.GetComponent<Renderer>().material
                    .SetTexture("_MainTex", p.item_texture);
                break;
            case MainScene.ShopItemType.HEADDRESS_BOW:
                head_bow.SetActive(true);
                head_cat.SetActive(false);
                if (msg.Type != MainScene.MainMenuMessageType.PREVIEW_ITEM)
                    DataController.instance.catsPurse.head_beauty = p.beauty_value;
                head_bow.GetComponent<Renderer>().material
                    .SetTexture("_MainTex", p.item_texture);
                break;
            case MainScene.ShopItemType.GLASSE:
                glasses.SetActive(true);
                if (msg.Type != MainScene.MainMenuMessageType.PREVIEW_ITEM)
                    DataController.instance.catsPurse.glasses_beauty = p.beauty_value;
                glasses.GetComponent<Renderer>().material
                    .SetTexture("_MainTex", p.item_texture);
                break;
            case MainScene.ShopItemType.COLLAR:
                if (msg.Type != MainScene.MainMenuMessageType.PREVIEW_ITEM)
                    DataController.instance.catsPurse.collar_beauty = p.beauty_value;
                collar.SetActive(true);
                collar.GetComponent<Renderer>().material
                    .SetTexture("_MainTex", p.item_texture);
                break;
            case MainScene.ShopItemType.EYE:
                if (msg.Type != MainScene.MainMenuMessageType.PREVIEW_ITEM)
                    DataController.instance.catsPurse.eye_beauty = p.beauty_value;
                eye_1.GetComponent<Renderer>().material
                    .SetTexture("_MainTex", p.item_texture);
                eye_2.GetComponent<Renderer>().material
                    .SetTexture("_MainTex", p.item_texture);
                break;
        }

    }

	// Use this for initialization
	override public void ExtendedStart () {
		
	}

    // Update is called once per frame
    override public void ExtendedUpdate() {
		
	}
}
