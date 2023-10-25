using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Ship : Destrucible
{
    public float speed;
    public bool owned,invulnerable;
    public int price, mineral;
    public UIMenu HUD;
    public Weapon weapon;
    public Sprite[] damagedSprite;

    public void Deploy()
    {
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        GameManager.player = Instantiate(gameObject, GameObject.Find("Canvas").transform).GetComponent<Ship>();
        GameManager.player.weapon.owner=GameManager.player.gameObject;
        GameManager.manager.SpawnEnable(true);
        GameManager.shop.Display(false);
        if(GameObject.Find("Speed Effect(Clone)") ==null)
            Instantiate(Resources.Load<ParticleSystem>("Speed Effect"));
        mineral = 0;
    }
    public override void Death(GameObject killer)
    {
        base.Death(killer);
        GameManager.mineral += mineral;
        GameManager.Save();
        Destroy(gameObject);
        GameObject.Find("Ads Manager").GetComponent<InterstitialAdExample>().ShowAd();
    }

    public override void TakeDamage(GameObject source, float amount)
    {
        if (!invulnerable)
        {
            base.TakeDamage(source, amount);
            Camera.main.transform.DOShakePosition(1, 0.5f, 10);
            Transform t = new GameObject("Crack", typeof(Image)).transform;
            t.SetParent(gameObject.transform);
            t.position = Camera.main.WorldToScreenPoint(source.transform.position);
            t.GetComponent<Image>().raycastTarget = false;
            t.GetComponent<Image>().sprite = damagedSprite[Random.Range(0, damagedSprite.Length)];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        HUD.SetText("Mineral Text", mineral.ToString());
    }
}

