using Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;

[Extension(Extensions.SUBSCRIBE_MESSAGE)]
public class BusterController : ExtendedBehaviour {

    public GameObject booster_area_prefub;
    public GameObject parent_canvas;

    public static BusterController getController()
    {
        return GameObject.Find("Controllers").transform.Find("BusterController")
            .GetComponent<BusterController>();
    }

    public bool use_magnit = false;
    [Subscribe("START_MAGNIT")]
    public void StartMagnitBuster(Message msg)
    {
        use_magnit = true;
    }
    [Subscribe("END_MAGNIT")]
    public void EndMagnitBuster(Message msg)
    {
        use_magnit = false;
    }

    public float fly_amount = 0.0f;
    [Subscribe("START_FLY")]
    public void StartFlyBuster(Message msg)
    {
        foreach (var buster in active_busters)
        {
            if (buster is FlyBuster)
                fly_amount = (buster as FlyBuster).life_time;
        }

        MessageBus.Instance.SendMessage("START_FLY_PLATFORM");
        
    }

    List<Buster> active_busters;

    void InitActiveBusters()
    {
        active_busters = DataController.instance.buster_entity.active_busters;
    }

    void start_busters()
    {
        foreach (var buster in active_busters)
        {
            buster.Start();
            DataController.instance.buster_entity.UncountBuster(buster.b_type);
            var ba = Instantiate(booster_area_prefub, parent_canvas.transform, false);
            ba.GetComponent<BoosterArea>().Init(buster.b_type);
        }

        foreach (var buster in active_busters)
        {
            StartCoroutine(buster.Update());
        }
    }

    [Subscribe(MiniGameMessageType.MINIGAME_REBORN)]
    public void UseReborn(Message msg)
    {
        foreach (var buster in active_busters)
        {
            if (buster is RebornBuster)
                (buster as RebornBuster).UseBuster();
        }
    }

    [Subscribe(MiniGameMessageType.CLOSE_GOAL_SCREEN)]
    public void StartBusters(Message msg)
    {
        InitActiveBusters();

        Invoke("start_busters", 0.1f);
    }
	// Use this for initialization
	public override void ExtendedStart () {
        
    }

    // Update is called once per frame
    public override void ExtendedUpdate() {

	}
}
