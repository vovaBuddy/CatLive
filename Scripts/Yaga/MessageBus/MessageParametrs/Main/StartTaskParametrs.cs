using System.Collections.Generic;
using UnityEngine;
using Yaga;

namespace MainScene
{
    public class StartTaskParametrs : MessageParametrs
    {
        public int index;
        public int price;
        public Vector3 btn_pos;

        public StartTaskParametrs(int indx, int prc, Vector3 pos)
        {
            index = indx;
            price = prc;
            btn_pos = pos;
        }
    }
}