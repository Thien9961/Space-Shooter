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
using static BayatGames.SaveGameFree.Examples.ExampleSaveCustom;

public class Ship : Destrucible
{
    public float speed;
    public bool owned,invulnerable;
    public int price, mineral;
    public UIMenu HUD;
    public Weapon weapon;
    public Sprite[] damagedSprite;
    public Shield shield;

    public void Deploy()
    {
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        GameManager.Begin();
        UIManager.main.GameIO();
        if (GameObject.Find("Speed Effect(Clone)") ==null)
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
            t.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            t.GetComponent<RectTransform>().anchorMax = Vector2.one;
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
        } 
        t.transform.DOPunchScale(t.rectTransform.localScale * 1.1f, 0.2f, 10, 0);
        t.transform.DOPunchRotation(new Vector3(0,0,40), 0.5f, 10, 0);
    }
}

