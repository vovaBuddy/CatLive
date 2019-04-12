using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    GameObject finish;
    GameObject start;

    List<string> final_trace;
    Dictionary<string, int> lenght;

    void SetLenghts(GameObject point, int value, List<string> checked_points)
    {
        value += 1;
        var neis = ((PathPoint)point.GetComponent<PathPoint>()).neighbors;
        for (int i = 0; i < neis.Count; ++i)
        {
            if (checked_points.Contains(neis[i].name))
                continue;

            if (!lenght.ContainsKey(neis[i].name))
            {
                lenght.Add(neis[i].name, value);

            }
            else  if(value < lenght[neis[i].name])
            {
                lenght[neis[i].name] = value;
            }
            else
            {
                return;
            }

            checked_points.Add(neis[i].name);
            SetLenghts(neis[i], value, new List<string>(checked_points));
        }

    }

    bool finded = false;

    void check_point(GameObject cur_point, List<string> trace, List<string> checked_points)
    {
        if (finded)
            return;

        trace.Add(cur_point.name);
        checked_points.Add(cur_point.name);

        

        if (string.Equals(cur_point.name, finish.name))
        {
            finded = true;
            final_trace = trace;

            //if (final_trace == null)
            //{
            //    final_trace = trace;
            //}
            //else if (final_trace.Count > trace.Count)
            //{
            //    final_trace = trace;
            //}
            return;
        }

        else
        {
            foreach (var n in ((PathPoint)cur_point.GetComponent<PathPoint>()).neighbors)
            {
                if (!checked_points.Contains(n.name))
                {
                    check_point(n, new List<string>(trace), new List<string>(checked_points));
                }
            }
        }
    }



    public List<string> GetPath(GameObject st, GameObject fnh)
    {
        finish = fnh;
        check_point(st, new List<string>(), new List<string>());

        return final_trace;
    }

    public List<Vector3> GetPathVectors(GameObject st, GameObject fnh)
    {
        List<Vector3> result = new List<Vector3>();
        final_trace = new List<string>();

        lenght = new Dictionary<string, int>();
        lenght.Add(fnh.name, 0);

        var checked_points = new List<string>();
        checked_points.Add(fnh.name);
        SetLenghts(fnh, 0, checked_points);

        

        while (st.name != fnh.name)
        {
            int cur_lenght = int.MaxValue;
            var neis = st.GetComponent<PathPoint>().neighbors;
            for (int i = 0; i < neis.Count; ++i)
            {
                if(lenght[neis[i].name] < cur_lenght)
                {
                    cur_lenght = lenght[neis[i].name];
                    st = neis[i];
                }
            }

            //final_trace.Add(st.name);
            result.Add(st.transform.position);
        }
        //Transform parent = st.transform.parent;

        //foreach(string name in GetPath(st, fnh))
        //{
        //    result.Add(parent.Find(name).position);
        //}

        return result;
    }
}
