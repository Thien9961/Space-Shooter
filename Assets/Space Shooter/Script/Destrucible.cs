using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrucible : MonoBehaviour
{
    public float maxHp;
    [HideInInspector] public float hp;
    public AudioClip onDeathSfx;
    public ParticleSystem onDeathVfx;

    public virtual void TakeDamage(GameObject source, float amount) 
    {
        hp-=amount;
        if (!(hp > 0))
            Death(source);
    }

    protected virtual void Start()
    {
        hp = maxHp;
    }

    public virtual void Death(GameObject killer)
    {
        GameManager.PlaySfx(onDeathSfx,transform.position);
        if(onDeathVfx != null)
            Instantiate(onDeathVfx, transform.position, onDeathVfx.transform.rotation);

    }

}
