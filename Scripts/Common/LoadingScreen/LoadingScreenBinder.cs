using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Common.LoadingScreen
{
    public class LoadingScreenBinder : MonoBehaviour
    {
        public GameObject panel;
        public GameObject pb_line;
        public GameObject pb;
        public GameObject btn;
        public float max_pb_with = 840;

        public Text play_btn_text;
        public Text loading_text;

        public Image logo_open_scene;
        public Image logo_close_scene;
    }
}