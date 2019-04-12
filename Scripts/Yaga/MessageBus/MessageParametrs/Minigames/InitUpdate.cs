using System;
using Yaga;
using UnityEngine;

namespace Minigames
{
    class InitUpdate : MessageParametrs
    {
        public InitUpdate(Transform t, Transform r1, Transform r2)
        {
            platform_tr = t;
            rune1_templ = r1;
            rune2_templ = r2;
        }
        public Transform platform_tr;
        public Transform rune1_templ;
        public Transform rune2_templ;
    }
}