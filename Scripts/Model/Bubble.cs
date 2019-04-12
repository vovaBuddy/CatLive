using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga.MessageBus;

namespace Main.Bubble
{
    public class Bubble : MonoBehaviour
    {
        public TextMesh text;
        public SpriteRenderer sprite;
        List<string> _replicas;

        public void Init(List<string> replicas)
        {
            _replicas = replicas;
        }

        public void Start()
        {
            StartCoroutine(next_or_close());
        }

        IEnumerator next_or_close()
        {
            foreach (string t in _replicas)
            {
                string result = t;

                if (t.Length < 40)
                {
                    sprite.size = new Vector2(0.33f * t.Length + 2, sprite.size.y);
                    sprite.transform.localPosition = new Vector3(sprite.size.x + 1.4f,
                        sprite.transform.localPosition.y,
                            sprite.transform.localPosition.z);

                    text.transform.localPosition = new Vector3(-(0.33f * t.Length) / 2,
                        text.transform.localPosition.y, text.transform.localPosition.z);


                    sprite.transform.localScale = new Vector3(2,
                        sprite.transform.localScale.y, sprite.transform.localScale.z);

                    text.transform.localScale = new Vector3(2,
                        text.transform.localScale.y, text.transform.localScale.z);
                }
                else if(t.Length > 40)
                {
                    result = "";
                    var splitted = t.Split(' ');

                    int first_lenght = 0;
                    int second_lenght = 0;
                    bool second = true;
                    
                    for(int i = 0; i < splitted.Length; i++)
                    {
                        if(i == splitted.Length / 2)
                        {
                            result += Environment.NewLine;
                            second = false;
                        }

                        if (!second)
                            first_lenght += (splitted[i] + " ").Length;
                        else
                            second_lenght += (splitted[i] + " ").Length;

                        result += splitted[i] + " ";
                    }

                    int lenght = first_lenght > second_lenght ? first_lenght : second_lenght;

                    sprite.transform.localScale = new Vector3(2,
                        sprite.transform.localScale.y, sprite.transform.localScale.z);

                    text.transform.localScale = new Vector3(2,
                        text.transform.localScale.y, text.transform.localScale.z);

                    sprite.size = new Vector2(0.33f * (lenght + 5), sprite.size.y * 1.3f);
                    sprite.transform.localPosition = new Vector3(sprite.size.x + 1.4f,
                        sprite.transform.localPosition.y,
                         sprite.transform.localPosition.z);

                    text.transform.localPosition = new Vector3(-(0.33f * t.Length) / 4,
                        text.transform.localPosition.y + 0.3f, text.transform.localPosition.z);

                }

                //if (true)
                if (Camera.main.WorldToScreenPoint(gameObject.transform.position).x > Screen.width / 2)
                {
                    sprite.transform.localScale = new Vector3(-2,
                         sprite.transform.localScale.y, sprite.transform.localScale.z);

                    text.transform.localScale = new Vector3(-2,
                        text.transform.localScale.y, text.transform.localScale.z);

                    sprite.transform.localPosition = new Vector3(-
                        sprite.size.x,
                        sprite.transform.localPosition.y,
                         sprite.transform.localPosition.z);

                    text.transform.localPosition = new Vector3((sprite.size.x / 2 - 0.75f),
                        text.transform.localPosition.y, text.transform.localPosition.z);
                }



                var res = "";
                for(int i = 0; i < result.Length; ++i)
                {
                    res += result[i];
                    text.text = res;
                    yield return new WaitForSeconds(0.05f);
                }

                yield return new WaitForSeconds(3.0f);
            }

            gameObject.SetActive(false);

            //yield return new WaitForSeconds(2.0f);

            MessageBus.Instance.SendMessage(BubbleAPI.PULL_QUEUE);

            Destroy(gameObject);
        }
    }
}