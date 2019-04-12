using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderingOrder : MonoBehaviour {

    public GameObject cat;
    public GameObject PointContainer;

    Quaternion protation;
    void Awake()
    {
        protation = PointContainer.transform.rotation;
    }
    void LateUpdate()
    {
        PointContainer.transform.rotation = protation;
    }

    public Dictionary<string, GameObject> collided_objects;

    public GameObject x;
    public GameObject x_z;
    public GameObject _z;
    public GameObject _x_z;
    public GameObject _x;
    public GameObject _xz;
    public GameObject z;
    public GameObject xz;

    string[] orderable_types = { "OrderableUp", "OrderableDown", "Background" };

    Camera cam;

    public void ClearDictionart()
    {
        collided_objects["x"] = null;
        collided_objects["x-z"] = null;
        collided_objects["-z"] = null;
        collided_objects["-x-z"] = null;
        collided_objects["-x"] = null;
        collided_objects["-xz"] = null;
        collided_objects["z"] = null;
        collided_objects["xz"] = null;
    }

    // Use this for initialization
    void Start () {
        collided_objects = new Dictionary<string, GameObject>();
        collided_objects.Add("x", null);
        collided_objects.Add("x-z", null);
        collided_objects.Add("-z", null);
        collided_objects.Add("-x-z", null);
        collided_objects.Add("-x", null);
        collided_objects.Add("-xz", null);
        collided_objects.Add("z", null);
        collided_objects.Add("xz", null);

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void GetNearestHelper(GameObject obj, string str)
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(obj.transform.position)), Vector2.zero);

        foreach (var h in hit)
        {
            if (h.collider != null)
            {
                if (h.collider.gameObject.tag == orderable_types[0] ||
                    h.collider.gameObject.tag == orderable_types[1] ||
                    h.collider.gameObject.tag == orderable_types[2])
                {
                    collided_objects[str] = h.collider.gameObject;
                }
            }
        }
    }

    void GetNearest()
    {
        GetNearestHelper(x, "x");
        GetNearestHelper(x_z, "x-z");
        GetNearestHelper(_z, "-z");
        GetNearestHelper(_x_z, "-x-z");
        GetNearestHelper(_x, "-x");
        GetNearestHelper(_xz, "-xz");
        GetNearestHelper(z, "z");
        GetNearestHelper(xz, "xz");
    }

    public void set_order_value(int value)
    {
        cat.GetComponent<SkinnedMeshRenderer>().sortingOrder = value;

        var shadow = cat.transform.parent.Find("New Sprite").GetComponent<SpriteRenderer>().sortingOrder = value;

        var chest_bone = cat.transform.parent.Find("Armature").Find("main_Bone").Find("chest_Bone");
        var collar = chest_bone.Find("collar_001");

        if(collar.gameObject.activeSelf)
        {
            collar.GetComponent<MeshRenderer>().sortingOrder = value;
        }

        var head_bone = chest_bone.Find("head_bone");

        var bat = head_bone.Find("bat");
        if (bat.gameObject.activeSelf)
        {
            bat.GetComponent<MeshRenderer>().sortingOrder = value;
        }

        var cap = head_bone.Find("cap_001");
        if (cap.gameObject.activeSelf)
        {
            cap.GetComponent<MeshRenderer>().sortingOrder = value;
        }

        var eye1 = head_bone.Find("eye_1");
        eye1.GetComponent<MeshRenderer>().sortingOrder = value;
        var eye2 = head_bone.Find("eye_2");
        eye2.GetComponent<MeshRenderer>().sortingOrder = value;

        var glasses = head_bone.Find("glasses_001");
        if (glasses.gameObject.activeSelf)
        {
            glasses.GetComponent<MeshRenderer>().sortingOrder = value;
        }
    }

    public void set_less_order(string str)
    {
        if (cat.GetComponent<SkinnedMeshRenderer>().sortingOrder > collided_objects[str].GetComponent<SpriteRenderer>().sortingOrder - 1)
        {
            set_order_value(collided_objects[str].GetComponent<SpriteRenderer>().sortingOrder - 1);
        }
    }

    public void set_more_order(string str)
    {
        set_order_value(collided_objects[str].GetComponent<SpriteRenderer>().sortingOrder + 1);
    }

    bool check_foreground()
    {
        foreach(var c in collided_objects)
        {
            if (c.Value == null) continue;

            if(c.Value.tag == "Background")
            {
                set_more_order(c.Key);

                return true;
            }
        }

        return false;
    }

    public void CheckNearest()
    {
        if (check_foreground()) return;

        if(collided_objects["z"] != null)
        {
            set_more_order("z");
        }

        if (collided_objects["xz"] != null)
        {
            set_more_order("xz");
        }

        if (collided_objects["-xz"] != null)
        {
            set_more_order("-xz");
        }

        if (collided_objects["x"] != null)
        {
            set_more_order("x");
        }

        //if (collided_objects["-x"] != null)
        //{
        //    set_less_order("-x");
        //}

        if (collided_objects["-x-z"] != null)
        {
            set_less_order("-x-z");
        }

        if (collided_objects["x"] != null && collided_objects["x-z"] != null && 
        collided_objects["x"] == collided_objects["x-z"])
        {
            set_less_order("x");
        }

        if (collided_objects["x"] != null && collided_objects["x-z"] != null && collided_objects["xz"] &&
        collided_objects["x"] == collided_objects["x-z"] && collided_objects["x"] == collided_objects["xz"])
        {
            set_more_order("x-z");
        }

        if (collided_objects["x-z"] != null && collided_objects["-z"] != null &&
            collided_objects["-z"] == collided_objects["x-z"])
        {
            set_less_order("x-z");
        }

        if (collided_objects["-z"] != null)
        {
            set_less_order("-z");
        }
    }

    // Update is called once per frame
    void Update () {
        ClearDictionart();

        GetNearest();

        CheckNearest();
    }
}
