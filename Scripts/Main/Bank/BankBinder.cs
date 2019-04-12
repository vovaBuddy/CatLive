using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using UnityEngine.UI;

namespace Main.Bank
{
    public class BankBinder : MonoBehaviour
    {
        public GameObject bank_panel;
        public GameObject items_conteiner;
        public GameObject item_prefab;
        [Localize]
        public Text header;
        [Localize]
        public Text restore_purchases_btn_text;

        public Text log_text;
    }
}