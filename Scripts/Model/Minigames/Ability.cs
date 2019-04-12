using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames
{
    public class Ability : MonoBehaviour {

        bool use;
        Transform target;
        float speed = 5.0f;
        // Use this for initialization
        void Start()
        {
            target = GameObject.Find("Controllers").transform.Find("CatController").GetComponent<CatController>().cat_ref.transform;
            use = BusterController.getController().use_magnit;
        }

        void Update() {
            if (!use)
                return;

            if (Vector3.Distance(transform.position, target.position) < 2)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }
        }
    }
}
