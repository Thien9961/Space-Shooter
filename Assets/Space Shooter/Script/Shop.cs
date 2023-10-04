using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting;
using System;
using System.Runtime.CompilerServices;

public class Shop : UIMenu
{
    public Ship[] ship;
    public static int selected = 0;

    public override void Preset()
    {
        Ship s = ship[selected];
        SetText("Ship Name Text", name);
        SetText("Mineral Text", "Mineral: "+GameManager.mineral.ToString());
        SetImage("Ship Image", s.GetComponent<UnityEngine.UI.Image>());
        if (s.owned)
        {
            ObjectSetActive("Lock Image", false);
            ButtonSetText("Select Ship Button", "Play");
            ButtonSetAction("Select Ship Button", s.Deploy);
        }   
        else
        {
            ObjectSetActive("Lock Image", true);
            ButtonSetText("Select Ship Button", "Unlock: " + s.price);
            ButtonSetAction("Select Ship Button", Sell);
        }
            
    }

    public void Sell()
    {
            if (GameManager.mineral >= ship[selected].price)
            {
                GameManager.mineral -= ship[selected].price;
                ship[selected].owned = true;
                SetText("Mineral Text", "Mineral: "+GameManager.mineral.ToString());
                ButtonSetAction("Select Ship Button", ship[selected].Deploy);
                ButtonSetText("Select Ship Button", "Play");
                ObjectSetActive("Lock Image", false);
            }
            else
            {
                Debug.Log("not enough");
            }
    }
}

