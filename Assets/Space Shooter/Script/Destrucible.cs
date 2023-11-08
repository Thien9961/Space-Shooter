using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrucible : MonoBehaviour
{
    public float maxHp;
    [HideInInspector] public float hp;
    public AudioClip onDeathSfx;
    public readonly int vfxPool=13;//Death vfx pool index

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
        if(killer!=null)
        {
            ParticleSystem p = Enviroment.pool.GetFromPool<Transform>(vfxPool).GetComponent<ParticleSystem>();
            p.transform.localScale = transform.localScale;
            p.transform.position = transform.position;
            p.Play();
        } 
    }

}
