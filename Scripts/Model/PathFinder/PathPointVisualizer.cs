using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PathPointVisualizer : MonoBehaviour {

    public int id;
    public string neighbors;

    void OnValidate()
    {
        gameObject.name = "Point " + id.ToString();
        gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = id.ToString();

        var ns = neighbors.Split(',');

        if (ns.Length > 0 && !string.Equals(ns[0], ""))
        {
            gameObject.GetComponent<PathPoint>().neighbors.Clear();

            foreach (string n in ns)
            {
                gameObject.GetComponent<PathPoint>().neighbors.Add(gameObject.transform.parent.Find("Point " + n).gameObject);
            }
        }

    }
	
}
