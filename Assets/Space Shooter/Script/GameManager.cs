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
    public static int mineral = 1000,level;
    public static Shop shop;
    public Enviroment[] enviroment;
    public static EdgeCollider2D screenBound;
    public static GameManager manager;
    public static float env_interval_coefficent;
    public static MusicPlayer musicManager;
    public float lvlupInterval, env_interval_descrease;//percent decrease
    public bool devMode;

    public static void ToMenu()
    {
        musicManager.PlayAlbum("In Menu");
    }

    public static void ToShop()
    {
        musicManager.PlayAlbum("In Shop");
    }

    public static void ToGame()
    {
        musicManager.PlayAlbum("In Game");
    }

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

    public void LevelUp()
    {
        if (env_interval_coefficent > 0)
        {
            env_interval_coefficent = 1 - Mathf.Clamp01(env_interval_descrease) * level;
            level++;
        }
        else
            CancelInvoke(nameof(LevelUp));
 
    }

    public static void Begin()
    {
        if (manager.lvlupInterval > 0)
            manager.InvokeRepeating(nameof(LevelUp), 0, manager.lvlupInterval);
        player = Instantiate(shop.ship[Shop.selected], GameObject.Find("Canvas").transform).GetComponent<Ship>();
        player.weapon.owner = player;
        manager.SpawnEnable(true);
        shop.Display(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        env_interval_coefficent = 1;
        level = 0;
        manager=GetComponent<GameManager>();
        musicManager= GameObject.Find("Music Manager").GetComponent<MusicPlayer>();
        ToMenu();
        if (GameObject.Find("Canvas").transform.Find("Shop").TryGetComponent(out Shop s))
        {
            shop = s;
            shop.Display(true);
        }      


    }

    private void OnApplicationQuit()
    {
        Save();
    }
    // Update is called once per frame
    public static void Save()
    {
        if(!manager.devMode)
        {
            SaveGame.Save<int>("mineral", mineral);
            SaveGame.Save<int>("selected", Shop.selected);
            foreach (Ship s in shop.ship)
                SaveGame.Save<bool>(s.name, s.owned);
        }
             
    }

    public static void Load()
    {
        if (!manager.devMode)
        {
            mineral = SaveGame.Load<int>("mineral");
            Shop.selected = SaveGame.Load<int>("selected");
            foreach (Ship s in shop.ship)
                s.owned = SaveGame.Load<bool>(s.name);
        }
    }
}
