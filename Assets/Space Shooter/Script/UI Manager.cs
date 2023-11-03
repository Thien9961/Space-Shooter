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
    public UIMenu setting, info, policy,eula;
    public Image background;
    public string appURL;


    public override void Preset()
    {
        SetButtonAction("Play Button", ShopIO);
        SetButtonAction("Setting Button", SettingIO);
        SetButtonAction("Privacy Button", PrivacyIO);
        SetButtonAction("Rate Button", ToRating);
        SetButtonAction("Info Button", InfoIO);
        SetButtonAction("Exit Info", InfoIO);
        SetButtonAction("Exit Setting", SettingIO);
        SetButtonAction("Exit Shop", ShopIO);
        SetButtonAction("Exit Privacy", PrivacyIO);
        SetButtonAction("Lisence Button", EULAIO);
        SetButtonAction("Exit EULA", EULAIO);
        background.sprite = Resources.Load<Sprite>("menu_bg");
    }

    private void Awake()
    {
        main = GetComponent<UIManager>();
        main.Init();
        shop.Init();
        setting.Init();
        info.Init();
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
        {
            background.sprite = Resources.Load<Sprite>("shop_bg");
            GameManager.musicManager.PlayAlbum("In Shop");
        } 
        else
        {
            GameManager.musicManager.PlayAlbum("In Menu");
            background.sprite= Resources.Load<Sprite>("menu_bg");
        }
            
    }

    public void SettingIO()
    {
        setting.gameObject.SetActive(!setting.gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
        background.sprite = Resources.Load<Sprite>("menu_bg");
    }


    public void ToMenu()
    {
        GameManager.musicManager.PlayAlbum("In Menu");
    }


    public void GameIO()
    {
        GameManager.musicManager.PlayAlbum("In Game");
    }

    public void PrivacyIO()
    {
        policy.gameObject.SetActive(!policy.gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
        background.sprite = Resources.Load<Sprite>("menu_bg");
    }

    public void EULAIO()
    {
        eula.gameObject.SetActive(!eula.gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
        background.sprite = Resources.Load<Sprite>("menu_bg");
    }

    public void ToRating()
    {
        Debug.Log(appURL);
    }

    public void InfoIO()
    {
        info.gameObject.SetActive(!info.gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
        background.sprite = Resources.Load<Sprite>("menu_bg");
    }
}
