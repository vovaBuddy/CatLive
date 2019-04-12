using Main.Bubble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaga.MessageBus;

namespace Task
{
    public delegate bool CondidionAction();

    public class TaskAction
    {       
        public Action action;
        public CondidionAction condition_action;
    }

    public class Task
    {
        bool sold10 = false;
        bool sold20 = false;
        bool sold30 = false;
        bool sold40 = false;
        bool sold50 = false;
        bool sold60 = false;
        bool sold70 = false;
        bool sold80 = false;
        bool sold90 = false;
        public void Sales()
        {
            if(initial_time > 0)
            {
                float percent = time_wait / (float)initial_time;

                percent = 1 - percent;

                if (percent >= 0.1f && percent < 0.2f && !sold10)
                {
                    sold10 = true;
                    speed_up_price = (int)(start_speed_up_price * 0.9f);

                    MessageBus.Instance.SendMessage(new Message(BubbleAPI.PUSH_TO_QUEUE,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_no_speedup_" +
                        UnityEngine.Random.Range(0,5).ToString()) }, 8)));
                }

                else if (percent >= 0.2f && percent < 0.3f && !sold20)
                {
                    sold20 = true;
                    speed_up_price = (int)(start_speed_up_price * 0.8f);

                    MessageBus.Instance.SendMessage(new Message(BubbleAPI.PUSH_TO_QUEUE,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_no_speedup_" +
                        UnityEngine.Random.Range(0,5).ToString()) }, 8)));
                }

                else if (percent >= 0.3f && percent < 0.4f && !sold30)
                {
                    sold30 = true;
                    speed_up_price = (int)(start_speed_up_price * 0.7f);

                    MessageBus.Instance.SendMessage(new Message(BubbleAPI.PUSH_TO_QUEUE,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_no_speedup_" +
                        UnityEngine.Random.Range(0,5).ToString()) }, 8)));
                }

                else if (percent >= 0.4f && percent < 0.5f && !sold40)
                {
                    sold40 = true;
                    speed_up_price = (int)(start_speed_up_price * 0.6f);

                    MessageBus.Instance.SendMessage(new Message(BubbleAPI.PUSH_TO_QUEUE,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_no_speedup_" +
                        UnityEngine.Random.Range(0,5).ToString()) }, 8)));
                }

                else if (percent >= 0.5f && percent < 0.6f && !sold50)
                {
                    sold50 = true;
                    speed_up_price = (int)(start_speed_up_price * 0.5f);

                    MessageBus.Instance.SendMessage(new Message(BubbleAPI.PUSH_TO_QUEUE,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_no_speedup_" +
                        UnityEngine.Random.Range(0,5).ToString()) }, 8)));
                }

                else if (percent >= 0.6f && percent < 0.7f && !sold60)
                {
                    sold60 = true;
                    speed_up_price = (int)(start_speed_up_price * 0.4f);

                    MessageBus.Instance.SendMessage(new Message(BubbleAPI.PUSH_TO_QUEUE,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_no_speedup_" +
                        UnityEngine.Random.Range(0,5).ToString()) }, 8)));
                }

                else if (percent >= 0.7f && percent < 0.8f && !sold70)
                {
                    sold70 = true;
                    speed_up_price = (int)(start_speed_up_price * 0.3f);

                    MessageBus.Instance.SendMessage(new Message(BubbleAPI.PUSH_TO_QUEUE,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_no_speedup_" +
                        UnityEngine.Random.Range(0,5).ToString()) }, 8)));
                }

                else if (percent >= 0.8f && percent < 0.9f && !sold80)
                {
                    sold80 = true;
                    speed_up_price = (int)(start_speed_up_price * 0.2f);

                    MessageBus.Instance.SendMessage(new Message(BubbleAPI.PUSH_TO_QUEUE,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_no_speedup_" +
                        UnityEngine.Random.Range(0,5).ToString()) }, 8)));
                }

                else if (percent >= 0.9f && !sold90)
                {
                    sold90 = true;
                    speed_up_price = (int)(start_speed_up_price * 0.1f);

                    MessageBus.Instance.SendMessage(new Message(BubbleAPI.PUSH_TO_QUEUE,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_no_speedup_" +
                        UnityEngine.Random.Range(0,5).ToString()) }, 8)));
                }
            }
        }

        public Action BeforeCutScene;
        public CondidionAction CutSceneCondition;
        public Action BeforeAction;
        public Action Idle;
        public Action CheckActionConditions;
        public CondidionAction DoneCondition;
        public Action DoneAction;
        public Action DoneInitAction;
        public Action Init;
        public Action TickAction;
        public bool init_done;

        public string icon_name;

        public List<TaskAction> TaskActions;

        public int index;
        public Data data;

        int initial_time;
        public int stars_price;
        public int time_wait;
        int start_speed_up_price;
        public int speed_up_price;
        public string description;

        public bool not_for_pb;
        bool _in_action;
        public bool in_action
        {
            set {
                _in_action = value;
            }
            get { return _in_action; }
        }

        public Task(int indx, int stars, int time, int speedup, 
            string desc, bool show, bool not_for)
        {
            stars_price = stars;
            time_wait = time;
            initial_time = time;
            speed_up_price = speedup;
            start_speed_up_price = speedup;
            index = indx;
            description = desc;
            not_for_pb = not_for;
            in_action = false;
            init_done = false;
        }

    }
}
