using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Initializer : MonoBehaviour {

    public GameObject pb_line;
    public GameObject pb;
    public GameObject btn;
    float max_pb_with = 840;

    public Text play_btn_text;
    public Text loading_text;

    bool init_phase_one = false;
    bool init_phase_two = false;

    public Image logo;

    bool inited = false;
    void init()
    {

        if (inited)
            return;        

        float tmp = pb_line.GetComponent<RectTransform>().sizeDelta.y;

        if (!init_phase_one)
        {
            OneSignalController.Create();
            GameStatistics.Create();

            TextManager.Init(Application.systemLanguage);

            if(Application.systemLanguage == SystemLanguage.Russian)
            {
                logo.sprite = Resources.Load<Sprite>("logo/Russian");
            }
            else
            {
                logo.sprite = Resources.Load<Sprite>("logo/English");
            }

            play_btn_text.text = TextManager.getText("initializer_btn_play");
            loading_text.text = TextManager.getText("initializer_loading");

            Helper.DeviceNameHelper.Init();
            DataController.Create();
            pb_line.GetComponent<RectTransform>().sizeDelta = new Vector2(25 * max_pb_with / 100.0f, tmp);
            init_phase_one = true;
            //AppodealController.Create();
            return;
        }

        if(!init_phase_two)
        {
            pb_line.GetComponent<RectTransform>().sizeDelta = new Vector2(70 * max_pb_with / 100.0f, tmp);
            StarTasksController.Create();
            init_phase_two = true;
            return;
        }

        pb.SetActive(false);
        btn.SetActive(true);

        inited = true;
    }

    // Use this for initialization
    bool load = false;
    void Update () {
        init();

        if(load)
            //SceneManager.LoadScene("scanning");
            SceneManager.LoadScene("main");
    }

    public void Play()
    {
        pb.SetActive(true);
        btn.SetActive(false);
        load = true;

        GameStatistics.instance.SendStat("startscreen_btn_play_presed", 0);
    }

    
}
