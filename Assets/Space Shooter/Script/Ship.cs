using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Ship : Destrucible
{
    public float alertThreshold;
    public bool owned;
    public int price;
    public UIMenu HUD;
    public Weapon weapon;
    public Sprite[] damagedSprite;
    public int mineral; //minerals in ship's storage



    public void Deploy()
    {
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        GameManager.player = Instantiate(gameObject, GameObject.Find("Canvas").transform).GetComponent<Ship>();
        GameManager.player.weapon.owner=GameManager.player.gameObject;
        GameManager.SpawnEnable(true);
        GameManager.shop.Display(false);
        mineral = 0;
    }
    public override void Death(GameObject killer)
    {
        base.Death(killer);
        GameManager.mineral += mineral;
        GameManager.Save();
        Destroy(gameObject);
    }

    public override void TakeDamage(GameObject source, float amount)
    {
        base.TakeDamage(source, amount);
        Camera.main.transform.DOShakePosition(1, 0.5f, 10);
        Transform t = new GameObject("Crack", typeof(Image)).transform;
        t.SetParent(gameObject.transform);
        t.position=Camera.main.WorldToScreenPoint(source.transform.position);
        t.GetComponent<Image>().raycastTarget = false;
        t.GetComponent<Image>().sprite = damagedSprite[Random.Range(0,damagedSprite.Length)];
    }

    public void Alert(bool alert)
    {
        Image i = (Image)HUD.hashtable["Alert"];
        i.gameObject.SetActive(alert);
    }

    // Update is called once per frame
    void Update()
    {
        HUD.SetText("Mineral Text", mineral.ToString());
        if (hp < alertThreshold)
            Alert(true);
        else
            Alert(false);
    }
}