using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionArea : MonoBehaviour {

    public Text text_mission;
    public Image img;

    public GameObject target_pos;

    public void Init(Sprite spr, GameObject target)
    {
        text_mission.text = TextManager.getText("dialog_mission_area_title_text");
        img.sprite = spr;
        target_pos = target;
    }

    IEnumerator move()
    {
        while(Vector3.Distance(transform.position, target_pos.transform.position) > 80)
        {
            if(Vector3.Distance(transform.position, target_pos.transform.position) < 1000)
                GetComponent<Animator>().SetBool("close", true);

            float step = 900 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target_pos.transform.position, step);
            yield return new WaitForSeconds(0.01f);
        }

        
    }

    public void MoveToPanel()
    {
        StartCoroutine(move());
    }
}
