using UnityEngine;

public class FloatRange
{
    private float _min;
    private float _max;

    public FloatRange(float min, float max)
    {
        _min = min;
        _max = max;
    }

    public float getFloat() { return Random.Range(_min, _max); }
}
