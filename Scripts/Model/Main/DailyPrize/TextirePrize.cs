using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;

public class TextirePrize : DailyPrize {

    public string texture_name;

    public TextirePrize(string t_name)
    {
        texture_name = t_name;
    }

    public void ActiveAction()
    {
        
    }

    public void CreatePrefub(GameObject instaniatedObj)
    {
        instaniatedObj.GetComponent<DailyPrizeUI>().value.text = "New!";
        instaniatedObj.GetComponent<DailyPrizeUI>().img.sprite = Resources.Load<Sprite>(texture_name);
    }
}
