using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;

namespace Minigames
{
    public class Rune : MonoBehaviour
    {
        public List<IRuneEffect> effects;
        public string msg_type;

        public void AddEffect(IRuneEffect effect)
        {
            if (effects == null)
                effects = new List<IRuneEffect>();

            effects.Add(effect);
        }
       
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "Player")
            {
                MessageBus.Instance.SendMessage(msg_type);
                if (gameObject.GetComponent<Rune>().msg_type == MiniGameMessageType.REVERSE_RUNE_CATCHED)
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
                else
                {
                    gameObject.GetComponent<Animator>().Play("coin_get", 0);
                    gameObject.GetComponent<Rune>().enabled = false;
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }

        public void Update()
        {
            //gameObject.transform.localRotation = 
            //    new Quaternion(, Time.deltaTime * 10, 0, 0);
        }

    }
}