using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Yaga.Storage;

namespace Main.Chapter
{
    [Serializable]

    public class ChapterData
    {
        [Serializable]
        class ChapterEntity
        {
            public int cur_chapter;

            public ChapterEntity()
            {
                cur_chapter = 1;
            }
        }

        StorableData<ChapterEntity> entity;

        public ChapterData()
        {
            entity = new StorableData<ChapterEntity>("ChapterData");
        }

        public int GetCurChapter()
        {
            return entity.content.cur_chapter;
        }

        public void IncreaseChapter()
        {
            entity.content.cur_chapter++;
            entity.Store();
        }

        public int GetMissionsInCurChapter()
        {
            return ChapterInfo.missions_in_chapter[entity.content.cur_chapter - 1];
        }

        public int GetEndChapterTaskIndex()
        {
            return ChapterInfo.end_chapter_task_index[entity.content.cur_chapter - 1];
        }
    }



    public class ChapterInfo
    {
        public static readonly int[] end_chapter_task_index = new int[2] { 24, 25 };
        public static readonly int[] missions_in_chapter = new int[2] { 7, 15 };
        public static readonly List<DailyPrize>[] prizes_of_chapter = new List<DailyPrize>[1]{ new List<DailyPrize>()
            { new InfHeartsPrize(5),
              new CoinsPrize(1000) } };

    }

}