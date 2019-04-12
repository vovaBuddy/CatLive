using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLocationOjects : MonoBehaviour {

    public GameObject trash;
    public GameObject trash_pack;
    public GameObject kitchen;
    public GameObject tv_zone;
    public GameObject tv_farm;
    public GameObject sleep_room;
    public GameObject sleep_room_farm;
    public GameObject music;
    public GameObject music_farm;

    public GameObject garden2_staf;
    public GameObject garden2_staf_farm;
    public GameObject garden2_bran;
    public GameObject garden2_bran_closed;

    public GameObject garden1_benches;
    public GameObject garden1_floor;
    public GameObject garden1_bushes;
    public GameObject garden1_pond;
    public GameObject garden1_pond_farm;
    public GameObject garden1_pond_bad;

    public GameObject Children_zone;
    public GameObject Children_boxes;
    public GameObject Children_obstruction;
    public GameObject Children_obstruction_farm;
    public GameObject Children_stuff_farm;

    public GameObject home_floor;
    public GameObject kitchen_floor;

    public GameObject home_wall;
    public GameObject home_wall_win;
    public GameObject home_wall_door;

    public GameObject door_wc;
    public GameObject door_garden;

    public GameObject obstruction_toilet;
    public GameObject obstruction_toilet_farm;

    public GameObject kitche_wall;

    public GameObject cat_show;

    static MainLocationOjects _instance;

    public GameObject GarbageTruckController;

    //tutors
    public GameObject arrow_kitchen;
    public GameObject customize_info_tutor;


    //UI
    public GameObject tasks_btn_footer;
    public GameObject minigames_btn_footer;
    public GameObject scanning_btn_footer;

    public GameObject taks_1_btn;
    public GameObject taks_2_btn;
    public GameObject taks_3_btn;

    public GameObject minigames_play_star_btn;
    public GameObject minigames_play_coins_btn;

    public GameObject scanning_btn;

    //scanners
    public GameObject trash_scanner;

    public static MainLocationOjects instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<MainLocationOjects>();
            return _instance;
        }
    }
}
