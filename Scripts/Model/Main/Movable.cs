using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Old
public class OldMovable : MonoBehaviour {

    public Transform _move;
    Vector3 speed = new Vector3(0.99f, 0.99f, 1.0f);

    public Transform target;
    Vector3 forward_vector;

    bool started_call;

    public List<Vector3> targets;
    Vector3 cur_target;
    int index;

    // Use this for initialization
    void Start () {
        //cat = transform.parent;
        forward_vector = _move.forward;
        started_call = false;
        index = 0;
        cur_target = Vector3.zero;
    }

    public static OldMovable GetController()
    {
        return GameObject.Find("Controllers").transform.Find("CatMoveController")
            .GetComponent<OldMovable>();
    }

    public void SetDestinations(List<Vector3> points)
    {
        targets = new List<Vector3>();
        targets = points;
        index = 0;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("col");
    }

    // Update is called once per frame
    void Update () {
        if (!started_call)
        {
            started_call = true;
            return;
        }

        if (targets == null || targets.Count == 0)
            return;

        if (cur_target == Vector3.zero)
        {
            cur_target = targets[index];
        }

        if (Mathf.Abs(_move.position.x - cur_target.x) <= 1 &&
            Mathf.Abs(_move.position.y - cur_target.y) <= 1)
        {
            index++;

            if (targets.Count > index)
            {
                cur_target = targets[index];
            }
            else
            {
                targets = null;
                index = 0;
                cur_target = Vector3.zero;
                return;
            }
        }

        var _direction = (cur_target - _move.position).normalized;
        float sign = (cur_target.y < _move.position.y) ? -1.0f : 1.0f;
        var angle = Vector3.Angle(forward_vector, _direction) * sign;
        var angle_lerp = Mathf.Lerp(_move.localEulerAngles.y, angle, 0.05f);
        _move.localEulerAngles = new Vector3(0, angle, 0);

        var new_pos = _move.position + (_direction * speed.x * Time.deltaTime);
        //new_pos.z = _move.position.z;
        _move.position = new_pos;
    }
}
