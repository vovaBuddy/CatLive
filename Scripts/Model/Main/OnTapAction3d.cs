using Main.Bubble;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;

public class OnTapAction3d : MonoBehaviour {

    public Cats cat;
    public int range_max_value;

    public void OnTapAction()
    {
        int _range_max_value;
        string str = string.Empty;
        if(Random.Range(0,2) == 0)
        {
            if(cat != Cats.Black &&
                cat != Cats.Jakky)
            {
                str = "Main";
            }
            else
            {
                str = cat.ToString();
            }
            
            _range_max_value = range_max_value == 0 ? 28 : range_max_value;
        }
        else
        {
            str = "Main";
            _range_max_value = 28;
        }
        MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
            new BubbleCreateParametr(
                CatsMoveController.GetController().GetTransform(cat), new List<string>()
                    {TextManager.getText("bubble_tap_" + str + "_" +
                                    Random.Range(0,_range_max_value).ToString()) }, 8, true)));

    }
}
