using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.Storage;
using Yaga;
using Yaga.MessageBus;

public class TutorCatShow
{
    [Serializable]
    public class tcsData
    {
        public bool done;
        public int cur_place;
        public int cur_value;
        public int play_count;

        public tcsData()
        {
            done = false;
            cur_place = 50;
            cur_value = 0;
            play_count = 0;
        }
    }

    public StorableData<tcsData> data;
    public ServeredTimer end_catshow_timer;

    public TutorCatShow()
    {
        data = new StorableData<tcsData>("tcsData");
        end_catshow_timer = new ServeredTimer(); 
    }

    string random_str()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 8)
          .Select(s => s[UnityEngine.Random.Range(0, chars.Length)]).ToArray());
    }

    public void UpdateValue(int new_value)
    {
        if(data.content.cur_value != new_value)
        {
            data.content.cur_value = new_value;
            data.content.cur_place -= UnityEngine.Random.Range(-1, 3);

            if (data.content.cur_place < 26)
            {
                data.content.cur_place = 26;
            }
            else if(data.content.cur_place > 50)
            {
                data.content.cur_place = 50;
            }

            data.Store();
        }
    }

    public void GetTutorSCoreBoard()
    {
        int place = data.content.cur_place;

        for (int i = 0; i < 4; ++i)
        {
            object[] objs = new object[3];
            objs[0] = data.content.cur_value + (5 - i) * (UnityEngine.Random.Range(10, 15));
            objs[1] = random_str();
            objs[2] = place - 4 + i;

            Message msg = new Message();
            msg.parametrs = new CommonMessageParametr(objs);
            msg.Type = CatShow.CatShowMessageType.ADD_RAIT_ITEM;
            MessageBus.Instance.SendMessage(msg, true);
        }

        {
            object[] objs = new object[3];
            objs[0] = data.content.cur_value;
            objs[1] = "ВЫ";
            objs[2] = place;

            Message msg = new Message();
            msg.parametrs = new CommonMessageParametr(objs);
            msg.Type = CatShow.CatShowMessageType.ADD_RAIT_ITEM;
            MessageBus.Instance.SendMessage(msg, true);
        }

        for (int i = 0; i < 4; ++i)
        {
            object[] objs = new object[3];
            objs[0] = data.content.cur_value - (i + 1) * (UnityEngine.Random.Range(10, 15));
            objs[0] = (int)objs[0] < 0 ? 0 : objs[0];
            objs[1] = random_str();
            objs[2] = place + i + 1;

            Message msg = new Message();
            msg.parametrs = new CommonMessageParametr(objs);
            msg.Type = CatShow.CatShowMessageType.ADD_RAIT_ITEM;
            MessageBus.Instance.SendMessage(msg, true);
        }
    }

}

