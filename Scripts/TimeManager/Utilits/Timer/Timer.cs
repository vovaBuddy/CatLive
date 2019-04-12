using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager.Utilits
{
    public class Timer : MonoBehaviour
    {
        public GameObject pb;
        float def_width;
        SpriteRenderer sr;

        public void InitFull()
        {            
            sr = pb.GetComponent<SpriteRenderer>();
            def_width = sr.size.x;
            sr.size = new Vector2(0.0f, sr.size.y);            
        }

        public void TickUp(float seconds, float delta)
        {
            sr.size = new Vector2(sr.size.x + (def_width / seconds) * delta, sr.size.y);
            //pb.transform.localPosition = pb.transform.localPosition + def_pos * (1 / seconds) * delta;
        }
    }
}