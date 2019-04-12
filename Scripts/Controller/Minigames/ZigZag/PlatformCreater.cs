using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;

namespace ZigZag
{
    public class CreationStrategy
    {
        public int id;
        public float cube_resp_time;
        public float percent_edge;
        public string prefab_name;
        float _offset;
        float init_offset;
        bool use_init_offset;

        public float offset
        {
            get {
                if (use_init_offset)
                {
                    use_init_offset = false;
                    return init_offset;                    
                }
                else
                {
                    return _offset;
                }
            }
        }



        public void SetInitOffset(float i_o)
        {
            use_init_offset = true;
            init_offset = i_o;
        }

        public CreationStrategy(int i, float resp, float pcnt, string name, float ofst)
        {
            id = i;
            cube_resp_time = resp;
            percent_edge = pcnt;
            prefab_name = name;
            _offset = ofst;
        }
    }

    public class PlatformCreater : MonoBehaviour, Minigames.PlatformCreater, IFlyBusterable, IRebornBusterable
    {
        public static CreationStrategy item3x3 = new CreationStrategy(3, 0.9f, 0.6f, "ZigZag/platform_item_3", 3);
        public static CreationStrategy item2x2 = new CreationStrategy(2, 0.6f, 0.4f, "ZigZag/platform_item_2", 2);
        public static CreationStrategy item1x1 = new CreationStrategy(1, 0.3f, 0.2f, "ZigZag/platform_item", 1);

        public void SetStrategy(CreationStrategy str)
        {
            cur_strategy = str;
        }

        private enum Direction
        {
            LEFT = 0,
            UP = 1,
            //RIGHT = 2,
        }

        private Direction cur_dir;

        CreationStrategy cur_strategy;

        GameObject init_cude;
        Vector3 cur_cube_coord;

        float resp_timer;
        float CUBE_RESP_TIME_FACTOR;

        float PLATFORM_SPEED;
        Transform platform;

        string platform_prefab_path = "ZigZag/ZigZagPlatform";
        string platform_item_prefab_path = "ZigZag/platform_item";

        private Material driving_platform_material1;
        private Vector3 offset1;
        private Material driving_platform_material2;
        private Vector3 offset2;
        private bool going;

        private GameObject trees_left;
        private GameObject trees_right;

        private float driving_platform_speed = 0.27f;
        private float trees_factor = 4.0f;

        public void UpDifficult()
        {
            if(cur_strategy == item3x3)
            {
                cur_strategy = item2x2;
            }
            else if(cur_strategy == item2x2)
            {
                cur_strategy = item1x1;
                cur_strategy.SetInitOffset(cur_strategy.offset + 1);
            }
        }

        public void LoadStartPlatform()
        {
            var go = GameObject.Find("env_new_zigzag").transform.Find("lc_mngm_001");
            driving_platform_material1 = go.Find("waterfront_001 (1)").GetComponent<Renderer>().material;
            offset1 = driving_platform_material1.GetTextureOffset("_MainTex");

            driving_platform_material2 = go.Find("waterfront_001").GetComponent<Renderer>().material;
            offset2 = driving_platform_material2.GetTextureOffset("_MainTex");

            trees_left = go.Find("left_set").gameObject;
            trees_right = go.Find("right_set").gameObject;

            PLATFORM_SPEED = 1.5f;

            cur_strategy = item3x3;
            cur_strategy.SetInitOffset(cur_strategy.offset - 1);

            CUBE_RESP_TIME_FACTOR = 1;

            //cur_strategy = item2x2;
            //cur_strategy.SetInitOffset(cur_strategy.offset - 1);

            platform = ((GameObject)Instantiate(Resources.Load(platform_prefab_path))).transform;
            init_cude = platform.Find("level_particle_init").gameObject;

            Message msg = new Message();
            msg.Type = Minigames.MiniGameMessageType.INIT_PLATFORM;
            msg.parametrs = new Minigames.UpdateInitPos(platform.transform);
            MessageBus.Instance.SendMessage(msg);
        }

