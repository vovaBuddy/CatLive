using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyPrizeUI : MonoBehaviour {

    public Text value;
    public Image img;
    public GameObject inf_heart;

    public void Init(Sprite spr, string vl)
    {
        img.sprite = spr;
        value.text = vl;
    }
}
