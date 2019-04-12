using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaga
{
    public class ExtendedBehaviour : MonoBehaviour
    {
        public MessageBus.Message empty_msg;
        public void EmptyUpdate() {}

        public virtual void ExtendedStart() { }
        public virtual void ExtendedUpdate() { }

        public System.Action update_action;

        List<IExtension> extensions;

        public void YagaUpdate()
        {
            foreach (IExtension extension in extensions)
            {
                extension.Update();
            }

            ExtendedUpdate();
        }

        private void Start()
        {
            empty_msg = new MessageBus.Message();
            update_action = YagaUpdate;

            var extension = GetType().GetCustomAttributes(
                typeof(Extension), true)[0] as Extension;

            extensions = new List<IExtension>();

            foreach(Extensions ext in extension.extensions)
            {
                switch (ext)
                {
                    case Extensions.LOCALIZATION:
                        extensions.Add(new Localization.UnityTextLocalizationExtension(this));
                        break;

                    case Extensions.SUBSCRIBE_MESSAGE:
                        extensions.Add(new SubscribeExtention(this));
                        break;

                    case Extensions.PAUSE:
                        extensions.Add(new PauseExtension(this));
                        break;
                }
            }            

            foreach (IExtension ext in extensions)
            {
                ext.Start();
            }

            ExtendedStart();
        }

        // Update is called once per frame
        private void Update()
        {
            update_action();
        }
    }
}
