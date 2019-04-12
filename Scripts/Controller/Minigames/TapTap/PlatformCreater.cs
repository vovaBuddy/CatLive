using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;

namespace TapTap
{
    public class PlatformCreater : MonoBehaviour, Minigames.PlatformCreater, IFlyBusterable
    {
        GameObject platform;

        public GameObject driving_paltform;
        public GameObject PatternRunes;
        public GameObject PatternRunesPoint;

        public GameObject boxes_pool;

        private Material driving_platform_material;
        private bool going;
        private Vector3 offset;
        private float driving_platform_speed = 0.27f;

        public GameObject RotatePointObj;
        private Vector3 RotatePointPnt;

        private bool need_rotate;
        private float rotation_angle;
        private float local_rot_angle;

        private float cur_rotation_angle;

        string platform_prefab_path = "TapTap/TapTapPlatform";

        public void LoadStartPlatform()
        {
            platform = ((GameObject)Instantiate(Resources.Load(platform_prefab_path)));
            driving_paltform = platform.transform.Find("level_002").gameObject;
            RotatePointObj = platform.transform.Find("RotatePoint").gameObject;

            PatternRunes = platform.transform.Find("PatternRunes").gameObject;
            PatternRunesPoint = platform.transform.Find("PatternRunesPoint").gameObject;

            //PatternRunesPoint.transform.Find("score_1").transform.localRotation = new
            //    Quaternion(0, 0, 90, 1);
            //PatternRunesPoint.transform.Find("score_2").transform.localRotation = new
            //    Quaternion(0, 0, 90, 1);

            driving_platform_material = driving_paltform.transform.Find("all_lines").GetComponent<Renderer>().material;
            offset = driving_platform_material.GetTextureOffset("_MainTex");
            RotatePointPnt = RotatePointObj.transform.position;

            rotation_angle = 1.0f;
            local_rot_angle = 1.0f;
            need_rotate = false;
            cur_rotation_angle = 0.0f;

            //init_cude = platform.Find("level_particle_init").gameObject;

            Message msg = new Message();
            msg.Type = Minigames.MiniGameMessageType.INIT_PLATFORM;
            msg.parametrs = new Minigames.InitUpdate(
                platform.transform.Find("BoxesPool"),
                PatternRunes.transform, PatternRunesPoint.transform);
            MessageBus.Instance.SendMessage(msg);
        }

        public void ReflexAngle()
        {
            rotation_angle *= -1.0f;

            if (!need_rotate)
            {
                local_rot_angle = rotation_angle;
            }
        }

        public void Rotate()
        {
            if (DataController.instance.gamesRecords.tapTapTutorDone == true)
                need_rotate = true;
        }

        public void StartFly()
        {
            GameSpeed.speed_factor = 8;
        }

        public void EndFly()
        {
            GameSpeed.speed_factor = 2;
        }

        public void MovePlatform()
        {
            offset.x -= Time.deltaTime * driving_platform_speed * GameSpeed.speed_factor;
            driving_platform_material.SetTextureOffset("_MainTex", offset);

            //transform.RotateAround(RotatePointPnt, Vector3.forward, 200 * Time.deltaTime);
        }

        float last_rotate_time = 0.0f;
        public void UpdatePlatform()
        {
            last_rotate_time -= Time.deltaTime;

            if (need_rotate && last_rotate_time <= 0.0f)
            {
                

                float delta = 500 * Time.deltaTime;

                if (cur_rotation_angle + delta > 90.0f)
                {
                    delta = 90.0f - cur_rotation_angle;
                }

                cur_rotation_angle += delta;

                platform.transform.RotateAround(RotatePointPnt, Vector3.forward, local_rot_angle * delta);

                if (cur_rotation_angle >= 90.0f)
                {
                    cur_rotation_angle = 0.0f;
                    need_rotate = false;
                    local_rot_angle = rotation_angle;
                    last_rotate_time = 0.5f;
                }
            }
        }

        public void onBtnPressed(Message msg)
        {
            Rotate();
        }
    }
}
