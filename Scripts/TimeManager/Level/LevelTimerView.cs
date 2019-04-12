using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using UnityEngine.UI;

namespace TimeManager.Level
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class LevelTimerView : ExtendedBehaviour
    {
        public Text time_text;

        [Subscribe(LevelAPI.Messages.TICK)]
        public void Tick(Message msg)
        {
            var param = Yaga.Helpers.CastHelper.Cast<LevelAPI.TickParametrs>(msg.parametrs);

            time_text.text = param.value.ToString();
        }

    }
}