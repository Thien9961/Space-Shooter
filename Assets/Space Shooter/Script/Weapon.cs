using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public float sensivity=20,cooldown=0.25f,damage=1,accuracy=0;
    public Joystick joystick;
    public Button trigger;
    public GameObject ammunition;
    public AudioClip firingSfx;
    public bool ready,fullauto;

    // Start is called before the first frame update
    void Start()
    {
        ready=true;
    }

    IEnumerator coolingdown(float sec)
    {

        yield return new WaitForSeconds(sec);
        ready=true;
    }
    public void fire()
    {
        if(ready)
        {
            ready = false;
            Vector2 hit = new Vector2(transform.position.x + Random.Range(-accuracy,accuracy),transform.position.y+ Random.Range(-accuracy, accuracy));           
            Instantiate(ammunition, Camera.main.ScreenToWorldPoint(hit), ammunition.transform.rotation);
            GetComponent<AudioSource>()?.PlayOneShot(firingSfx);
            StartCoroutine(coolingdown(cooldown));
            Debug.Log("Weapon fire");
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
        Vector2 v = new Vector2(joystick.Horizontal * sensivity , joystick.Vertical * sensivity);
        transform.Translate(v);
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
