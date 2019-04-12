using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDataController : MonoBehaviour {

    public static TimerDataController GetController()
    {
        return GameObject.Find("Controllers").transform.Find("CatPurseController")
            .GetComponent<TimerDataController>();
    }

    int hearts_timer;
    int inf_hearts_timer;

    ServeredTimer inf_hearts_servtimer;
    bool inf_hearts_inited = false;

    int HEARTS_TIME = 5 * 60;
    ServeredTimer hearts_servtimer;
    public Text hearts_text;
    bool hearts_inited = false;

    int energy_timer;
    int ENERGY_TIME = 5 * 60;
    ServeredTimer energy_servtimer;
    public Text energy_text;
    bool energy_inited = false;

    bool invoked = false;
    public void Start()
    {
        if (!invoked)
        {
            invoked = true;
            InvokeRepeating("SecondUpdate", 0, 1.0f);
        }

        hearts_servtimer = new ServeredTimer();
        hearts_servtimer.GetTime("hearts_timer",
            (answ) =>
            {
                hearts_timer = answ.data.time;
                hearts_inited = true;
            },
            (answ) =>
            {
                hearts_timer = HEARTS_TIME;
                hearts_servtimer.SetTime("hearts_timer", hearts_timer);
                hearts_inited = true;
            });

        inf_hearts_servtimer = new ServeredTimer();
        inf_hearts_servtimer.GetTime("infinity_heart",
            (answ) =>
            {
                inf_hearts_timer = answ.data.time;
                inf_hearts_inited = true;

                if(inf_hearts_timer > 0)
                {
                    DataController.instance.catsPurse.InfinityHearts = true;
                }
            },
            (answ) =>
            {
                inf_hearts_inited = false;
            });

        energy_servtimer = new ServeredTimer();
        energy_servtimer.GetTime("energy_timer",
            (answ) =>
            {
                energy_timer = answ.data.time;
                energy_inited = true;
            },
            (answ) =>
            {
                energy_timer = ENERGY_TIME;
                energy_servtimer.SetTime("energy_timer", energy_timer);
                energy_inited = true;
            });
    }

    float now;
    float hdelta;
    float edelta;
    public void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            now = Time.realtimeSinceStartup;
        }
        else
        {
            hdelta = Time.realtimeSinceStartup - now;
            edelta = Time.realtimeSinceStartup - now;
        }
    }

    void SecondUpdate()
    {
        if (energy_inited)
        {
            energy_timer -= 1;
            energy_timer -= (int)edelta;

            edelta = 0;

            energy_text.text = Helper.TextHelper.TimeFormatMinutes(energy_timer);

            if (energy_timer < 1)
            {
                if(energy_timer < -ENERGY_TIME)
                {
                    int additional = Mathf.Abs(energy_timer / ENERGY_TIME);

                    if (additional > 0)
                        DataController.instance.catsPurse.Hearts += additional;

                }

                energy_timer = ENERGY_TIME - Mathf.Abs(energy_timer) % ENERGY_TIME;
                energy_servtimer.SetTime("energy_timer", energy_timer);
                DataController.instance.catsPurse.Energy += 1;
            }
        }

        if (hearts_inited)
        {
            if (DataController.instance.catsPurse.InfinityHearts)
            {
                if (inf_hearts_inited)
                {
                    inf_hearts_timer -= 1;
                    hearts_text.text = Helper.TextHelper.TimeFormatMinutes(inf_hearts_timer);

                    if (inf_hearts_timer < 1)
                    {
                        DataController.instance.catsPurse.InfinityHearts = false;
                    }
                }
            }
            else
            {
                hearts_timer -= 1;
                hearts_timer -= (int)hdelta;

                hdelta = 0;               

                if (hearts_timer < 1)
                {
                    if (hearts_timer < -HEARTS_TIME)
                    {
                        int additional = Mathf.Abs(hearts_timer / HEARTS_TIME);

                        if (additional > 0)
                            DataController.instance.catsPurse.Hearts += additional;

                    }

                    hearts_timer = HEARTS_TIME - Mathf.Abs(hearts_timer) % HEARTS_TIME;
                    hearts_servtimer.SetTime("hearts_timer", hearts_timer);
                    DataController.instance.catsPurse.Hearts += 1;
                }

                hearts_text.text = Helper.TextHelper.TimeFormatMinutes(hearts_timer);
            }
        }
    }
}
