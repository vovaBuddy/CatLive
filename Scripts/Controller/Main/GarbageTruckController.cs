using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageTruckController : MonoBehaviour {

    public GameObject truck;
    public List<GameObject> front_wheels;
    public List<GameObject> rear_wheels;

    public GameObject stop_point;

    public GameObject front_axis;
    public GameObject rear_axis;

    float speed_wheel_max = 500;
    public float speed_wheel;

    float speed_max = 3;
    public float speed;

    public bool stop;
    bool stoped = false;
    float stop_time = 2.0f;

    public bool end_garbage = false;
    public float end_garbage_timer = 2.0f;

    public static GarbageTruckController GetController()
    {
        return GameObject.Find("Controllers").transform.Find("GarbageTruckController")
            .GetComponent<GarbageTruckController>();
    }

    // Use this for initialization
    void Start () {
        truck.SetActive(true);

        truck.GetComponent<MeshRenderer>().sortingOrder = 300;
        truck.transform.Find("body_claw").GetComponent<MeshRenderer>().sortingOrder = 300;
    }
	
	// Update is called once per frame
	void Update () {
        if(stop_point.transform.position.x > truck.transform.position.x && !stoped)
        {
            stop = true;
            stoped = true;
        }

        if(stop_time > 0.0f && stoped)
        {
            stop_time -= Time.deltaTime;

            if(stop_time < 0.0f)
            {
                stop = false;

                MainLocationOjects.instance.trash_pack.SetActive(false);
                end_garbage = true;
            }
        }

        if (end_garbage)
            end_garbage_timer -= Time.deltaTime;

        if (truck.transform.localPosition.x < -1.0f)
        {
            truck.SetActive(false);
            gameObject.SetActive(false);
        }


        if(stop)
        {
            if (speed_wheel > 0.0f) 
                speed_wheel -= Mathf.Lerp(0.0f, speed_wheel, Time.deltaTime * 10f);

            if (speed_wheel < 0.0f)
                speed_wheel = 0.0f;

            if (speed > 0.0f)
                speed -= Mathf.Lerp(0.0f, speed, Time.deltaTime * 10f);

            if (speed < 0.0f)
                speed = 0.0f;
        }
        else
        {
            if(speed_wheel < speed_wheel_max)
                speed_wheel += Mathf.Lerp(speed_wheel, speed_wheel_max, Time.deltaTime * 0.0001f);

            if (speed < speed_max)
                speed += Mathf.Lerp(speed, speed_max, Time.deltaTime * 0.0001f);
        }


        foreach (var w in front_wheels)
        {
            w.transform.RotateAround(front_axis.transform.position, front_axis.transform.forward, speed_wheel * Time.deltaTime);
        }

        foreach (var w in rear_wheels)
        {
            w.transform.RotateAround(rear_axis.transform.position, rear_axis.transform.forward, speed_wheel * Time.deltaTime);
        }

        truck.transform.localPosition = new Vector3(truck.transform.localPosition.x - speed * Time.deltaTime,
            truck.transform.localPosition.y, truck.transform.localPosition.z);
    }
}
