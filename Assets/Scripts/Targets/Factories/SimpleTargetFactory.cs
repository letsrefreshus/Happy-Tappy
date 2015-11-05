using UnityEngine;

public class SimpleTargetFactory : BaseTargetFactory
{
    private static string baseName = "Simple Target #";
    private static int numCreated = 0;
    private static int autoNamed = 0;
    private static string _overrideName = null;

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

        GameObject obj = GameObject.Instantiate(Resources.Load("Prefabs/SimpleTargetPrefab")) as GameObject;   //Because it's a fast way to do it
        obj.name = name;

        numCreated++;
        return obj.GetComponent<SimpleTarget>();
    }

    //Setters
    public void setOverrideName(string value) { _overrideName = value; }
}
