using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;

namespace Main.Chapter
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class ChapterController : ExtendedBehaviour
    {
        ChapterBinder binder;

        [Subscribe(API.Messages.OPEN_PRIZE_SCREEN)]
        public void OpenPrize(Message msg)
        {
            binder.chapter_prize_panel.SetActive(true);

            Message start_task_msg = new Message(MainScene.MainMenuMessageType.STARTED_TASK, 
                new MainScene.StartTaskParametrs(DataController.instance.chapter_data.GetEndChapterTaskIndex(), 0, new Vector3()));
            MessageBus.Instance.SendMessage(start_task_msg);
                
            MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_TASK_LIST_ONLY);

            int cur_chapter = DataController.instance.chapter_data.GetCurChapter();

            foreach(var prize in ChapterInfo.prizes_of_chapter[cur_chapter - 1])
            {
                if(prize is BoosterPrize)
                {
                    var p = prize as BoosterPrize;

                    binder.booster_cnt.text = "x" + p.count.ToString();
                    var sprite_name = p.GetIconName();
                    binder.booster_img.sprite = Resources.Load<Sprite>(sprite_name);
                    binder.name = p.GetName();
                }
                else if (prize is CoinsPrize)
                {
                    var p = prize as CoinsPrize;

                    binder.coins_cnt.text = p.coins.ToString();
                }
                else if (prize is InfHeartsPrize)
                {
                    var p = prize as InfHeartsPrize;

                    binder.inf_hearts_timer.text = Helper.TextHelper.TimeFormatMinutes(p.minutes * 60);
                }
            }
        }

        public override void ExtendedStart()
        {
            binder = transform.parent.Find("ChapterBinder").GetComponent<ChapterBinder>();
        }

        public void PickUp()
        {
            binder.chapter_prize_panel.GetComponent<Animator>().SetBool("pickup", true);

            int cur_chapter = DataController.instance.chapter_data.GetCurChapter();

            foreach (var prize in ChapterInfo.prizes_of_chapter[cur_chapter - 1])
            {
                prize.ActiveAction();
            }

            DataController.instance.chapter_data.IncreaseChapter();

            GameStatistics.instance.SendStat("pick_up_chapter_prize", 0);
        }
    }
}
