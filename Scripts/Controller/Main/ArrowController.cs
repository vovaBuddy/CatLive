using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MainScene
{
    class ArrowController : MonoBehaviour
    {

        public static ArrowController Instance
        {
            get
            {
                return GameObject.Find("Controllers").transform.Find("MainMenuController")
                    .GetComponent<ArrowController>();
            }
        }

        public GameObject arrow_start_task;
        public GameObject arrow_scan_btn;
        public GameObject scanning_btn;
        public GameObject arrow_game_btn;
        public GameObject arrow_kitchen_set;

        public GameObject arrow_play_show;
        public GameObject arrow_shop;
        public GameObject arrow_cat_show;
        public GameObject tutor_customize;
    }
}
