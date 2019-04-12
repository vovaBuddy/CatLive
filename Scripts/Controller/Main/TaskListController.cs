using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;
using Yaga;
using Yaga.Helpers;
using Task;
using UnityEngine.UI;

namespace MainScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class TaskListController : ExtendedBehaviour {

        public GameObject taskList;

        public GameObject tasks_container;
        public TaskController tasksController;
        List<int> finishing_order = new List<int>();
        int cur_index;

        List<TaskItem> task_items = new List<TaskItem>();

        public GameObject pb;
        public Text pb_text;
        int pb_max_width = 890;
        public GameObject sign;

        public GameObject star_proto;

        public GameObject task_item_cont_template;

        Queue<GameObject> stars_to_start = new Queue<GameObject>();
        List<GameObject> move_stars = new List<GameObject>();
        Vector3 dest;
        bool seted_btn = false;
        Message msg_kash;

        public GameObject end_chapter_btn;
        public Text chapter_text;

        //[Subscribe("TASK_ITEM_CLOSE_END")]
        //public void CloseAnimEnd(Message msg)
        //{
        //    if(tasksController.GetDoneTaskInChapterCount() == DataController.instance.chapter_data.GetMissionsInCurChapter())
        //    {
        //        end_chapter_btn.SetActive(true);
        //    }
        //}

        bool wait = false;
        IEnumerator open_task_list()
        {
            yield return new WaitForSeconds(1.0f);

            foreach (var item in task_items)
            {
                if (tasksController.GetTaskInfo(item.task_index).data.done)
                {
                    item.gameObject.GetComponent<Animator>().SetBool("done", true);
                    wait = true;
                }

                if (tasksController.GetTaskInfo(item.task_index).data.ready_show &&
                        !tasksController.GetTaskInfo(item.task_index).data.idle)
                {
                    if(wait)
                    {
                        yield return new WaitForSeconds(1.5f);
                        wait = false;
                    }
                    item.gameObject.SetActive(true);
                    item.gameObject.GetComponent<Animator>().SetBool("ready_show", true);
                }
            }

        }



        [Subscribe(MainMenuMessageType.OPEN_TASK_LIST)]
        public void OpenTaskList(Message msg)
        {
            StartCoroutine(open_task_list());            
        }


        //IEnumerator close_task_list()
        //{
        //    foreach (var item in task_items)
        //    {
        //        item.gameObject.GetComponent<Animator>().SetBool("close", true);
        //        yield return new WaitForSeconds(0.1f);
        //    }
        //}
        //[Subscribe(MainMenuMessageType.CLOSE_TASK_LIST)]
        //public void CLoseTL(Message msg)
        //{
        //    StartCoroutine(close_task_list());
        //}

        [Subscribe(MainMenuMessageType.START_STAR_ANIMATION)]
        public void StartStarAnimation(Message msg)
        {
            msg_kash = msg;

            var param = CastHelper.Cast<StartTaskParametrs>(msg.parametrs);

            dest = param.btn_pos;

            seted_btn = true;

            for (int i = 0; i < param.price; ++i)
            {
                var star = GameObject.Instantiate(star_proto);
                star.transform.parent = star_proto.transform.parent;
                star.transform.position = star_proto.transform.position;
                star.transform.localScale = star_proto.transform.localScale;
                stars_to_start.Enqueue(star);
            }
        }

        public override void ExtendedStart()
        {
            chapter_text.text = TextManager.getText("mm_tasks_chapter_text").Replace("%N%",
                DataController.instance.chapter_data.GetCurChapter().ToString());
        }

        [Subscribe(MainMenuMessageType.INIT_TASKS)]
        public void TaskInit(Message msg)
        {
            pb_text.text = ((int)((tasksController.GetDoneTaskInChapterCount() / 
                (float)DataController.instance.chapter_data.GetMissionsInCurChapter()) * 100)).ToString() + "%";
            extended_pb_value = (tasksController.GetDoneTaskInChapterCount() / 
                (float)DataController.instance.chapter_data.GetMissionsInCurChapter()) * pb_max_width;
            float tmp = pb.GetComponent<RectTransform>().sizeDelta.y;
            pb.GetComponent<RectTransform>().sizeDelta =
                new Vector2(extended_pb_value, tmp);
        }

