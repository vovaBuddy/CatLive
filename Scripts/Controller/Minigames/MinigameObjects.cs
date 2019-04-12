using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameObjects : MonoBehaviour {

    static MinigameObjects _instance;

    public static MinigameObjects instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<MinigameObjects>();
            return _instance;
        }
    }

    public GameObject start_star_game_btn;
    public GameObject start_star_magnet_btn;
    public GameObject start_star_fly_btn;
    public GameObject start_star_reborn_btn;
}
