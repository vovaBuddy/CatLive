using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames
{

    public interface CatControllerInterface
    {
        void InitCat();
        void Move();
        void OnTapAction();
        GameObject getCat();
    }
}
