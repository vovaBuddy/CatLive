using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga;
using Yaga.MessageBus;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class BoosterArea : ExtendedBehaviour {

    public Text buster_name;
    public Text counter_text;
    public Image icon;
    public GameObject pb;
    int counter;
    float delta;
    BusterType type;

    delegate IEnumerator UpdateAction();

    [Subscribe(Minigames.MiniGameMessageType.GAME_OVER, Minigames.MiniGameMessageType.OPEN_WIN_PANEL)]
    public void Close(Message msg)
    {
        gameObject.GetComponent<Animator>().SetBool("close", true);
    }

    [Subscribe(Minigames.MiniGameMessageType.MINIGAME_REBORN)]
    public void Reborn(Message msg)
    {
        if (type != BusterType.REBORN)
            return;

        float x = pb.GetComponent<RectTransform>().sizeDelta.x;
        float y = pb.GetComponent<RectTransform>().sizeDelta.y;
        pb.GetComponent<RectTransform>().sizeDelta =
            new Vector2(x + delta, y);

        counter -= 1;

        if (counter == 0)
        {
            gameObject.GetComponent<Animator>().SetBool("close", true);
        }

        counter_text.text = counter.ToString();
    }
   
    IEnumerator MagnetUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            float x = pb.GetComponent<RectTransform>().sizeDelta.x;
            float y = pb.GetComponent<RectTransform>().sizeDelta.y;
            pb.GetComponent<RectTransform>().sizeDelta =
                new Vector2(x + delta, y);

            counter -= 1;

            if (counter == 0)
            {
                gameObject.GetComponent<Animator>().SetBool("close", true);
                break;
            }

            counter_text.text = Helper.TextHelper.TimeFormatMinutes(counter);
        }
    }

    IEnumerator FlyUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            float x = pb.GetComponent<RectTransform>().sizeDelta.x;
            float y = pb.GetComponent<RectTransform>().sizeDelta.y;
            pb.GetComponent<RectTransform>().sizeDelta =
                new Vector2(x + delta, y);

            counter -= 1;

            if (counter == 0)
            {
                gameObject.GetComponent<Animator>().SetBool("close", true);
                break;
            }

            counter_text.text = Helper.TextHelper.TimeFormatMinutes(counter);
        }
    }

    public void Init(BusterType in_type)
    {
        type = in_type;

        switch (in_type)
        {
            case BusterType.FLY:
                buster_name.text = TextManager.getText("booster_supercat_name");
                icon.sprite = Resources.Load<Sprite>("Icon_booster_speed_up_01");
                counter = FlyBuster.set_time_by_lvl(
                    DataController.instance.buster_entity.getLevel(BusterType.FLY));
                counter_text.text = Helper.TextHelper.TimeFormatMinutes(counter);
                delta = pb.GetComponent<RectTransform>().sizeDelta.x / counter;
                StartCoroutine(FlyUpdate());
                break;
            case BusterType.REBORN:
                buster_name.text = TextManager.getText("booster_reborn_name");
                icon.sprite = Resources.Load<Sprite>("Icon_booster_heart_01");
                counter = RebornBuster.set_cnt_by_lvl(
                    DataController.instance.buster_entity.getLevel(BusterType.REBORN));
                counter_text.text = counter.ToString();
                delta = pb.GetComponent<RectTransform>().sizeDelta.x / counter;
                break;
            case BusterType.MAGNIT:
                buster_name.text = TextManager.getText("booster_magnet_name");
                icon.sprite = Resources.Load<Sprite>("Icon_booster_magnet_01");
                counter = MagnitBuster.set_time_by_lvl(
                    DataController.instance.buster_entity.getLevel(BusterType.MAGNIT));
                counter_text.text = Helper.TextHelper.TimeFormatMinutes(counter);
                delta = pb.GetComponent<RectTransform>().sizeDelta.x / counter;
                StartCoroutine(MagnetUpdate());
                break;

        }

        float y = pb.GetComponent<RectTransform>().sizeDelta.y;
        pb.GetComponent<RectTransform>().sizeDelta =
            new Vector2(0, y);

    }
	
}
