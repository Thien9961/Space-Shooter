using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class Shield : PowerUp
{
    public float duration;
    public Ship target;
    public Image Vfx;
    public static Image shield;
    public static int stack=0;

    public void Init(Vector2 position, Ship target)
    {
        this.target = target;
        transform.position = position;
        
    }
    public override void EffectStart()
    {
        base.EffectStart();
        if(target.invulnerable)
            stack++;
        else
        {
            shield=Instantiate(Vfx, target.transform.Find("HUD").transform);
            if (duration > 0)
                Invoke(nameof(EffectEnd), duration);
            target.invulnerable = true;
            if (Vfx != null)
                Vfx.enabled = true;
        }  
    }

    public void EffectEnd()
    {
        if(stack>0)
        {
            stack--;
            Invoke(nameof(EffectEnd), duration);
        } 
        else
        {
            target.invulnerable = false;
            if (shield != null)
                shield.GetComponent<Animator>().SetBool("alive", false);
        } 
    }

    public void DestroyShield()
    {
        Destroy(shield.gameObject);
        Debug.Log("Shield Effect worn out");
    }
}
