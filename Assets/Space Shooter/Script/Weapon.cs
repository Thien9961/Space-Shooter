using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public float sensivity = 20, cooldown = 0.25f, damage = 1, spread=0;
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
    public GameObject trigger, owner;
    public AudioClip firingSfx;
    public bool ready;


    // Start is called before the first frame update
    void Start()
    {
        ready=true;
        trigger.GetComponent<Btn>().action=new Btn.Action(Fire);
        GetComponent<Image>().raycastTarget = false;
    }

    public List<Destrucible> HitScan()
    {
        List<Destrucible> d = new List<Destrucible>();
        for(int i=0;i<multishot;i++)
        {
            Vector2 hit = Camera.main.ScreenToWorldPoint(new Vector2(transform.position.x + Random.Range(-Spread, Spread), transform.position.y + Random.Range(-Spread, Spread)));
            foreach(Asteroid a in Enviroment.GetActiveAsteroid())
                if(a.GetComponent<Collider2D>().bounds.Contains(hit))
                {
                    Instantiate(hitVfx, hit, hitVfx.transform.rotation).transform.localScale=a.transform.localScale;
                    if(owner!=null)
                        a.TakeDamage(owner,damage);
                    if (!d.Contains(a))
                        d.Add(a);
                }         
        }
        return d;
    }

     private IEnumerator StopBlinking(float sec, Color c)
    {
        Image i = GetComponent<Image>();
        Color original = i.color;
        i.color = c;
        yield return new WaitForSeconds(sec);
        i.color = original;
        StopCoroutine(StopBlinking(sec, c));
    }
    public void BlinkCrosshair(Color c, float rate)
    {
        StartCoroutine(StopBlinking(rate, c));
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
            BlinkCrosshair(Color.red, cooldown*0.95f);
            if(firingSfx != null)
                GetComponent<AudioSource>().PlayOneShot(firingSfx);
            StartCoroutine(coolingdown(cooldown));
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
