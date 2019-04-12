using System;
using Yaga;
using UnityEngine;

namespace Minigames
{
    class UpdatePurseParametrs : MessageParametrs
    {
        public UpdatePurseParametrs(int c, int h, int s)
        {
            coins = c;
            hearts = h;
            stars = s;
        }

        public int coins;
        public int hearts;
        public int stars;
    }
}