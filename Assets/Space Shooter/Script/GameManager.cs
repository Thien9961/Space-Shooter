using BayatGames.SaveGameFree;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Purchasing;

public class GameManager : MonoBehaviour
{
    public static Ship player;
    public static int mineral=1000;
    public static Shop shop;
    public Enviroment[] enviroment;
    public static EdgeCollider2D screenBound;
    public static GameManager manager;

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

    public void SpawnEnable(bool enable)
    {
        foreach(Enviroment e in enviroment)
                e.gameObject.SetActive(enable);
    }

    // Start is called before the first frame update
    void Start()
    {
        manager=GetComponent<GameManager>();
        if(GameObject.Find("Canvas").transform.Find("Shop").TryGetComponent<Shop>(out Shop s))
        {
            shop = s;
            shop.Display(true);
        }      
        screenBound = new GameObject("Screen Bound").AddComponent<EdgeCollider2D>();
        screenBound.gameObject.layer = 5;
        List<Vector2> points= new List<Vector2>(5);
        Rect r= GameObject.Find("Canvas").GetComponent<Canvas>().pixelRect;
        points.Add(new Vector2(r.xMin, r.yMin));
        points.Add(new Vector2(r.xMin, r.yMax));
        points.Add(new Vector2(r.xMax, r.yMax));
        points.Add(new Vector2(r.xMax, r.yMin));
        points.Add(new Vector2(r.xMin, r.yMin));
        screenBound.SetPoints(points);
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
