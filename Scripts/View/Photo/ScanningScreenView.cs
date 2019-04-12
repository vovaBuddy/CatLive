using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;

namespace PhotoScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class ScanningScreenView : ExtendedBehaviour
    {

        public GameObject scanning_screen;

        public Text header_text;
        public Text btn_text;
        public Text percent_text;
        public GameObject percent_pb;

        int max_pb_with = 940;


        // Use this for initialization
        override public void ExtendedStart()
        {
            scanning_screen.SetActive(false);

            header_text.text = TextManager.getText("scanning_header_text");
            btn_text.text = TextManager.getText("scanning_btn_text");
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {

        }

        [Subscribe(UIMessageType.SCANNING_UPDATE_PERCENT)]
        public void UpdateValue(Message msg)
        {
            int value = (int)CastHelper.Cast<UpdateValueParametr>(msg.parametrs).value;
            percent_text.text = value.ToString() + " %";

            float tmp = percent_pb.GetComponent<RectTransform>().sizeDelta.y;
            percent_pb.GetComponent<RectTransform>().sizeDelta = new Vector2(value * max_pb_with / 100.0f, tmp);
        }

        [Subscribe(UIMessageType.OPEN_SCANNING_SCREEN)]
        public void Open(Message msg)
        {
            scanning_screen.SetActive(true);
        }

        [Subscribe(UIMessageType.CLOSE_SCANNING_SCREEN)]
        public void Close(Message msg)
        {
            scanning_screen.SetActive(false);
        }
    }
}