using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;

namespace Minigames
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class FxController : ExtendedBehaviour
    {
        ParticleSystem fx;
        Light light;

        

        [Subscribe(MiniGameMessageType.INIT_PLATFORM)]
        public void Init(Message msg)
        {
            var param = CastHelper.Cast<InitUpdate>(msg.parametrs);

            fx = param.platform_tr.parent.Find("fx_shrink").GetComponent<ParticleSystem>();
            light = param.platform_tr.parent.Find("light").GetComponent<Light>();

        }

        [Subscribe(MiniGameMessageType.BOX_CRASH)]
        public void BoxCrash(Message msg)
        {
            fx.Play();
            light.intensity = 5.0f;
        }

        // Use this for initialization
        override public void ExtendedStart()
        {

        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {
            if (light != null)
            {
                if (light.intensity > 1.0f)
                {
                    light.intensity -= Time.deltaTime * 10;
                }
            }
        }
    }
}