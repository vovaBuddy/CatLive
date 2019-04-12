using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using UnityEngine.UI;
using Task;

namespace MainScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class TaskListView : ExtendedBehaviour
    {
        public Text header_text;
        public GameObject taskList;
        public GridLayoutGroup grid;

        public static TaskListView taskListView;

        [Subscribe(MainMenuMessageType.OPEN_TASK_LIST)]
        public void Open(Message msg)
        {
            if (DataController.instance.gamesRecords.StarMinigameNeed)
            {
                DataController.instance.gamesRecords.StarMinigameNeed = false;
            }

            //taskList.GetComponent<Animator>().SetBool("close", false);
            //if(!TaskController.GetController().check_any_task_in_action())
            taskList.SetActive(true);
        }

        [Subscribe(MainMenuMessageType.CLOSE_TASK_LIST, MainMenuMessageType.CLOSE_TASK_LIST_ONLY, MainMenuMessageType.OPEN_MINI_GAMES)]
        public void Close(Message msg)
        {
            taskList.GetComponent<Animator>().SetBool("close", true);
        }

        // Use this for initialization
        override public void ExtendedStart()
        {
            header_text.text = TextManager.getText("mm_tasks_header_text");
            taskList.SetActive(false);

            if (DataController.instance.gamesRecords.StarMinigameNeed)
            {
                Open(new Message());
            }

            if (taskListView == null)
                taskListView = gameObject.GetComponent<TaskListView>();
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {

        }
    }
}