using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaga;

namespace PhotoScene
{
    class UpdateValueParametr : MessageParametrs
    {
        public float value;

        public UpdateValueParametr(float v)
        {
            value = v;
        }
    }
}
