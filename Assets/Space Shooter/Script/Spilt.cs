using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode;
using Utility;

public class Spilt : PowerUp
{
    // Start is called before the first frame update
    public int qty; 
    public FlyingObject[] fragment;

    public override void EffectStart()
    {
        base.EffectStart();
        for(int i = 0; i < qty; i++)
        {
            int rng=Random.Range(0,fragment.Length);
            FlyingObject f = fragment[rng].GetComponent<FlyingObject>();
            Enviroment.pool.GetFromPool<Transform>(f.poolIndex);
            f.IntialState(GetComponent<FlyingObject>().transform.position, Random.value * Vector3.one, 50, GetComponent<FlyingObject>().transform.localScale, f.maxHp, Color.white);
        }
    }
}
