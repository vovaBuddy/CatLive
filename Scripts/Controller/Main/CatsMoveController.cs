using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using Yaga.Storage;
using System;

public enum Cats
{
    Main,
    Black,
    Jakky,
    Gamer1,
    Gamer2,

    Baby1,
    Baby2,

    Kitchen1
}

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class CatsMoveController : ExtendedBehaviour {

    [Serializable]
    class SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        public SerializableVector3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public Vector3 toVector3()
        {
            return new Vector3(x, y, z);
        }
    }

    [Serializable]
    class CatPosInfoItem
    {
        public SerializableVector3 position;
        public float y_angle;
        public string path_point_name;
        public bool show;

        public CatPosInfoItem(bool show_in_start, string ppn)
        {
            position = null;
            y_angle = 0;
            show = show_in_start;
            path_point_name = ppn;
        }
    }

    [Serializable]
    class CatPosInfo
    {
        public Dictionary<Cats, CatPosInfoItem> items;

        public CatPosInfo()
        {
            items = new Dictionary<Cats, CatPosInfoItem>();
            items.Add(Cats.Main, new CatPosInfoItem(true, "Point 1"));
            items.Add(Cats.Black, new CatPosInfoItem(false, "Point 3"));
            items.Add(Cats.Jakky, new CatPosInfoItem(false, "Point 7"));

            items.Add(Cats.Gamer1, new CatPosInfoItem(false, "Point 3"));
            items.Add(Cats.Gamer2, new CatPosInfoItem(false, "Point 7"));

            items.Add(Cats.Baby1, new CatPosInfoItem(false, "Point 3"));
            items.Add(Cats.Baby2, new CatPosInfoItem(false, "Point 7"));

            items.Add(Cats.Kitchen1, new CatPosInfoItem(false, "Point 7"));
        }
    }



    class Move
    {

        enum DirectionToZero
        {
            FROM_UP,
            FROM_DOWN,
            NO
        }

        public List<Vector3> targets;
        public Vector3 cur_target;
        public int index;
        public Vector3 forward_vector;
        Vector3 speed = new Vector3(2.0f, 2.0f, 2.0f);
        public Transform transform;
        public GameObject curPathPoint;
        float angle;
        float angle_buffer;
        Vector3 _direction;
        public bool finished;

        DirectionToZero to_zero = DirectionToZero.NO;

        public Move(Transform tr)
        {
            transform = tr;
            forward_vector = transform.forward;
            finished = false;
        }


        public void SetDestination(List<Vector3> points)
        {
            targets = points;
            index = 0;
            cur_target = Vector3.zero;
        }

        public void DoMove()
        {
            if (targets == null || targets.Count == 0)
            {
                if (transform.gameObject.GetComponent<Animator>().GetInteger("state") == 1)
                    transform.gameObject.GetComponent<Animator>().SetInteger("state", 0);
                return;
            }
            else
            {
                if (transform.gameObject.GetComponent<Animator>().GetInteger("state") != 1)
                {
                    transform.gameObject.GetComponent<Animator>().Play("New State");
                }

                transform.gameObject.GetComponent<Animator>().SetInteger("state", 1);
            }

            if (cur_target == Vector3.zero)
            {
                cur_target = targets[index];


                var new_cur_target = Quaternion.Euler(-22.5f, -45, 22.5f) * cur_target;
                var new_transform_pos = Quaternion.Euler(-22.5f, -45, 22.5f) * transform.position;
                _direction = (cur_target - transform.position).normalized;
                float sign = (new_cur_target.y < new_transform_pos.y) ? -1.0f : 1.0f;
                angle = Vector3.Angle(forward_vector, _direction) * sign;

                if (angle < 0.0f)
                {
                    angle = 360 + angle;
                }
            }

            if (Mathf.Abs(transform.position.x - cur_target.x) <= 0.1 &&
                Mathf.Abs(transform.position.y - cur_target.y) <= 0.1)
            {
                index++;

                if (targets.Count > index)
                {
                    cur_target = targets[index];


                    var new_cur_target = Quaternion.Euler(-22.5f, -45, 22.5f) * cur_target;
                    var new_transform_pos = Quaternion.Euler(-22.5f, -45, 22.5f) * transform.position;
                    _direction = (cur_target - transform.position).normalized;
                    float sign = (new_cur_target.y < new_transform_pos.y) ? -1.0f : 1.0f;
                    angle = Vector3.Angle(forward_vector, _direction) * sign;

                    if (angle < 0.0f)
                    {
                        angle = 360 + angle;

                    //    if(transform.localEulerAngles.y + (360 - angle) <
                    //        angle - transform.localEulerAngles.y)
                    //    {
                    //        angle_buffer = angle;
                    //        angle = 0;
                    //        to_zero = DirectionToZero.FROM_UP;
                    //    }
                    //}

                    //else if((360 - transform.localEulerAngles.y) + angle <
                    //        transform.localEulerAngles.y - angle)
                    //{
                    //    angle_buffer = angle;
                    //    angle = 360;
                    //    to_zero = DirectionToZero.FROM_DOWN;
                    }
                }
                else
                {
                    targets = null;
                    index = 0;
                    cur_target = Vector3.zero;

                    finished = true;

                    return;
                }
            }

            //var new_cur_target = Quaternion.Euler(-22.5f, -45, 22.5f) * cur_target;
            //var new_transform_pos = Quaternion.Euler(-22.5f, -45, 22.5f) * transform.position;
            //var _direction = (cur_target - transform.position).normalized;
            //float sign = (new_cur_target.y < new_transform_pos.y) ? -1.0f : 1.0f;
            //var angle = Vector3.Angle(forward_vector, _direction) * sign;



            var angle_lerp = Mathf.Lerp(transform.Find("Armature").localEulerAngles.y, angle, 0.1f);

            //if(angle_lerp > 359.5f && to_zero == DirectionToZero.FROM_DOWN)
            //{
            //    to_zero = DirectionToZero.NO;
            //    angle = angle_buffer;
            //    //transform.localEulerAngles = new Vector3(0, 0, 0);
            //    angle_lerp = Mathf.Lerp(0, angle, 0.15f);
            //}
            //else if(angle_lerp < 0.25f && to_zero == DirectionToZero.FROM_UP)
            //{
            //    to_zero = DirectionToZero.NO;
            //    angle = angle_buffer;
            //    //transform.localEulerAngles = new Vector3(0, 0, 0);
            //    angle_lerp = Mathf.Lerp(360, angle, 0.01f);
            //}

            //if (Mathf.Abs(angle_lerp) > Mathf.Abs(angle))
            //    angle_lerp = angle;

            transform.Find("Armature").localEulerAngles = new Vector3(-90, -angle, 0);

            var new_pos = transform.position + (_direction * speed.x * Time.deltaTime);
            //new_pos.z = _move.position.z;
            transform.position = new_pos;
        }
    }

    StorableData<CatPosInfo> cat_pos_info;
    public Transform main_cat;
    public Transform black_cat;
    public Transform jakky_cat;

    public Transform gamer1_cat;
    public Transform gamer2_cat;

    public Transform baby1_cat;
    public Transform baby2_cat;

    public Transform kitchen1_cat;


    public PathFinder pf;

    Dictionary<Cats, Move> cats;

    [Subscribe(MainScene.MainMenuMessageType.OPEN_CAT_SHOW)]
    public void GoCatShow(Message msg)
    {
        foreach(var c in cats)
        {
            c.Value.targets = null;

            if (c.Key == Cats.Main)
            {
                c.Value.transform.position = GameObject.Find("CatSpace")
                    .transform.Find("Points").Find("CatShowMain").position;
            }
            else
            {
                c.Value.transform.position = GameObject.Find("CatSpace")
                    .transform.Find("Points").Find("CatShowAther").position;
            }
        }

        //cats[Cats.Main].transform.localEulerAngles = new Vector3(0, 130, 0);
        cats[Cats.Main].transform.Find("Armature").localEulerAngles = new Vector3(-90, 45, 0);
    }

    [Subscribe(MainScene.MainMenuMessageType.CLOSE_CAT_SHOW)]
    public void CloseCatShow(Message msg)
    {
        foreach (var item in cat_pos_info.content.items)
        {
            cats[item.Key].transform.position = item.Value.position.toVector3();
            cats[item.Key].transform.Find("Armature").localEulerAngles = new Vector3(-90, item.Value.y_angle, 0);
        }
    }

    // Use this for initialization
    void Awake () {
        cat_pos_info = new StorableData<CatPosInfo>("cat_pos_info");

        cats = new Dictionary<Cats, Move>();
        cats.Add(Cats.Main, new Move(main_cat));
        cats.Add(Cats.Black, new Move(black_cat));
        cats.Add(Cats.Jakky, new Move(jakky_cat));

        cats.Add(Cats.Gamer1, new Move(gamer1_cat));
        cats.Add(Cats.Gamer2, new Move(gamer2_cat));

        cats.Add(Cats.Baby1, new Move(baby1_cat));
        cats.Add(Cats.Baby2, new Move(baby2_cat));

        cats.Add(Cats.Kitchen1, new Move(kitchen1_cat));

        pf = new PathFinder();

        foreach (var item in cat_pos_info.content.items)
        {
            cats[item.Key].transform.gameObject.SetActive(item.Value.show);

            if (item.Value.position == null)
            {
                item.Value.position = new SerializableVector3(
                    cats[item.Key].transform.position.x, cats[item.Key].transform.position.y, cats[item.Key].transform.position.z);
                cat_pos_info.Store();
            }
            else
            {
                cats[item.Key].transform.position = item.Value.position.toVector3();
                cats[item.Key].transform.Find("Armature").localEulerAngles = new Vector3(-90, item.Value.y_angle, 0);
            }
        }

        //TEST

        //cat_pos_info.content.items[Cats.Black].path_point_name = "Point 11";

        //GameObject finish = GameObject.Find("CatSpace")
        //    .transform.Find("PathPoints").transform.Find("Point 58").gameObject;


        //CatsMoveController.GetController().
        //    SetDestination(Cats.Main, "Point 50");
        //CameraMoveController.GetController().GoCat();
    }

    public Transform GetTransform(Cats c)
    {
        return cats[c].transform;
    }

    public bool DoesCatReachDestination(Cats c)
    {
        return cats[c].targets == null;
    }

    public void SetDestination(Cats c, string path_point_name)
    {
        GameObject start = GameObject.Find("CatSpace")
            .transform.Find("PathPoints").transform
            .Find(cat_pos_info.content.items[c].path_point_name).gameObject;

        GameObject end = GameObject.Find("CatSpace")
            .transform.Find("PathPoints").transform.Find(path_point_name).gameObject;

        var points = pf.GetPathVectors(start, end);

        if (points.Count > 0)
        {
            cats[c].SetDestination(points);

            cat_pos_info.content.items[c].path_point_name = path_point_name;
            cat_pos_info.content.items[c].position =
                new SerializableVector3(
                    points[points.Count - 1].x, points[points.Count - 1].y, points[points.Count - 1].z);
            cat_pos_info.Store();
        }
    }

    public void ActiveCat(Cats c, bool value = true)
    {
        cats[c].transform.gameObject.SetActive(value);
        cat_pos_info.content.items[c].show = value;
        cat_pos_info.Store();
    }

    public void SetPosition(Cats c, string point_name)
    {
        var point = GameObject.Find("CatSpace")
            .transform.Find("PathPoints").transform.Find(point_name).position;
        cats[c].transform.position = point;

        cat_pos_info.content.items[c].position = new SerializableVector3(
                point.x, point.y, point.z); 
        cat_pos_info.Store();
    }

    public Vector3 GetCatLocation(Cats c)
    {
        return cats[c].transform.position;
    }


    float idle_timer = 0.0f;
    public override void ExtendedUpdate()
    {
        foreach(var move in cats)
        {
            move.Value.DoMove();

            if(move.Value.finished)
            {
                cat_pos_info.content.items[move.Key].y_angle =
                    cats[move.Key].transform.Find("Armature").localEulerAngles.y;

                cat_pos_info.Store();
            }
        }

        idle_timer -= Time.deltaTime;
        if (idle_timer <= 0.0f)
        {
            idle_timer = 15.0f;

            foreach (var c in cats)
            {
                Animator anim = c.Value.transform.gameObject.GetComponent<Animator>();

                if (anim.GetInteger("state") != 1)
                {
                    anim.SetInteger("state", 3);
                    anim.SetInteger("idle", UnityEngine.Random.Range(1, 6));
                }
            }
        }
    }

    public static CatsMoveController GetController()
    {
        return GameObject.Find("Controllers").transform.Find("CatsMoveController")
            .GetComponent<CatsMoveController>();
    }

}
