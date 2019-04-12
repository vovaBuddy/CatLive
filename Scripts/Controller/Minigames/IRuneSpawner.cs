using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;

namespace Minigames
{
    public interface IRuneSpawner
    {
        void Init();
        void InitPlatform(Message m);
        void UpdateInitPos(Message m);
        void Update();
    }
}