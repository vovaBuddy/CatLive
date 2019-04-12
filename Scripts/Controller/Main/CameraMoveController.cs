using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;
using Yaga;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class CameraMoveController : ExtendedBehaviour {

    [HideInInspector]
    List<Vector3> dests;

    public GameObject LeftDown;
    public GameObject RightUp;

    bool go_cat;
    bool init_go_cat;

    bool camera_touch_move;

    public void SetTouchMove(bool value)
    {
        camera_touch_move = value;
    }

    [Subscribe(MainScene.MainMenuMessageType.OPEN_CAT_SHOW)]
    public void GoCatShow(Message msg)
    {
        SetPosition("CatShow");
        go_cat = false;
        dests = null;
    }

    public static CameraMoveController GetController()
    {
        return GameObject.Find("Controllers").transform.Find("CameraMoveController")
            .GetComponent<CameraMoveController>();
    }

    bool init_done = false;

    [Subscribe(MainScene.MainMenuMessageType.OPEN_CUSTOMIZER,
        MainScene.MainMenuMessageType.OPEN_CUSTOMIZER_WITH_CLOSE,
        MainScene.MainMenuMessageType.OPEN_MINI_GAMES, 
        MainScene.MainMenuMessageType.OPEN_TASK_LIST, 
        MainScene.MainMenuMessageType.OPEN_SHOP, 
        MainScene.MainMenuMessageType.SHOW_SCAN_MENU)]
    public void OpenMenu(Message msg)
    {
        SetTouchMove(false);
    }

    [Subscribe(MainScene.MainMenuMessageType.CLOSE_CUSTOMIZER,
        MainScene.MainMenuMessageType.CLOSE_MINI_GAMES,
        MainScene.MainMenuMessageType.CLOSE_TASK_LIST,
        MainScene.MainMenuMessageType.CLOSE_SHOP,
        MainScene.MainMenuMessageType.SHOW_MAIN_MENU,
        MainScene.MainMenuMessageType.CLOSE_SCAN_MENU)]
    public void CloseMenu(Message msg)
    {
        SetTouchMove(true);
    }

    bool cat_show;

    [Subscribe(MainScene.MainMenuMessageType.OPEN_CAT_SHOW)]
    public void OpenCatShow(Message msg)
    {
        if (init_done)
        {
            cat_show = true;
            //Camera.main.orthographicSize = 5;
        }
        init_done = true;
    }

    IEnumerator zoom()
    {
        while (Camera.main.orthographicSize <= 18)
        {
            Camera.main.orthographicSize += .5f;
            yield return new WaitForSeconds(0.025f);
        }
    }
    [Subscribe(MainScene.MainMenuMessageType.ZOOM_CAMERA)]
    public void Zoom(Message msg)
    {
        SetPosition("t1");
        StartCoroutine(zoom());
    }

    [Subscribe(MainScene.MainMenuMessageType.SHOW_MAIN_MENU)]
    public void CloseCatShow(Message msg)
    {
        if (init_done)
        {
            cat_show = false;
            //Camera.main.orthographicSize = 7.15f;
        }
    }

    // Use this for initialization
    public override void ExtendedStart () {
        //go_cat = true;
        init_go_cat = true;
        camera_touch_move = true;

        if (DataController.instance.tasks_storage.content.ContainsKey("catshow_scene") &&
            (bool)DataController.instance.tasks_storage.content["catshow_scene"] == true)
        {
            cat_show = true;
            //Camera.main.orthographicSize = 5;
        }
        else
        {
            cat_show = false;
            //Camera.main.orthographicSize = 7.15f;
        }
    }

    public bool DoesReachDestination()
    {
        return dests == null;
    }

    public void GoCat()
    {
        go_cat = true;
    }

    public void GoCatOnce()
    {
        Vector3 new_pos = CatsMoveController.GetController()
            .GetCatLocation(Cats.Main);
        new_pos.z = Camera.main.transform.position.z;
        Camera.main.transform.position = new_pos;
    }

    public void StopGoCat()
    {
        go_cat = false;
    }

    public void SetPosition(Vector3 pos)
    {
        pos.z = Camera.main.transform.position.z;
        Camera.main.transform.position = pos;
    }

    public void SetPosition(string str)
    {
        var pos = GameObject.Find("CameraTasksTargets").transform.Find(str).position;
        pos.z = Camera.main.transform.position.z;
        Camera.main.transform.position = pos;
    }

    public void ClearDestination()
    {
        dests = null;
        index = 0;
        cur_dest = Vector3.zero;
    }

    public void SetDestinations(params string[] trace)
    {
        List<Vector3> points = new List<Vector3>();

        Transform points_go = GameObject.Find(trace[0]).transform;

        for (int i = 1; i < trace.Length; ++i)
        {
            points_go = points_go.transform.Find(trace[i]);
        }

        if (points_go.childCount > 0)
        {
            for (int i = 0; i < points_go.childCount; ++i)
            {
                points.Add(points_go.GetChild(i).position);
            }
        }
        else
        {
            points.Add(points_go.position);
        }

        SetDestinations(points);
    }

    public void SetDestinations(List<Vector3> points)
    {
        Debug.Log("SetDestinations " + points.Count);
        dests = points;
        index = 0;
        cur_dest = Vector3.zero;
    }

    public float speed = 0.001F;
    // Update is called once per frame
    public override void ExtendedUpdate() {

        if ((go_cat && !CatsMoveController.GetController().DoesCatReachDestination(Cats.Main)) || init_go_cat)
        { 
            Vector3 new_pos = CatsMoveController.GetController()
            .GetCatLocation(Cats.Main);
            new_pos.z = Camera.main.transform.position.z;
            Camera.main.transform.position = new_pos;
            init_go_cat = false;
        }
        else
        {
            if (camera_touch_move && !cat_show)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    // Get movement of the finger since last frame
                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                    // Move object across XY plane
                    Camera.main.transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);

                    if(Camera.main.transform.position.x < LeftDown.transform.position.x)
                    {
                        Camera.main.transform.position = new Vector3(LeftDown.transform.position.x,
                            Camera.main.transform.position.y, Camera.main.transform.position.z);
                    }

                    if (Camera.main.transform.position.y < LeftDown.transform.position.y)
                    {
                        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,
                            LeftDown.transform.position.y, Camera.main.transform.position.z);
                    }


                    if (Camera.main.transform.position.x > RightUp.transform.position.x)
                    {
                        Camera.main.transform.position = new Vector3(RightUp.transform.position.x,
                            Camera.main.transform.position.y, Camera.main.transform.position.z);
                    }

                    if (Camera.main.transform.position.y > RightUp.transform.position.y)
                    {
                        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,
                            RightUp.transform.position.y, Camera.main.transform.position.z);
                    }

                }
            }

            Move();
        }
    }

    Vector3 direction;
    private Vector3 cur_dest = Vector3.zero;
    private int index = 0;
    private float add_x = 0.00f;
    private float add_y = 0.00f;
    public void Move()
    {
        //Debug.Log(dests.Count);

        if (dests == null || dests.Count == 0)
            return;

        if (cur_dest == Vector3.zero)
        {
            cur_dest = dests[index];
            direction = cur_dest - Camera.main.transform.position;
            add_x = direction.x / (2 * (direction.x * direction.x + direction.y + direction.y));
            add_y = direction.y / (2 * (direction.x * direction.x + direction.y + direction.y));
        }

        if (Mathf.Abs(Camera.main.transform.position.x - cur_dest.x) <= 1 &&
            Mathf.Abs(Camera.main.transform.position.y - cur_dest.y) <= 1)
        {
            index++;

            if (dests.Count > index)
            {
                cur_dest = dests[index];

                direction = cur_dest - Camera.main.transform.position;
                add_x = direction.x / (2 *  (direction.x * direction.x + direction.y + direction.y));
                add_y = direction.y / (2 * (direction.x * direction.x + direction.y + direction.y));
            }
            else
            {
                dests = null;
                index = 0;
                cur_dest = Vector3.zero;
                return;
            }
        }

        //Vector3 pos = Vector3.Lerp(Camera.main.transform.position, cur_dest, 0.01f);
        Vector3 pos = Camera.main.transform.position + direction * 0.25f * Time.deltaTime;
        //Vector3 pos = new Vector3(
        //    Camera.main.transform.position.x + add_x,
        //    Camera.main.transform.position.y + add_y,
        //    Camera.main.transform.position.z);

        pos.z = Camera.main.transform.position.z;

        //Debug.Log("pos " + pos);

        Camera.main.transform.position = pos;
    }

}
