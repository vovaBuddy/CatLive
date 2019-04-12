using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;

public enum BusterType
{
    MAGNIT = 1, 
    FLY = 2, 
    REBORN = 3
}

public interface IFlyBusterable
{
    void StartFly();
    void EndFly();
}

public interface IRebornBusterable
{
    void StartReborn();
    void EndReborn();
    void Reborn();
}

public class RebornBuster : UnitBuster
{
    public static int set_cnt_by_lvl(int lvl)
    {
        switch (lvl)
        {
            case 1:
                return 1;
            case 2:
                return 2;
            case 3:
                return 3;
            case 4:
                return 4;
            default:
                return 0;
        }

    }
    public RebornBuster(int lvl) :
        base(BusterType.REBORN, set_cnt_by_lvl(lvl))
    {

    }

    public void UseBuster()
    {
        count -= 1;
    }
}

public class FlyBuster : TimableBuster
{
    public static int set_time_by_lvl(int lvl)
    {
        switch (lvl)
        {
            case 1:
                return 10;
            case 2:
                return 15;
            case 3:
                return 20;
            case 4:
                return 25;
            default:
                return 0;
        }

    }
    public FlyBuster(int lvl) : 
        base(BusterType.FLY, set_time_by_lvl(lvl))
    {

    }

    public override void Start()
    {
        life_time = life_time_cash;
        loop = true;
        Message msg = new Message();
        msg.Type = "START_FLY";
        msg.parametrs = new UpdateInt(25);
        MessageBus.Instance.SendMessage(msg);
    }
}

public class MagnitBuster : TimableBuster
{
    public int range;

    public static int set_time_by_lvl(int lvl)
    {
        switch (lvl)
        {
            case 1:
                return 15;
            case 2:
                return 20;
            case 3:
                return 30;
            case 4:
                return 40;
            default:
                return 0;
        }

    }
    public MagnitBuster(int lvl) : 
        base(BusterType.MAGNIT, set_time_by_lvl(lvl))
    {

    }
}


public class UnitBuster : Buster
{
    public int count;
    int count_cash;
    bool loop;

    public UnitBuster(BusterType type, int cnt) : base(type)
    {
        count = cnt;
        count_cash = cnt;
    }

    public override void Start()
    {
        count = count_cash;
        loop = true;
        base.Start();
    }

    public override IEnumerator Update()
    {
        while (loop)
        {
            yield return new WaitForSeconds(0.0f);

            if (count <= 0)
            {
                MessageBus.Instance.SendMessage(end_msg);
                loop = false;
            }
        }
    }
}


public class TimableBuster : Buster
{
    public int life_time;
    protected int life_time_cash;
    protected bool loop;

    public TimableBuster(BusterType type, int time) : base(type)
    {
        life_time = time;
        life_time_cash = time;
    }

    public override void Start()
    {
        life_time = life_time_cash;
        loop = true;
        base.Start();
    }

    public override IEnumerator Update()
    {
        while (loop)
        {
            yield return new WaitForSeconds(1.0f);

            life_time -= 1;

            if (life_time <= 0)
            {
                MessageBus.Instance.SendMessage(end_msg);
                loop = false;
            }
        }
    }     
}

public class Buster {

    public static string GetMessage(BusterType type, bool start)
    {
        if(start)
        {
            return "START_" + type.ToString();
        }
        else
        {
            return "END_" + type.ToString();
        }
    }

    public string start_msg;
    public string end_msg;
    public BusterType b_type;

    public Buster(BusterType type)
    {
        start_msg = "START_" + type.ToString();
        end_msg = "END_" + type.ToString();
        b_type = type;
    }

    public virtual IEnumerator Update() { yield return new WaitForSeconds(0); }

    public virtual void Start()
    {
        MessageBus.Instance.SendMessage(start_msg);
    }
}
