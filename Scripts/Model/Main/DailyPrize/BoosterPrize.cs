using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterPrize : DailyPrize
{
    public int count;
    BusterType type;

    public BoosterPrize(BusterType t, int cnt)
    {
        count = cnt;
        type = t;
    }

    public void ActiveAction()
    {
        DataController.instance.buster_entity.BougthBuster(type, count);
    }

    public string GetName()
    {
        string name;
        switch (type)
        {
            case BusterType.FLY:
                name = "booster_supercat_name";
                break;
            case BusterType.MAGNIT:
                name = "booster_magnet_name";
                break;
            case BusterType.REBORN:
                name = "booster_reborn_name";
                break;
            default:
                name = "booster_reborn_name";
                break;
        }

        return TextManager.getText(name);
    }

    public string GetIconName()
    {
        string sprite_name;
        switch (type)
        {
            case BusterType.FLY:
                sprite_name = "Icon_booster_speed_up_01";
                break;
            case BusterType.MAGNIT:
                sprite_name = "Icon_booster_magnet_01";
                break;
            case BusterType.REBORN:
                sprite_name = "Icon_booster_heart_01";
                break;
            default:
                sprite_name = "Icon_booster_speed_up_01";
                break;
        }

        return sprite_name;
    }

    public void CreatePrefub(GameObject instaniatedObj)
    {
        instaniatedObj.GetComponent<DailyPrizeUI>().value.text = "x" + count.ToString();

        var sprite_name = GetIconName();

        instaniatedObj.GetComponent<DailyPrizeUI>().img.sprite = Resources.Load<Sprite>(sprite_name);
    }
}
