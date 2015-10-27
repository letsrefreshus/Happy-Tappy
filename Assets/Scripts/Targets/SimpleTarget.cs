using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SimpleTarget : BaseTarget
{
    private static string baseName = "Simple Target #";
    private static int numCreated = 0;

    //Simple target will be a score giving target on hit. and a time sucking target on miss.
    public static SimpleTarget create(string name = null)
    {
        if(name == null)
        {
            name = baseName + numCreated;
        }

        GameObject obj = Instantiate(Resources.Load("Prefabs/SimpleTargetPrefab")) as GameObject;   //Because it's a fast way to do it
        obj.name = name;

        numCreated++;
        return obj.GetComponent<SimpleTarget>();
    }
}
