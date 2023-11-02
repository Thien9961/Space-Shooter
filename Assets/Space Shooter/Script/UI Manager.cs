using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : UIMenu
{
    public Shop shop;
    public static UIManager main;
    public UIMenu setting;
    public TextMeshProUGUI info;

    public override void Preset()
    {
        SetButtonAction("Play Button", ShopIO);
        SetButtonAction("Setting Button", SettingIO);
        SetButtonAction("Privacy Button", ToPrivacy);
        SetButtonAction("Rate Button", ToRating);
        SetButtonAction("Info Button", InfoIO);
        SetButtonAction("Exit Info", InfoIO);
        SetButtonAction("Exit Setting", SettingIO);
        SetButtonAction("Exit Shop", ShopIO);
    }

    private void Awake()
    {
        main = GetComponent<UIManager>();
        shop.Init();
        main.Init();
        setting.Init();
        Preset();
    }

    public override void SetButtonAction(string buttonName, UnityAction action)
    {
        Button b = (Button)hashtable[buttonName];
        b.onClick.AddListener(action);
    }

    public void ShopIO()
    {
        shop.Display(!shop.gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
        if (shop.gameObject.activeSelf)
            GameManager.musicManager.PlayAlbum("In Shop");
        else
            GameManager.musicManager.PlayAlbum("In Menu");
    }

    public void SettingIO()
    {
        setting.gameObject.SetActive(!setting.gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
    }


    public void ToMenu()
    {
        GameManager.musicManager.PlayAlbum("In Menu");
    }


    public void GameIO()
    {
        GameManager.musicManager.PlayAlbum("In Game");
    }

    public void ToPrivacy()
    {
        Debug.Log("privacy url");
    }

    public void ToRating()
    {
        Debug.Log("rating url");
    }

    public void InfoIO()
    {
        info.gameObject.SetActive(!info.gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
