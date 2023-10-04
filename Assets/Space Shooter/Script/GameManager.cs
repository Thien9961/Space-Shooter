using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Ship player;
    public static int mineral=1000;
    public static Shop shop;
    public static Enviroment enviroment;

    public static Vector2 RandomLocInRect(Rect r) 
    {
        float x=Random.Range(r.xMin,r.xMax);
        float y=Random.Range(r.yMin,r.yMax);
        return new Vector2(x,y);
    }

    public static IEnumerator StopBlinking(Renderer renderer,float sec,Color c,bool oneshot)
    {
        Color original = renderer.material.color;
        renderer.material.color=c;
        yield return new WaitForSeconds(sec);
        renderer.material.color = original;
        if(!oneshot) 
            BlinkObject(renderer.gameObject,c,sec,oneshot);
    }
    public static void BlinkObject(GameObject whichobject, Color c, float rate, bool oneshot)
    {
       whichobject.GetComponent<MonoBehaviour>().StartCoroutine(StopBlinking(whichobject.GetComponent<Renderer>(), rate,c,oneshot));
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

    public static void SpawnEnable(bool enable)
    { 
        if (enable)
            enviroment.InvokeRepeating("Spawn", 1, Random.Range(enviroment.spawnInterval.x, enviroment.spawnInterval.y));
        else
            enviroment.CancelInvoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        shop = GameObject.Find("Canvas").transform.Find("Shop").GetComponent<Shop>();
        //Load();
        shop.Display(true);
        enviroment= GetComponent<Enviroment>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        // Save the player's score.
        SaveGame.Save<int>("mineral", mineral);
        SaveGame.Save<int>("selected", Shop.selected);
    }

    public void Load()
    {
        // Load the player's score.
        mineral = SaveGame.Load<int>("mineral");
        Shop.selected = SaveGame.Load<int>("selected");
        // Set the player's score
    }
}
