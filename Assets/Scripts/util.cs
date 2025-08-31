using UnityEngine;
using System;

class Util
{
    public static float maybeConstrainByAbsValue(float value, float limit)
    {
        return Math.Max(-limit, Math.Min(limit, value));
    }

}