using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Ship player;

    public static Vector2 RandomLocInRect(Rect r) 
    {
        float x=Random.Range(r.xMin,r.xMax);
        float y=Random.Range(r.yMin,r.yMax);
        return new Vector2(x,y);
    }

    public static void PlaySfx(AudioClip clip,Vector2 pos)
    {
        if (clip != null)
        {
            GameObject g =new GameObject(clip.name);
            g.transform.position = pos;
            g.AddComponent<AudioSource>().PlayOneShot(clip);            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(new GameObject("Ship")).AddComponent<Ship>();
        player.hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