        bool need_up_pb;
        float extended_pb_value;


        [Subscribe(MainMenuMessageType.UPDATE_TASKS_PB)]
        public void UpdateTasks(Message msg)
        {
            var param = CastHelper.Cast<CommonMessageParametr>(msg.parametrs);
            List<int> data = (List<int>)param.obj;
            int value = data[0];

            finishing_order.Add(value);
        }

        [Subscribe(MainMenuMessageType.ADD_NEW_TASK_ITEM)]
        public void AddNewItem(Message msg)
        {
            var param = CastHelper.Cast<CommonMessageParametr>(msg.parametrs);
            var task = (Task.Task)param.obj;

            AddItem(task);

            if(!task.data.started)
                sign.SetActive(true);
        }

        float star_emit_timer = 0.0f;
        public override void ExtendedUpdate()
        {
            if (stars_to_start.Count > 0)
            {
                star_emit_timer -= Time.deltaTime;
                if (star_emit_timer <= 0)
                {
                    star_emit_timer = 0.2f;

                    move_stars.Add(stars_to_start.Dequeue());
                }
            }

            foreach (var s in move_stars)
            {
                var dist = Vector3.Distance(star_proto.transform.position, dest);
                var cur_dust = Vector3.Distance(star_proto.transform.position, s.transform.position);
                if (Vector3.Distance(s.transform.position, dest) < 0.1)
                {
                    move_stars.Remove(s);
                    Destroy(s);
                    continue;
                }
                var pos = Vector3.Lerp(star_proto.transform.position, dest, ((cur_dust) / dist) + Time.deltaTime);
                s.transform.position = pos;
            }

            if(seted_btn && move_stars.Count == 0 && stars_to_start.Count == 0)
            {
                msg_kash.Type = MainMenuMessageType.STARTED_TASK;
                MessageBus.Instance.SendMessage(msg_kash);
                Close();
                seted_btn = false;
            }

            foreach (TaskItem item in task_items)
            {
                Task.Task task_info = 
                    tasksController.GetTaskInfo(item.task_index);

                //if (task_info.data.show_finish_in_pb)
                //{
                //    item.gameObject.SetActive(false);
                //    task_items.Remove(item);
                //}

                if (task_info.data.done)
                {
                    item.btn_speed_up.SetActive(false);

                    item.check_done.SetActive(true);

                    //task_items.Remove(item);
                }

                else if(task_info.data.started)
                {
                    if (task_info.time_wait <= 0)
                    {
                        item.SetNoBtn();
                    }
                    else
                    {
                        task_info.Sales();

                        item.SetBtnSpeedUp(task_info.time_wait,
                            task_info.speed_up_price);
                    }
                }
            }

            if (need_up_pb && taskList.activeSelf)
            {
                float new_value = pb.GetComponent<RectTransform>().sizeDelta.x + Time.deltaTime * 30;

                if (new_value >= extended_pb_value)
                {
                    new_value = extended_pb_value;
                    need_up_pb = false;

                    Message msg = new Message();
                    msg.Type = MainMenuMessageType.DONE_TASK_IN_PB;
                    msg.parametrs = new UpdateInt(cur_index);
                    MessageBus.Instance.SendMessage(msg);

                    finishing_order.RemoveAt(0);
                }

                float tmp = pb.GetComponent<RectTransform>().sizeDelta.y;
                pb.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(new_value, tmp);
            }
            else if (finishing_order.Count > 0)
            {
                need_up_pb = true;

                pb_text.text = ((int)((tasksController.GetDoneTaskInChapterCount() / 
                    (float)DataController.instance.chapter_data.GetMissionsInCurChapter()) * 100)).ToString() + "%";
                extended_pb_value = (tasksController.GetDoneTaskInChapterCount() / 
                    (float)DataController.instance.chapter_data.GetMissionsInCurChapter()) * pb_max_width;

                cur_index = finishing_order[0];
            }
        }

