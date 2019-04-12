using System;
using Yaga;
using UnityEngine;

class UpdateInt : MessageParametrs
{
    public UpdateInt(int v)
    {
        value = v;
    }
    public int value;
}

class UpdateFloat : MessageParametrs
{
    public UpdateFloat(float v)
    {
        value = v;
    }
    public float value;
}
