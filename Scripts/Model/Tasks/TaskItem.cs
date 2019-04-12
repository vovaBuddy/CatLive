using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour {

    public GameObject icon;
    public Text text;
    public int task_index;

    public GameObject btn_star;
    public Text btn_star_price;

    public GameObject btn_star_time;
    public Text btn_star_time_price;
    public Text btn_star_time_count;

    public GameObject btn_speed_up;
    public Text btn_speed_up_time;
    public Text btn_speed_up_price;

    public GameObject check_done;
    public GameObject btn_finish;

    // Use this for initialization
    void Start () {
        //btn_star.SetActive(false);
        //btn_star_time.SetActive(false);
        //btn_speed_up.SetActive(false);
    }

    public void SetBtnFinish()
    {
        btn_star.SetActive(false);
        btn_star_time.SetActive(false);
        btn_speed_up.SetActive(false);
        btn_finish.SetActive(true);
    }

    public void SetBtnStar(int star_cnt)
    {
        btn_star.SetActive(true);
        btn_star_time.SetActive(false);
        btn_speed_up.SetActive(false);
        btn_finish.SetActive(false);

        btn_star_price.text = star_cnt.ToString();
    }

    public void SetBtnStarsAndTime(int star_cnt, int time_cnt)
    {
        btn_star.SetActive(false);
        btn_star_time.SetActive(true);
        btn_speed_up.SetActive(false);
        btn_finish.SetActive(false);
        btn_star_time_price.text = star_cnt.ToString();
        btn_star_time_count.text = Helper.TextHelper.TimeFormatMinutes(time_cnt);
    }

    public void SetNoBtn()
    {
        btn_star.SetActive(false);
        btn_star_time.SetActive(false);
        btn_speed_up.SetActive(false);
        btn_finish.SetActive(false);
    }

    public void SetBtnSpeedUp(int time_cnt, int speedup_price)
    {
        btn_star.SetActive(false);
        btn_star_time.SetActive(false);
        btn_finish.SetActive(false);
        btn_speed_up.SetActive(true);
        btn_speed_up_time.text = Helper.TextHelper.TimeFormatMinutes(time_cnt);
        btn_speed_up_price.text = speedup_price.ToString();
    }

    // Update is called once per frame
    float sec_timer;

	void Update () {
        sec_timer -= Time.deltaTime;

        if(sec_timer <= 0)
        {
            sec_timer = 1.0f;
        }
    }
}
