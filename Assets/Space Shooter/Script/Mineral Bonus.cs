using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralBonus : PowerUp
{
    public Ship recipient;
    public int amount;

    // Start is called before the first frame update
    public override void EffectStart()
    {
        base.EffectStart();
        if (recipient != null)
            recipient.mineral += amount;
    }
}
