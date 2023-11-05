using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Shop : UIMenu
{
    public Ship[] ship;
    public UIMenu popup;
    public static int selected = 0;
    public void NextShip()
    {
        if (selected + 1 < ship.Length)
            selected++;
        else
            selected = 0;
        Preset();
        GameManager.Save();
    }

    public void PreviousShip()
    {
        if(selected-1 >= 0)
            selected--;
        else
            selected=ship.Length-1;
        Preset();
        GameManager.Save();
    }

    private void Awake()
    {
        popup.Init();
        popup.SetButtonAction("Button", () => { popup.SetAnimatorBool("Pop-up", "active", false); });
    }
    public override void Preset()
    {
        Ship s = ship[selected];
        SetText("Ship Name Text", s.name);
        SetText("Mineral Text", "Mineral: "+GameManager.mineral.ToString());
        SetText("Ship Stat Text", s.GetInfo());
        SetImage("Ship Image", s.GetComponent<Image>());
        SetButtonAction("Next Ship Button", NextShip);
        SetButtonAction("Previous Ship Button", PreviousShip);
        if (s.owned)
        {
            SetButtonText("Select Ship Button", "Play");
            SetButtonAction("Select Ship Button", s.Deploy);
        }   
        else
        {
            SetButtonText("Select Ship Button", "Unlock: " + s.price);
            SetButtonAction("Select Ship Button", Sell);
        }
            
    }

    

    public void Sell()
    {
            if (GameManager.mineral >= ship[selected].price)
            {
                GameManager.mineral -= ship[selected].price;
                ship[selected].owned =true;
                SetText("Mineral Text", "Mineral: "+GameManager.mineral.ToString());
                SetButtonAction("Select Ship Button", ship[selected].Deploy);
                SetButtonText("Select Ship Button", "Play");
                SetAnimatorBool("Lock Image","isLocked", false);
                GameManager.Save();
            }
            else
            {
                popup.SetText("Message", "Not enough mineral.");
                popup.Display(true);
            }
    }

    private void Update()
    {
        Ship s = ship[selected];
        if (s.owned)
            SetAnimatorBool("Lock Image", "isLocked", !s.owned);
        else
            SetAnimatorBool("Lock Image", "isLocked", !s.owned);
    }
}

