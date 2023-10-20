using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : FlyingObject
{
    public DustCloud dustCloud;
    public override void Death(GameObject killer)
    {
        base.Death(killer);        
        if (DustCloud.cloud == gameObject)
            DustCloud.cloud = null;
        dustCloud.StartCoroutine(dustCloud.NaturalSpawn());
        Debug.Log("Cloud " + name + "Destroyed");
    }
}
