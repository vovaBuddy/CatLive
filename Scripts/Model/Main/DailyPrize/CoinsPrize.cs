using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPrize : DailyPrize
{
    public int coins;
    public CoinsPrize(int value)
    {
        coins = value;
    }

    public void ActiveAction()
    {
        DataController.instance.catsPurse.Coins += coins;
    }

    public void CreatePrefub(GameObject instaniatedObj)
    {
        instaniatedObj.GetComponent<DailyPrizeUI>().value.text = coins.ToString();
        instaniatedObj.GetComponent<DailyPrizeUI>().img.sprite = Resources.Load<Sprite>("coin_icon");
    }
}
