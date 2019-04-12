using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga;

namespace Main.Chapter
{
    public class ChapterBinder : MonoBehaviour
    {
        public GameObject chapter_prize_panel;
        public Text chapter_text;
        [Localize]
        public Text done_text;

        public Image booster_img;
        public Text booster_cnt;
        public Text booster_name;

        public Text inf_hearts_timer;
        public Text coins_cnt;

        [Localize]
        public Text pick_up_btn;
    }
}