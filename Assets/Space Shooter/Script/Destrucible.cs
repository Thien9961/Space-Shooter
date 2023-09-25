using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrucible : MonoBehaviour
{
    public float hp;
    public AudioClip onDeathSfx;
    public ParticleSystem onDeathVfx;
    // Start is called before the first frame update

    public void TakeDamage(float amount) 
    {
        hp-=amount;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