        public void MovePlatform()
        {
            float delta = Time.deltaTime;

            platform.position = new Vector3(platform.position.x + PLATFORM_SPEED * delta,
                platform.position.y, platform.position.z - PLATFORM_SPEED * delta);

            offset1.y -= Time.deltaTime * driving_platform_speed;
            driving_platform_material1.SetTextureOffset("_MainTex", offset1);

            offset2.y += Time.deltaTime * driving_platform_speed;
            driving_platform_material2.SetTextureOffset("_MainTex", offset2);

            if (trees_left.transform.position.x > 12.0f)
            {
                var rand = (-1 * UnityEngine.Random.Range(5200, 8500));
                trees_left.transform.position = new Vector3(
                    trees_left.transform.position.x + delta * driving_platform_speed * rand,
                    trees_left.transform.position.y,
                    trees_left.transform.position.z - delta * driving_platform_speed * rand);
            }
            else
            {
                trees_left.transform.position = new Vector3(
                    trees_left.transform.position.x + delta * driving_platform_speed * trees_factor,
                    trees_left.transform.position.y,
                    trees_left.transform.position.z - delta * driving_platform_speed * trees_factor);

            }

            if (trees_left.transform.position.x > 12.0f)
            {
                var rand = (-1 * UnityEngine.Random.Range(5200, 8500));
                trees_right.transform.position = new Vector3(
                    trees_right.transform.position.x + delta * driving_platform_speed * rand,
                    trees_right.transform.position.y,
                    trees_right.transform.position.z - delta * driving_platform_speed * rand);
            }
            else
            {
                trees_right.transform.position = new Vector3(
                    trees_right.transform.position.x + delta * driving_platform_speed * trees_factor,
                    trees_right.transform.position.y,
                    trees_right.transform.position.z - delta * driving_platform_speed * trees_factor);
            }
        }

        GameObject CreateLineSafe(GameObject init_obj, bool last)
        {
            var cur_coord = init_obj.transform.localPosition;
            Vector3 screen_pos = Camera.main.WorldToScreenPoint(init_obj.transform.position);
            GameObject last_go = init_obj;

            while (screen_pos.x >= 0.2f * Screen.width)
            {
                init_obj.transform.localPosition = new Vector3(init_obj.transform.localPosition.x - 1,
                    init_obj.transform.localPosition.y, init_obj.transform.localPosition.z);

                screen_pos = Camera.main.WorldToScreenPoint(init_obj.transform.position);

                GameObject go = (GameObject)Instantiate(Resources.Load(platform_item_prefab_path));
                go.transform.parent = platform;
                go.transform.position = init_obj.transform.position;
                last_go = go;
            }


            init_obj.transform.localPosition = cur_coord;
            screen_pos = Camera.main.WorldToScreenPoint(init_obj.transform.position);            

            while (Screen.width - screen_pos.x >= 0.2f * Screen.width)
            {
                init_obj.transform.localPosition = new Vector3(init_obj.transform.localPosition.x + 1,
                    init_obj.transform.localPosition.y, init_obj.transform.localPosition.z);

                screen_pos = Camera.main.WorldToScreenPoint(init_obj.transform.position);

                GameObject go = (GameObject)Instantiate(Resources.Load(platform_item_prefab_path));
                go.transform.parent = platform;
                go.transform.position = init_obj.transform.position;
            }

            init_obj.transform.localPosition = cur_coord;

            return last_go;
        }

