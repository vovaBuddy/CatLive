using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfHeartsPrize : DailyPrize
{

    public int minutes;

    public InfHeartsPrize(int m)
    {
        minutes = m;
    }

    public void ActiveAction()
    {
        DataController.instance.catsPurse.inf_h_timer.SetTime("infinity_heart", minutes * 60);
        TimerDataController.GetController().Start();
    }

    public void CreatePrefub(GameObject instaniatedObj)
    {
        instaniatedObj.GetComponent<DailyPrizeUI>().value.text = minutes.ToString() + "m";
        instaniatedObj.GetComponent<DailyPrizeUI>().img.gameObject.SetActive(false);
        instaniatedObj.GetComponent<DailyPrizeUI>().inf_heart.SetActive(true);
    }
}
