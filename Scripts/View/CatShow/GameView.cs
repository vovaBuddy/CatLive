using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using UnityEngine.UI;

namespace CatShow
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class GameView : ExtendedBehaviour
    {

        public Text points;
        public Text coins;
        public Text energy;

        public Text way;
        public GameObject pb_way;
        float pb_way_max_size = 900;

        public GameObject pb_factor;
        float pb_factor_max_size = 900;

        [Subscribe(CatShowMessageType.UPDATE_ENERGY)]
        public void UpdEnergy(Message msg)
        {
            var param = CastHelper.Cast<UpdateInt>(msg.parametrs);
            energy.text = param.value.ToString();
        }

        [Subscribe(CatShowMessageType.UPDATE_COINS)]
        public void UpdCoins(Message msg)
        {
            var param = CastHelper.Cast<UpdateInt>(msg.parametrs);
            coins.text = param.value.ToString();
        }

        [Subscribe(CatShowMessageType.UPDATE_GAME_POINTS)]
        public void UpdPoints(Message msg)
        {
            var param = CastHelper.Cast<UpdateInt>(msg.parametrs);
            points.text = param.value.ToString();
        }

        [Subscribe(CatShowMessageType.OBSTACLE_UPDATE)]
        public void UpdObstacles(Message msg)
        {
            var param = CastHelper.Cast<UpdateInt>(msg.parametrs);
            way.text = (param.value * 10) + "/" + "250";

            var percent = param.value / 25.0f;

            float pb_width = (percent) * pb_way_max_size;
            float tmp = pb_way.GetComponent<RectTransform>().sizeDelta.y;
            pb_way.GetComponent<RectTransform>().sizeDelta =
                new Vector2(pb_width, tmp);
        }

        [Subscribe(CatShowMessageType.UPDATE_GAME_FACTOR)]
        public void UpdFactors(Message msg)
        {
            var param = CastHelper.Cast<UpdateFloat>(msg.parametrs);
            float percent = param.value - 1;


            float pb_width = (percent) * pb_factor_max_size;
            float tmp = pb_factor.GetComponent<RectTransform>().sizeDelta.y;
            pb_factor.GetComponent<RectTransform>().sizeDelta =
                new Vector2(pb_width, tmp);
        }

        [Subscribe(CatShowMessageType.UPDATE_GAME_TIME)]
        public void UpdTime(Message msg)
        {
            //var param = CastHelper.Cast<UpdateInt>(msg.parametrs);
            //time.text = param.value.ToString();
        }

        // Use this for initialization
        override public void ExtendedStart()
        {
            energy.text = DataController.instance.catsPurse.Energy.ToString();
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {

        }
    }
}