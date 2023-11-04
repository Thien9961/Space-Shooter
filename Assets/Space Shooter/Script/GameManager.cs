using BayatGames.SaveGameFree;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static Ship player;
    public static int mineral = 1000,level;
    public Shop shop;
    public Enviroment[] enviroment;
    public static EdgeCollider2D screenBound;
    public static GameManager manager;
    public static float env_interval_coefficent;
    public static MusicPlayer musicManager;
    public float lvlupInterval, env_interval_descrease;//percent decrease
    public bool devMode;

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
        Reset();
        if (manager.lvlupInterval > 0)
            manager.InvokeRepeating(nameof(LevelUp), 0, manager.lvlupInterval);
        player = Instantiate(manager.shop.ship[Shop.selected], GameObject.Find("Canvas").transform).GetComponent<Ship>();
        player.HUD.Init();
        player.weapon.owner = player;
        player.HUD.SetText("Mineral Text",player.mineral.ToString().PadLeft(8,'0'));
        manager.SpawnEnable(true);
    }

    public static void Reset()
    {
       Enviroment.ClearAll();
       manager.CancelInvoke(nameof(LevelUp));
       env_interval_coefficent = 1;
       level = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        manager=GetComponent<GameManager>();
        musicManager= GameObject.Find("Music Manager").GetComponent<MusicPlayer>();
        musicManager.PlayAlbum("In Menu");
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
            Slider s1 = (Slider)UIManager.main.hashtable["Volume"], s2= (Slider)UIManager.main.hashtable["Joystick Sensitivity"];
            SaveGame.Save("voloume", s1.value);
            SaveGame.Save("sensitivity", s2.value);
            SaveGame.Save("mineral", mineral);
            SaveGame.Save("selected", Shop.selected);
            foreach (Ship s in manager.shop.ship)
                SaveGame.Save(s.name, s.owned);
        }
             
    }

    public static void Load()
    {
        if (!manager.devMode)
        {
            Slider s1 = (Slider)UIManager.main.hashtable["Volume"], s2 = (Slider)UIManager.main.hashtable["Joystick Sensitivity"];
            s1.value=SaveGame.Load<float>("voloume");
            s2.value=SaveGame.Load<float>("sensitivity");
            mineral = SaveGame.Load<int>("mineral");
            Shop.selected = SaveGame.Load<int>("selected");
            foreach (Ship s in manager.shop.ship)
                s.owned = SaveGame.Load<bool>(s.name);
        }
    }
}
