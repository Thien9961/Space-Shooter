using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrucible : MonoBehaviour
{
    public float hp;
    public AudioClip onDeathSfx;
    public ParticleSystem onDeathVfx;
    // Start is called before the first frame update


    public virtual void TakeDamage(float amount) 
    {
        hp-=amount;
    }

    public virtual void Death()
    {

        GameManager.PlaySfx(onDeathSfx,transform.position);
        if(onDeathVfx != null)
            Instantiate(onDeathVfx, transform.position, onDeathVfx.transform.rotation);

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!(hp > 0))
            Death();
    }
}
