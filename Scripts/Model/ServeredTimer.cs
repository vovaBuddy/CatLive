using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Text;
using Yaga.MessageBus;

public class ServeredTimer
{
    public delegate void AnsverDespatcher(ServerAnswer answ);
    AnsverDespatcher result_action;
    AnsverDespatcher bad_action;
    bool need_send_msg;

    public void GetTime(string event_name, AnsverDespatcher action,
        AnsverDespatcher bad_action)
    {
        string url = server_addres + get_event_address;

        url = url.Replace("{1}", Helper.DeviceNameHelper.GetDeviceName());
        url = url.Replace("{2}", event_name);

        result_action = action;
        this.bad_action = bad_action;
        need_send_msg = true;

        getRequest(url);
    }

    public void GetTimeToEndShow(AnsverDespatcher action,
        AnsverDespatcher bad_action)
    {
        string url = server_addres + get_time_to_end_show;

        result_action = action;
        this.bad_action = bad_action;
        need_send_msg = true;

        getRequest(url);
    }

    public void SetTime(string event_name, int time)
    {
        string url = server_addres + set_event_address;

        url = url.Replace("{1}", Helper.DeviceNameHelper.GetDeviceName());
        url = url.Replace("{2}", event_name);
        url = url.Replace("{3}", time.ToString());

        need_send_msg = false;

        getRequest(url);
    }

    string server_addres = "http://whatcat.pesya.ru";
    string get_event_address = "/api/get_event?player_id={1}&event_name={2}";
    string set_event_address = "/api/set_event?player_id={1}&event_name={2}&time={3}";
    string get_time_to_end_show = "/api/time_to_end_show";

    void getRequest(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.BeginGetResponse(OnAsyncCallback, request);
    }

    [Serializable]
    public class ServerAnswer
    {
        [Serializable]
        public class Data
        {
            public int time;
        }
        public string result;
        public Data data;
    }

    private void OnAsyncCallback(IAsyncResult asyncResult)
    {
        var httpWebRequest = (HttpWebRequest)asyncResult.AsyncState;
        WebResponse response = httpWebRequest.EndGetResponse(asyncResult);
        var reader = new StreamReader(response.GetResponseStream());
        string str = reader.ReadToEnd();
        var answ = JsonUtility.FromJson<ServerAnswer>(str);

        if(need_send_msg)
        {
            try
            {
                if (string.Equals(answ.result, "success"))
                {
                    result_action(answ);
                }
                else
                {
                    bad_action(answ);
                }
            }

            catch (Exception ex)
            {
                bad_action(answ);
            }
        }
    }
}
