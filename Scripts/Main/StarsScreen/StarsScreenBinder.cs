using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using UnityEngine.UI;

namespace Main.StarsScreen
{
    public class StarsScreenBinder : MonoBehaviour
    {
        public GameObject panel;
        [Localize]
        public Text header;
        [Localize]
        public Text title1;
        [Localize]
        public Text title_game;
        [Localize]
        public Text title_stars;
        [Localize]
        public Text title_tasks;
        [Localize]
        public Text btn_text;

        public GameObject close_btn;
        public Animator scheme_animator;
    }
}
