using System;
using Yaga;
using UnityEngine;

namespace Minigames
{
    class UpdateInitPos : MessageParametrs
    {
        public UpdateInitPos(Transform t)
        {
            transform = t;
        }
        public Transform transform;
    }
}