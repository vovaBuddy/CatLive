using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotateble : IRuneEffect
{
    private float rotSpeed = 120;

    public void Update (GameObject obj) {
        obj.transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.World);
    }
}
