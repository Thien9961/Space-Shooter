using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public float sensivity = 20, cooldown = 0.25f, damage = 1, spread;
    private float Spread
    {
        get { return spread; }
        set
        {
            spread = Mathf.Abs(value);
            
        }
    }
    public int multishot=1;
    public Joystick joystick;
    public ParticleSystem hitVfx;
    public GameObject trigger;
    public AudioClip firingSfx;
    public bool ready;

    // Start is called before the first frame update
    void Start()
    {
        ready=true;
        trigger.GetComponent<Btn>().action=new Btn.Action(Fire);
    }

    public Destrucible HitScan()
    {
        for(int i=0;i<multishot;i++)
        {
            Collider2D[] victims=Physics2D.OverlapCircleAll(transform.position, Spread);
            Vector2 hit = Camera.main.ScreenToWorldPoint(new Vector2(transform.position.x + Random.Range(-Spread, Spread), transform.position.y + Random.Range(-Spread, Spread)));
            foreach(Collider2D c in victims)
                if(c.bounds.Contains(hit) && c.GetComponent<Destrucible>()!=null)
                {
                    Instantiate(hitVfx, hit, hitVfx.transform.rotation);
                    c.GetComponent<Destrucible>().TakeDamage(damage);
                    Debug.Log(c.name + " tooks " + damage + " damages.");
                    return c.GetComponent<Destrucible>();
                }         
        }
        return null;
    }
    
    IEnumerator coolingdown(float sec)
    {
        yield return new WaitForSeconds(sec);
        ready=true;
    }
    public void Fire()
    {
        if(ready)
        {
            ready = false;
            HitScan();
            if(firingSfx != null)
                GetComponent<AudioSource>().PlayOneShot(firingSfx);
            StartCoroutine(coolingdown(cooldown));
            Debug.Log("Fire!");
        }
        
    }
    public void Aim()
    {
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        float x = Mathf.Clamp( transform.position.x+joystick.Horizontal * sensivity,canvas.pixelRect.xMin,canvas.pixelRect.xMax);
        float y = Mathf.Clamp(transform.position.y+joystick.Vertical * sensivity,canvas.pixelRect.yMin, canvas.pixelRect.yMax);
        Vector2 v = new Vector2(x, y);
        transform.position = v;
    }
    
    // Update is called once per frame
    void Update()
    {
        Aim();
    }
}
