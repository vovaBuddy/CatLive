using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Yaga.Helpers
{
    public static class ResourceHelper
    {
        public static Sprite LoadSprite(string folder, string name)
        {
            return Resources.Load<Sprite>(folder + "/" + name);
        }

        public static Texture LoadTexture(string name)
        {
            return Resources.Load<Texture>(name);
        }

        public static Sprite LoadSprite(string name)
        {
            return Resources.Load<Sprite>(name);
        }
    }
}