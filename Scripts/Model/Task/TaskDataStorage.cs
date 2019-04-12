using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaga.Storage;

namespace Task
{
    [Serializable]
    public class Data
    {
        public bool cut_scene_showed;
        public int current_action_index;
        public bool done;
        public bool show_finish_in_pb;
        public bool started;

        public bool ready_show;
        public bool idle;
        public bool ready_done;

        public Data()
        {
            cut_scene_showed = false;
            done = false;
            started = false;
            current_action_index = 0;
            show_finish_in_pb = false;

            ready_show = false;
            idle = false;
            ready_done = false;
        }
    }

    [Serializable]
    public class DataEntity
    {
        public List<Data> storable_data;

        public int done_mission_cnt;

        //todo
        //>= 28 идут миссии завершения глав
        public DataEntity()
        {
            storable_data = new List<Data>();

            for (int i = 0; i < 30; ++i)
            {
                storable_data.Add(new Data());
            }

            done_mission_cnt = 0;
        }
    }
}
