using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;
using Yaga;

[Extension(Extensions.PAUSE)]
public class PlatformController : ExtendedBehaviour {

    public GameObject platform;
    public GameObject left_board;
    public GameObject right_board;
    Material driving_platform_material;
    Material left_board_material;
    Material right_board_material;

    Vector3 offset;
    Vector3 left_offset;
    Vector3 right_offset;

    float driving_platform_speed = 1.45f;
    float board_speed = 3.0f;

    // Use this for initialization
    public override void ExtendedStart () {
        driving_platform_material = platform.GetComponent<Renderer>().material;
        offset = driving_platform_material.GetTextureOffset("_MainTex");

        left_board_material = left_board.GetComponent<Renderer>().material;
        left_offset = left_board_material.GetTextureOffset("_MainTex");

        right_board_material = right_board.GetComponent<Renderer>().material;
        right_offset = right_board_material.GetTextureOffset("_MainTex");
    }

    // Update is called once per frame
    public override void ExtendedUpdate() {
        offset.x += Time.deltaTime * driving_platform_speed;
        driving_platform_material.SetTextureOffset("_MainTex", offset);

        left_offset.y += Time.deltaTime * board_speed;
        left_board_material.SetTextureOffset("_MainTex", left_offset);

        right_offset.y += Time.deltaTime * board_speed;
        right_board_material.SetTextureOffset("_MainTex", right_offset);
    }
}
