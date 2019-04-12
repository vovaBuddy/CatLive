using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Text;
using Yaga.MessageBus;

public class ServeredData
{
    public delegate void AnsverDespatcher<T>(T answ);
    public delegate void BadAnsverDespatcher();
    AnsverDespatcher<ServerAnswer> result_action;
    AnsverDespatcher<PrizeAnswer> prize_result_action;
    AnsverDespatcher<ServerAnswerScoreboard> scoreboard_result_action;
    AnsverDespatcher<TimeAnswer> time_result_action;
    BadAnsverDespatcher bad_action;
    bool need_send_msg;

    public void GetCurDateTime(AnsverDespatcher<TimeAnswer> action,
        BadAnsverDespatcher bad_action)
    {
        string url = server_addres + current_time;

        time_result_action = action;
        this.bad_action = bad_action;
        need_send_msg = true;

        getRequest<TimeAnswer>(url);
    }

    public void GetPrize(AnsverDespatcher<PrizeAnswer> action,
        BadAnsverDespatcher bad_action)
    {
        string url = server_addres + prize;

        url = url.Replace("{1}", Helper.DeviceNameHelper.GetDeviceName());

        prize_result_action = action;
        this.bad_action = bad_action;
        need_send_msg = true;

        getRequest<PrizeAnswer>(url);
    }

    public void GetScoreBoard(AnsverDespatcher<ServerAnswerScoreboard> action,
        BadAnsverDespatcher bad_action)
    {
        string url = server_addres + scoreboard_adress;

        url = url.Replace("{1}", Helper.DeviceNameHelper.GetDeviceName());

        scoreboard_result_action = action;
        this.bad_action = bad_action;
        need_send_msg = true;

        getRequest<ServerAnswerScoreboard>(url);
    }

    public void GetValue(AnsverDespatcher<ServerAnswer> action,
        BadAnsverDespatcher bad_action)
    {
        string url = server_addres + get_score_address;

        url = url.Replace("{1}", Helper.DeviceNameHelper.GetDeviceName());

        result_action = action;
        this.bad_action = bad_action;
        need_send_msg = true;

        getRequest<ServerAnswer>(url);
    }

    public void SetValue(int value)
    {
        string url = server_addres + set_score_address;

        url = url.Replace("{1}", Helper.DeviceNameHelper.GetDeviceName());
        url = url.Replace("{2}", value.ToString());

        need_send_msg = false;

        getRequest<ServerAnswer>(url);
    }

    string server_addres = "http://whatcat.pesya.ru";
    string get_score_address = "/api/get_score?player_id={1}";
    string set_score_address = "/api/set_score?player_id={1}&score={2}";
    string scoreboard_adress = "/api/scoreboard?player_id={1}";
    string prize = "/api/get_prize?player_id={1}";
    string current_time = "/api/get_cur_time";

    void getRequest<T>(string url) where T : IResultedData
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.BeginGetResponse(OnAsyncCallback<T>, request);
    }

    public interface IResultedData
    {
        string getResult();
    }

    [Serializable]
    public class TimeAnswer : IResultedData
    {
        [Serializable]
        public class Data
        {
            public string time;
        }

        public string result;
        public Data data;

        public string getResult()
        {
            return result;
        }
    }


    [Serializable]
    public class PrizeAnswer : IResultedData
    {
        [Serializable]
        public class Data
        {
            public int cnt;
            public int place;
            public int value;
        }

        public string result;
        public Data data;

        public string getResult()
        {
            return result;
        }
    }

    [Serializable]
    public class ServerAnswerScoreboard : IResultedData
    {
        [Serializable]
        public class Datum
        {
            public string id;
            public int score;
        }


        [Serializable]
        public class Data
        {
            public int place;
            public List<Datum> data;
        }

        public string result;
        public Data data;

        public string getResult()
        {
            return result;
        }
    }

    [Serializable]
    public class ServerAnswer : IResultedData
    {
        [Serializable]
        public class Data
        {
            public int score;
        }
        public Data data;
        public string result;

        public string getResult()
        {
            return result;
        }
    }

    private void OnAsyncCallback<T>(IAsyncResult asyncResult) where T : IResultedData
    {
        var httpWebRequest = (HttpWebRequest)asyncResult.AsyncState;
        WebResponse response = httpWebRequest.EndGetResponse(asyncResult);
        var reader = new StreamReader(response.GetResponseStream());
        string str = reader.ReadToEnd();
        var answ = JsonUtility.FromJson<T>(str.Replace(Helper.DeviceNameHelper.GetDeviceName(), "ВЫ"));

        if (need_send_msg)
        {
            try
            {
                if (string.Equals(answ.getResult(), "success"))
                {
                    if (answ is ServerAnswerScoreboard)
                    {
                       scoreboard_result_action(answ as ServerAnswerScoreboard);
                    }
                    else if(answ is ServerAnswer)
                    {
                       result_action(answ as ServerAnswer);
                    }
                    else if(answ is PrizeAnswer)
                    {
                        prize_result_action(answ as PrizeAnswer);
                    }
                    else if (answ is TimeAnswer)
                    {
                        time_result_action(answ as TimeAnswer);
                    }
                }
                else
                {
                    bad_action();
                }
            }

            catch (Exception ex)
            {
                bad_action();
            }
        }
    }
}
