using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Yaga;

class GameStatistics : DontDestroy<GameStatistics>, Initable
{
    public void Init()
    {
        SendStat("start_app", 0);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            SendStat("app_has_focus", 0);
        }
        else
        {
            SendStat("app_lost_focus", 0);
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SendStat("app_pause_start", 0);
        }
        else
        {
            SendStat("app_pause_finish", 0);
        }
    }


    public void SendStat(string name, int value)
    {
        StartCoroutine(Send_stat("new_game_" + name, value));
        Debug.Log(name);
    }

    private IEnumerator Send_stat(string name, int value)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("value", value);
        form.AddField("online", "true");
        form.AddField("device_id", Helper.DeviceNameHelper.GetDeviceName());

        UnityWebRequest www = UnityWebRequest.Post("http://stat.pesya.ru/events", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}

