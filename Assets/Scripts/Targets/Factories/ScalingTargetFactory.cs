using UnityEngine;

public class ScalingTargetFactory : BaseTargetFactory
{
    private static string baseName = "Scaling Target #";
    private static int numCreated = 0;
    private static int autoNamed = 0;
    private static string _overrideName = null;

    private FloatRange _minScale;
    private FloatRange _maxScale;
    private FloatRange _timePerCycle;

    public ScalingTargetFactory(float minMinScale, float maxMinScale, float minMaxScale, float maxMaxScale, float minTimePerCycle, float maxTimePerCycle)   //minMax maxMin, these really should get better names.
    {
        _minScale = new FloatRange(minMinScale, maxMinScale);
        _maxScale = new FloatRange(minMaxScale, maxMaxScale);
        _timePerCycle = new FloatRange(minTimePerCycle, maxTimePerCycle);
    }

    //Overrides
    public override BaseTarget makeTarget()
    {
        string name = null;
        if (_overrideName == null)
        {
            name = baseName + autoNamed;
            autoNamed++;
        }
        else
        {
            name = _overrideName;
            _overrideName = null;
        }

        GameObject obj = GameObject.Instantiate(Resources.Load("Prefabs/ScalingTargetPrefab")) as GameObject;   //Because it's a fast way to do it
        obj.name = name;
        ScalingTarget target = obj.GetComponent<ScalingTarget>();
        target.init(_minScale.getFloat(), _maxScale.getFloat(), _timePerCycle.getFloat());

        numCreated++;
        return target;
    }

    //Setters
    public void setOverrideName(string value) { _overrideName = value; }
}
