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
    public UIMenu setting, info, policy,eula,gameOver;
    public string appURL;


    public override void Preset()
    {
        //UnityAction<float> set_joystick_sens = (value) => { Weapon.sensivity = value; };
        SetBackground("menu_bg",Color.white);
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
        //SetSliderAction("Joystick Sensitivity", set_joystick_sens);
        
    }

    private void Awake()
    {
        Init();
        main = GetComponent<UIManager>();
        shop.Init();
        setting.Init();
        info.Init();
        gameOver.Init();
        gameOver.SetButtonAction("Button", () => { gameOver.SetAnimatorBool("Game Over", "active", false); });
        gameOver.SetButtonAction("Double", () => { gameOver.SetAnimatorBool("Game Over", "active", false); /*GameObject.Find("Ads Manager").GetComponent<InterstitialAdExample>().ShowAd();*/ });
        gameOver.SetButtonAction("Back to menu", () => { gameOver.SetAnimatorBool("Game Over", "active", false); });
        Preset();
        Slider s = (Slider)hashtable["Volume"];
        s.onValueChanged.AddListener((val) => { AudioListener.volume = val; });

    }

    public void SetBackground(string spritePath,Color color)
    {
        Image background = (Image)hashtable["Background"];
        background.sprite = Resources.Load<Sprite>(spritePath);
        background.color=color;
        background.raycastTarget = false;
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
            SetBackground("shop_bg",Color.white);
            GameManager.musicManager.PlayAlbum("In Shop");
        } 
        else
        {
            gameObject.SetActive(true);
            GameManager.musicManager.PlayAlbum("In Menu");
            SetBackground("menu_bg", Color.white);
        }
            
    }

    public void SettingIO()
    {
        setting.gameObject.SetActive(!setting.gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
        SetBackground("menu_bg", Color.white);
    }



    public void GameIO() 
    {
        GameManager.musicManager.PlayAlbum("In Game");
        shop.Display(false);
        SetBackground("shop_bg", Color.clear);
        gameObject.SetActive(false);
    }

    public void PrivacyIO()
    {
        policy.gameObject.SetActive(!policy.gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
        SetBackground("menu_bg", Color.white);
    }

    public void EULAIO()
    {
        SetBackground("menu_bg", Color.white);
        eula.gameObject.SetActive(!eula.gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ToRating()
    {
        if (appURL != null)
            Application.OpenURL(appURL);
    }

    public void InfoIO()
    {
        info.gameObject.SetActive(!info.gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
        SetBackground("menu_bg", Color.white);
    }
}
