using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Ship : Destrucible
{
    public bool owned;
    public int price;
    public GameObject HUD,info;
    public Weapon weapon;

    public void Deploy()
    {
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        GameManager.player = Instantiate(gameObject, GameObject.Find("Canvas").transform).GetComponent<Ship>();
        GameManager.SpawnEnable(true);
        GameManager.shop.Display(false);
    }
    public override void Death()
    {
        base.Death();
        Debug.Log("Game Over");
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
