using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace ZigZag
{

    public class DestroyableItem : MonoBehaviour
    {
        public bool debug = false;

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            if (screenPos.y < 0)
            {
                GameObject.Destroy(gameObject);
            }

            if (screenPos.y < 100)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}