using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasks
{
    public class EmptyTask : TaskAction
    {
        public void StartAction()
        {
            Debug.Log("Empty Task Sart");
        }


        public void IdleAction()
        {
        }

        public void EndAction()
        {
            Debug.Log("Empty Task End");
        }

        public void DoneInit()
        {
            Debug.Log("Empty Task Done Action");
        }

        public bool CheckItDone()
        {
            return false;
        }

        public void FirstAction(TaskEntity info)
        {
            throw new NotImplementedException();
        }
    }
}
