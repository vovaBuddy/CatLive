using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;

namespace TimeManager.WinPanel
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class WinPanelController : ExtendedBehaviour
    {
        public WinPanelBinder binder;

        [Subscribe(WinPanelAPI.Messages.SHOW)]
        public void Show(Message msg)
        {
            binder.win_panel.SetActive(true);
        }

        public void PickUpBtn()
        {
            binder.win_panel.GetComponent<Animator>().SetBool("pickup", true);
        }
    }
}