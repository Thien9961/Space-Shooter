using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Shop : UIMenu
{
    public Ship[] ship;
    public static int selected = 0;
    public IAP iap;

    public void NextShip()
    {
        if (selected + 1 < ship.Length)
            selected++;
        else
            selected = 0;
        Display(true);
        GameManager.Save();
    }

    public void PreviousShip()
    {
        if(selected-1 >= 0)
            selected--;
        else
            selected=ship.Length-1;
        Display(true);
        GameManager.Save();
    }
    public override void Preset()
    {
        Ship s = ship[selected];
        SetText("Ship Name Text", s.name);
        SetText("Mineral Text", "Mineral: "+GameManager.mineral.ToString());
        SetText("Ship Stat Text", "HP: "+s.hp+"\nDamage: "+s.weapon.damage);
        SetImage("Ship Image", s.GetComponent<UnityEngine.UI.Image>());
        SetButtonAction("Next Ship Button", NextShip);
        SetButtonAction("Previous Ship Button", PreviousShip);
        if (s.owned)
        {
            SetObjectActive("Lock Image", false);
            SetButtonText("Select Ship Button", "Play");
            SetButtonAction("Select Ship Button", s.Deploy);
        }   
        else
        {
            SetObjectActive("Lock Image", true);
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
                SetObjectActive("Lock Image", false);
                GameManager.Save();
            }
            else
            {
                GUI.Window(GetInstanceID(),new Rect(0,0,100,100),PopupMessage,"Not enough mineral");
            }
    }
    public void PopupMessage(int windowID)
    {
        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }
}

