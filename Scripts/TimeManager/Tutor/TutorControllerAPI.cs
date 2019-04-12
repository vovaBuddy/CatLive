using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeManager.Tutor;

namespace TimeManager.TutorAPI
{
    public static class Messages
    {
        public const string INIT = "TimeManager.TutorAPI.INIT";
        public const string CLOSE_TUTOR_MASK = "TimeManager.TutorAPI.CLOSE_TUTOR_MASK";
    }

    public class InitParams : Yaga.MessageParametrs
    {
        public List<TutorItem> items;

        public InitParams(List<TutorItem> i)
        {
            items = i;
        }
    }

}