        public void CreateSafePlatform()
        {
            cur_cube_coord = init_cude.transform.position;

            for ( int i = 0; i < platform.childCount; ++i )
            {
                Destroy(platform.GetChild(i).gameObject);
            }
            
            GameObject go1 = (GameObject)Instantiate(Resources.Load(platform_item_prefab_path));
            go1.transform.parent = platform;
            go1.transform.position = new Vector3(cur_cube_coord.x,
                        cur_cube_coord.y,
                        cur_cube_coord.z + 1);

            init_cude = go1;

            for (int i = 0; i < 21; ++i)
            {
                init_cude = CreateLineSafe(init_cude, true);

                cur_cube_coord = init_cude.transform.position;
                GameObject go = (GameObject)Instantiate(Resources.Load(platform_item_prefab_path));
                go.transform.parent = platform;
                go.transform.position = new Vector3(cur_cube_coord.x + i,
                            cur_cube_coord.y,
                            cur_cube_coord.z - 1);

                init_cude = go;
            }

            init_cude = go1;

            go1 = (GameObject)Instantiate(Resources.Load(platform_item_prefab_path));
            go1.transform.parent = platform;
            go1.transform.position = new Vector3(init_cude.transform.position.x,
                        init_cude.transform.position.y,
                        init_cude.transform.position.z + 1);

            init_cude = go1;

            Message msg = new Message();
            msg.Type = Minigames.MiniGameMessageType.UPDATE_INIT_POS;
            msg.parametrs = new Minigames.UpdateInitPos(init_cude.transform);
            MessageBus.Instance.SendMessage(msg);

            if(cur_strategy.id != 1)
                cur_strategy.SetInitOffset(cur_strategy.offset - 1);
            else
                cur_strategy.SetInitOffset(cur_strategy.offset);
        }

        float safe_timer = float.MaxValue;
        public void UpdatePlatform()
        {
            float delta = Time.deltaTime;

            resp_timer -= delta;
            safe_timer -= delta;

            if (safe_timer < 0)
            {
                CreateSafePlatform();
                safe_timer = float.MaxValue;
            }

            else if (resp_timer < 0)
            {
                resp_timer = cur_strategy.cube_resp_time / CUBE_RESP_TIME_FACTOR;

                GameObject go = (GameObject)Instantiate(Resources.Load(cur_strategy.prefab_name));
                Vector3 new_pos;
                cur_cube_coord = init_cude.transform.position;

                Direction dir;
                dir = (Direction)UnityEngine.Random.Range(0, 2);
                Direction tmp_dir = dir;

                Vector3 screen_pos = Camera.main.WorldToScreenPoint(init_cude.transform.position);
                if (screen_pos.x < cur_strategy.percent_edge * Screen.width && cur_dir == Direction.LEFT)
                {
                    tmp_dir = Direction.UP;
                }
                if (Screen.width - screen_pos.x < cur_strategy.percent_edge * Screen.width && cur_dir == Direction.UP)
                {
                    tmp_dir = Direction.LEFT;
                }
                dir = tmp_dir;

                cur_dir = dir;

                if (dir == Direction.UP)
                {
                    new_pos = new Vector3(cur_cube_coord.x,
                        cur_cube_coord.y,
                        cur_cube_coord.z + cur_strategy.offset);
                }
                else if (dir == Direction.LEFT)
                {
                    new_pos = new Vector3(cur_cube_coord.x - cur_strategy.offset,
                        cur_cube_coord.y,
                        cur_cube_coord.z);
                }
                else
                {
                    new_pos = new Vector3(cur_cube_coord.x + cur_strategy.offset,
                        cur_cube_coord.y,
                        cur_cube_coord.z);
                }
                                
                go.transform.position = new_pos;
                go.transform.parent = platform;
                init_cude = go;

                Message msg = new Message();
                msg.Type = Minigames.MiniGameMessageType.UPDATE_INIT_POS;
                msg.parametrs = new Minigames.UpdateInitPos(init_cude.transform);
                MessageBus.Instance.SendMessage(msg);

            }
        }

        public void onBtnPressed(Message msg)
        {

        }

        public void StartFly()
        {
            CUBE_RESP_TIME_FACTOR = 3.1f;
            PLATFORM_SPEED = 1.5f * 3;

            driving_platform_speed *= 3;

            safe_timer = BusterController.getController().fly_amount;
        }

        public void EndFly()
        {
            CUBE_RESP_TIME_FACTOR = 1;
            PLATFORM_SPEED = 1.5f;

            driving_platform_speed /= 3;
        }

        public void StartReborn()
        {
        }

        public void EndReborn()
        {
        }

        public void Reborn()
        {
            safe_timer = 0;
        }
    }
}
