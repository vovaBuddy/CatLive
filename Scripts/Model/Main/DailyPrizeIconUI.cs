using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyPrizeIconUI : MonoBehaviour {

    public enum DPIState
    {
        DONE, 
        TODAY,
        FUTURE
    }


    public GameObject check_icon;
    public GameObject prize_icon;
    public Text day_text;
    public Animator anim;
    public Button btn;

    public void Init(DPIState state, int day)
    {
        day_text.text = TextManager.getText("mm_daily_prize_day") + " " + day.ToString();

        switch (state)
        {
            case DPIState.DONE:
                prize_icon.SetActive(false);
                check_icon.SetActive(true);
                break;

            case DPIState.TODAY:
                prize_icon.SetActive(true);
                check_icon.SetActive(false);
                anim.SetBool("idle", true);
                anim.SetBool("no_check", true);
                btn.onClick.AddListener(() => { Done(); });
                break;

            case DPIState.FUTURE:
                prize_icon.SetActive(true);
                check_icon.SetActive(false);
                anim.SetBool("no_check", true);
                break;
        }

    }

    public void Done()
    {
        anim.SetBool("done", true);
    }
}
