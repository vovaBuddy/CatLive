using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.LoadingScreen.API
{
    public class LoadingScreenAPI 
    {
        public const string OPEN_CLOSE_ANIM = "Common.LoadingScreen.OPEN_CLOSE_ANIM";
        public const string OPEN_OPEN_ANIM = "Common.LoadingScreen.OPEN_OPEN_ANIM";
        public const string OPEN_ANIM_ON_START = "Common.LoadingScreen.OPEN_ANIM_ON_START";
        public const string OPEN_ANIM_ON_FINISH = "Common.LoadingScreen.OPEN_ANIM_ON_FINISH";
    }

    public class SceneNameParametr : Yaga.MessageParametrs
    {
        public string name;
        public bool show_ads;

        public SceneNameParametr(string n, bool ads)
        {
            name = n;
            show_ads = ads;
        }
    }


    public class SceneNames
    {
        public const string MAIN = "main";
        public const string SCANNING = "scanning";
        public const string MINIGAMES = "TM";
    }

}