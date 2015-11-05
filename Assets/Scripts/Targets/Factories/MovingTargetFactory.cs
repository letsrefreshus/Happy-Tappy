using UnityEngine;

public class MovingTargetFactory : BaseTargetFactory
{
    private static string baseName = "Moving Target #";
    private static int numCreated = 0;
    private static int autoNamed = 0;
    private static string _overrideName = null;

    private FloatRange _direction;
    private FloatRange _distance;
    private FloatRange _timePerCycle;

    public MovingTargetFactory(float minDirection, float maxDirection, float minDistance, float maxDistance, float minTimePerCycle, float maxTimePerCycle)
    {
        _direction = new FloatRange(minDirection, maxDirection);
        _distance = new FloatRange(minDistance, maxDistance);
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

        GameObject obj = GameObject.Instantiate(Resources.Load("Prefabs/MovingTargetPrefab")) as GameObject;   //Because it's a fast way to do it
        obj.name = name;
        MovingTarget target = obj.GetComponent<MovingTarget>();
        target.init(_direction.getFloat(), _distance.getFloat(), _timePerCycle.getFloat());

        numCreated++;
        return target;
    }

    //Setters
    public void setOverrideName(string value) { _overrideName = value; }
}
