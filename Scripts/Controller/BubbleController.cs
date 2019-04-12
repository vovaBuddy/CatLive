using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;

namespace Main.Bubble
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class BubbleController : ExtendedBehaviour
    {
        [Subscribe(MainScene.MainMenuMessageType.BOUGHT_ITEM)]
        public void Bought(Message msg)
        {
            DataController.instance.world_state_data.CustomizeToday();
        }

        //[Subscribe(MainScene.MainMenuMessageType.UPDATE_CATSHOW_COINS)]
        public void UpdateCoins(Message msg)
        {
            var p = Yaga.Helpers.CastHelper.Cast<UpdateInt>(msg.parametrs);

            if(p.value < 500)
            {
                MessageBus.Instance.SendMessage(new Message(BubbleAPI.PUSH_TO_QUEUE,
                new BubbleCreateParametr(
                    CatsMoveController.GetController().main_cat, new List<string>()
                        {TextManager.getText("bubble_money_" +
                            UnityEngine.Random.Range(0,4).ToString()) }, 8)));
            }

            PullQueue(new Message());
        }

        Dictionary<string, float> last_bubble;

        IEnumerator check_bubble_condition()
        {
            yield return new WaitForSeconds(2);

            if (DataController.instance.world_state_data.last_game_event == GAME_EVENT.SCANNED)
            {
                MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_" + GAME_EVENT.SCANNED + "_" +
                            UnityEngine.Random.Range(0,8).ToString()) }, 8)));

            }
            else if (DataController.instance.world_state_data.last_game_event == GAME_EVENT.NEW_RANK_REACHED)
            {
                MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_" + GAME_EVENT.NEW_RANK_REACHED + "_" +
                            0.ToString()) }, 5)));
            }
            else if (DataController.instance.world_state_data.last_game_event == GAME_EVENT.WON_MINIGAME)
            {
                MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_" + GAME_EVENT.WON_MINIGAME + "_" +
                            UnityEngine.Random.Range(0,10).ToString())}, 8)));
            }
            else if (DataController.instance.world_state_data.last_game_event == GAME_EVENT.LOSE_MINIGAME)
            {
                MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_" + GAME_EVENT.LOSE_MINIGAME + "_" +
                            UnityEngine.Random.Range(0,10).ToString())}, 8)));
            }

            if (DataController.instance.catsPurse.Coins < 500)
            {
                MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                new BubbleCreateParametr(
                    CatsMoveController.GetController().main_cat, new List<string>()
                        {TextManager.getText("bubble_money_" +
                        UnityEngine.Random.Range(0,4).ToString()) }, 8)));
            }

            if(DataController.instance.world_state_data.DaysWithOutCusomize > 0)
            {
                MessageBus.Instance.SendMessage(new Message(BubbleAPI.OPEN,
                    new BubbleCreateParametr(
                        CatsMoveController.GetController().main_cat, new List<string>()
                            {TextManager.getText("bubble_no_customize_" +
                                            UnityEngine.Random.Range(0,5).ToString()) }, 8)));
            }

            //PullQueue(new Message());

            DataController.instance.world_state_data.last_game_event = GAME_EVENT.NON;

            yield return new WaitForSeconds(25);
        }
        
        public override void ExtendedStart()
        {
            ////tests
            //MessageBus.Instance.SendMessage(new Message(BubbleAPI.PUSH_TO_QUEUE,
            //new BubbleCreateParametr(
            //    CatsMoveController.GetController().main_cat, new List<string>()
            //        { "Иногда хочется путешествовать." })));            

            last_bubble = new Dictionary<string, float>();

            StartCoroutine(check_bubble_condition());
        }

        public GameObject bubble_prefub;
        Queue<Message> queue = new Queue<Message>();

        [Subscribe(BubbleAPI.PUSH_TO_QUEUE)]
        public void PushToQueue(Message msg)
        {
            queue.Enqueue(msg);
        }

        IEnumerator create_buuble(Message msg)
        {
            yield return new WaitForSeconds(0.5f);

            var param = Yaga.Helpers.CastHelper.Cast<BubbleCreateParametr>(msg.parametrs);

            if (DialogController.GetController().DialogWindow.activeSelf ||
                MainScene.TaskListView.taskListView.taskList.activeSelf ||
                Task.TaskController.GetController().check_any_task_in_action())
            {
                if (param.tap_action)
                    yield break;

                queue.Enqueue(msg);
                yield break;
            }

            foreach(var value in last_bubble)
            {
                if (Time.time - value.Value < param.seconds_from_prev)
                {
                    if (param.tap_action)
                        yield break;

                    yield return new WaitForSeconds(param.seconds_from_prev -
                        (Time.time - value.Value));
                }
            }
            //if (last_bubble.ContainsKey(param.owner.gameObject.name))
            //{
            //    if (Time.time - last_bubble[param.owner.gameObject.name] < param.seconds_from_prev)
            //    {
            //        if (param.tap_action)
            //            yield break;

            //        yield return new WaitForSeconds(param.seconds_from_prev - 
            //            (Time.time - last_bubble[param.owner.gameObject.name]));
            //    }
            //}

            last_bubble[param.owner.gameObject.name] = Time.time;

            var bubble = Instantiate(bubble_prefub, param.owner, false);
            bubble.GetComponent<Bubble>().Init(param.messages);
        }

        [Subscribe(BubbleAPI.OPEN)]
        public void CreateBubble(Message msg)
        {
            StartCoroutine(create_buuble(msg));
        }


        IEnumerator check_pull()
        {
            yield return new WaitForSeconds(0.5f);

            if (!Task.TaskController.GetController().check_any_task_in_action())
            {
                if (queue.Count > 0)
                {
                    var m = queue.Dequeue();
                    m.Type = BubbleAPI.OPEN;
                    MessageBus.Instance.SendMessage(m);
                }
            }
        }

        [Subscribe(BubbleAPI.PULL_QUEUE,
            MainScene.MainMenuMessageType.CLOSE_TASK_LIST, 
            DailyPrizesAPI.Messages.CLOSE,
            DialogControllerAPI.Message.CLOSED)]
        public void PullQueue(Message msg)
        {
            StartCoroutine(check_pull());
        }
    }

    public static class BubbleAPI
    {
        public const string OPEN = "Main.Bubble.OPEN";
        public const string PUSH_TO_QUEUE = "Main.Bubble.PUSH_TO_QUEUE";
        public const string PULL_QUEUE = "Main.Bubble.PULL_QUEUE";
    }

    public class BubbleCreateParametr : MessageParametrs
    {
        public Transform owner;
        public List<string> messages;
        public int seconds_from_prev;
        public bool tap_action;

        public BubbleCreateParametr(Transform o, List<string> strs, int prev, bool tap = false)
        {
            owner = o;
            messages = strs;
            seconds_from_prev = prev;
            tap_action = tap;
        }
    }
}

