using System;
using System.Collections.Generic;
using System.Linq;
using Yaga;
using UnityEngine;

namespace PhotoScene
{
    class UpdateVector2Parametr : MessageParametrs
    {
        public Vector2 value;

        public UpdateVector2Parametr(Vector2 v)
        {
            value = v;
        }
    }
}
