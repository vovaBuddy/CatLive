using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager.Utilits
{
    public class CustomerTimer : MonoBehaviour
    {
        public GameObject pb;
        float def_height;
        SpriteRenderer sr;

        public void Init()
        {
            sr = pb.GetComponent<SpriteRenderer>();
            def_height = sr.size.y;
        }

        public void TickDown(float seconds, float delta)
        {
            sr.size = new Vector2(sr.size.x, sr.size.y - (def_height / seconds) * delta);

            if(sr.size.y < 0)
            {
                sr.size = new Vector2(sr.size.x, 0);
            }
        }


    }
}