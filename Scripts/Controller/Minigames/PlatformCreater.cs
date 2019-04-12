using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;

namespace Minigames
{
    public interface PlatformCreater
    {
        void LoadStartPlatform();
        void MovePlatform();
        void UpdatePlatform();
        void onBtnPressed(Message msg);
    }
}