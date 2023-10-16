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
            enviroment.StartCoroutine(enviroment.NaturalSpawn());
        else
            enviroment.StopCoroutine(enviroment.NaturalSpawn());
    }

    // Start is called before the first frame update
    void Start()
    {       
        shop = GameObject.Find("Canvas").transform.Find("Shop").GetComponent<Shop>();
        Load();
        shop.Display(true);
        enviroment= GetComponent<Enviroment>();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    // Update is called once per frame
    public static void Save()
    {
        SaveGame.Save<int>("mineral", mineral);
        SaveGame.Save<int>("selected", Shop.selected);
        foreach (Ship s in shop.ship)
            SaveGame.Save<bool>(s.name, s.owned);       
    }

    public static void Load()
    {
        mineral = SaveGame.Load<int>("mineral");
        Shop.selected = SaveGame.Load<int>("selected");
        foreach (Ship s in shop.ship)
            s.owned = SaveGame.Load<bool>(s.name);     
    }
}
