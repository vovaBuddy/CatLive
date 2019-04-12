using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Bank
{
    public class BankBuyItem : MonoBehaviour
    {
        public Image sprite;
        public Text value;
        public Text btn_text;
        public Button btn;
        public int id;
        readonly string sprite_name = "bank_icon_";

        public void Init(int i, string price, Action onclick)
        {
            id = i;
            //sprite.sprite = Resources.Load<Sprite>(sprite_name + "001");
            value.text = BuyItemsInfo.values[i].ToString();
            btn_text.text = price;

            btn.onClick.AddListener(() => { onclick(); });
        }
    }
}