        void AddItem(Task.Task task)
        {
            var obj = (GameObject)GameObject.Instantiate(Resources.Load("Main/TaskItem"), tasks_container.transform, false);

            if(task.icon_name != null)
            {
                obj.transform.Find("ic").GetComponent<Image>().sprite = Resources.Load<Sprite>(task.icon_name);
            }

            var task_item = obj.GetComponent<TaskItem>();
            task_items.Add(task_item);

            task_item.text.text = task.description;
            task_item.task_index = task.index;

            task_item.btn_speed_up.GetComponent<UnityEngine.UI.Button>()
            .onClick.AddListener(() =>
            {
                Message msg = new Message();
                msg.Type = MainMenuMessageType.SPEED_UP_TASK;
                msg.parametrs = new StartTaskParametrs(task.index,
                                    task.speed_up_price, Vector3.zero);

                MessageBus.Instance.SendMessage(msg);
                Close();
            });

            if (task.data.started)
            {
                if (task.speed_up_price <= 0)
                {
                    task_item.SetNoBtn();
                }
                else
                {
                    task_item.SetBtnSpeedUp(task.time_wait, task.speed_up_price);
                }
            }
            else
            {
                if (task.time_wait > 0)
                {
                    task_item.SetBtnStarsAndTime(task.stars_price,
                        task.time_wait);

                    task_item.btn_star_time.
                        GetComponent<UnityEngine.UI.Button>().onClick.
                        AddListener(() =>
                        {
                            if (move_stars.Count == 0 && stars_to_start.Count == 0)
                            {
                                Message msg = new Message();
                                msg.Type = MainMenuMessageType.START_TASK;
                                msg.parametrs = new StartTaskParametrs(task.index,
                                    task.stars_price, task_item.btn_star_time.transform.position);

                                MessageBus.Instance.SendMessage(msg);
                                //Close();
                            }
                        });
                }
                else
                {
                    task_item.SetBtnFinish();

                    if (task.stars_price == 0)
                    {
                        task_item.btn_finish.
                            GetComponent<UnityEngine.UI.Button>().onClick.
                            AddListener(() =>
                            {
                                if (move_stars.Count == 0 && stars_to_start.Count == 0)
                                {
                                    Message msg = new Message();
                                    msg.Type = MainMenuMessageType.START_TASK;
                                    msg.parametrs = new StartTaskParametrs(task.index,
                                        task.stars_price, task_item.btn_star.transform.position);

                                    MessageBus.Instance.SendMessage(msg);
                                    //Close();
                                }
                            });
                    }
                    else
                    {
                        task_item.SetBtnStar(task.stars_price);

                        task_item.btn_star.
                            GetComponent<UnityEngine.UI.Button>().onClick.
                            AddListener(() =>
                            {
                                if (move_stars.Count == 0 && stars_to_start.Count == 0)
                                {
                                    Message msg = new Message();
                                    msg.Type = MainMenuMessageType.START_TASK;
                                    msg.parametrs = new StartTaskParametrs(task.index,
                                        task.stars_price, task_item.btn_star.transform.position);

                                    MessageBus.Instance.SendMessage(msg);
                                //Close();
                            }
                            });
                    }
                }
            }

            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.transform.localPosition = new Vector3(490, 0, 0);
            //obj.SetActive(false);

            if (tasksController.GetTaskInfo(task.index).data.ready_show &&
                !tasksController.GetTaskInfo(task.index).data.idle)
            {
                obj.SetActive(false);
            }
        }

        public void Close()
        {
            if ( move_stars.Count == 0 && stars_to_start.Count == 0)
            {
                MessageBus.Instance.SendMessage(MainMenuMessageType.CLOSE_TASK_LIST);
            }
        }

        public void Open()
        {
            sign.SetActive(false);
            end_chapter_btn.SetActive(false);
            MessageBus.Instance.SendMessage(MainMenuMessageType.OPEN_TASK_LIST);
        }
    }
}