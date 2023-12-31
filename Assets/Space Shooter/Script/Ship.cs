using BayatGames.SaveGameFree;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ship : Destrucible
{
    public float speed;
    public bool owned,invulnerable;
    public int price, mineral;
    public UIMenu HUD;
    public Weapon weapon;
    public Sprite[] damagedSprite;
    public Shield shield;

    float travelTime;

    protected override void Start()
    {
        HUD.hashtable.Clear();
        HUD.Init();
        travelTime = Enviroment.spatialExtend * Time.fixedDeltaTime / speed;
        HUD.SetText("Mineral Text", mineral.ToString().PadLeft(8, '0'));
        HUD.SetText("Ammo Text", weapon.maxAmmo.ToString());
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        base.Start();
        UIManager.main.GameIO();
        if (GameObject.Find("Speed Effect(Clone)") == null)
            Instantiate(Resources.Load<ParticleSystem>("Speed Effect"));
        mineral = 0;
        InvokeRepeating(nameof(UpdateTravelTime), 0, 1f);
    }

    public override void Death(GameObject killer)
    {
        base.Death(killer);
        if (GameManager.highscore < mineral)
            SaveGame.Save<int>("high", mineral);
        GameManager.mineral += mineral;
        GameManager.Save();
        GameManager.extraMineral = mineral;
        UIManager.main.gameOver.gameObject.SetActive(true);
        HUD.hashtable.Clear();
        Destroy(gameObject);
        //GameObject.Find("Ads Manager").GetComponent<InterstitialAdExample>().ShowAd();
    }

    public override void TakeDamage(GameObject source, float amount)
    {
        if (!invulnerable)
        {
            base.TakeDamage(source, amount);
            Camera.main.transform.DOShakePosition(1, 0.5f, UnityEngine.Random.Range(10,15));
            Toggle toggle=(Toggle)UIManager.main.hashtable["Viberation"];
            if (toggle.isOn)
                Vibration.Vibrate(500);
            if(damagedSprite.Length>0)
            {
                Transform t = new GameObject("Crack", typeof(Image)).transform;
                t.SetParent(gameObject.transform);
                t.position = Camera.main.WorldToScreenPoint(source.transform.position);
                t.GetComponent<Image>().raycastTarget = false;
                t.GetComponent<Image>().sprite = damagedSprite[UnityEngine.Random.Range(0, damagedSprite.Length)];
                t.GetComponent<RectTransform>().anchorMin = Vector2.zero;
                t.GetComponent<RectTransform>().anchorMax = Vector2.one;
                t.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,0,UnityEngine.Random.value*360);
            }
        }
        else if (shield != null && HUD.transform.GetComponentInChildren<Shield>()!=null)
            shield.TakeDamage();  
    }

    public void AddMineral(int amount)
    {
        mineral += amount;
        HUD.SetText("Mineral Text", mineral.ToString().PadLeft(8, '0'));
        TextMeshProUGUI t=(TextMeshProUGUI)HUD.hashtable["Mineral Text"];
        if (DOTween.IsTweening(t.rectTransform))
        {
            DOTween.Kill(t.rectTransform);
            t.rectTransform.rotation = Quaternion.Euler(0,0,0);
            t.rectTransform.localScale = Vector3.one;
        } 
        t.transform.DOPunchScale(t.rectTransform.localScale * 1.1f, 0.2f, 10, 0);
        t.transform.DOPunchRotation(new Vector3(0,0,40), 0.5f, 10, 0);
    }

    public string GetInfo()
    {
        string range;
        if (GetComponent<ADM>().sensivity > 3)
            range = "Near";
        else if (GetComponent<ADM>().sensivity > 2.5)
            range = "Medium";
        else
            range = "Far";
        string s = "COST: "+price+ " Mineral\n" +"\nHull Points: " + hp + "\n\nMultishot: " + weapon.multishot + "\n\nWeapon Damage: " + weapon.damage+"(x"+weapon.multishot+ ")\n\nAttack Cooldown: " + weapon.cooldown+"s\n\nAsteroid Detector Module Range: " + range;
            return s;
    }

    public void UpdateTravelTime()
    {
        travelTime-=1f;
        string s= TimeSpan.FromSeconds(travelTime).ToString(@"mm\:ss");
        if (travelTime <= 0 && hp > 0)
            Death(null);
        else
            HUD.SetText("Timer", s);
        

    }
}

