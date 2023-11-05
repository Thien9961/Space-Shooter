using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCallback : EventCallback
{
    public List<Button> button;
    public UIMenu rewardNotice;
    int _index;

    private void Awake()
    {
        button[0].onClick.AddListener(SetAction0);
        button[1].onClick.AddListener(SetAction1);
        button[2].onClick.AddListener(SetAction2);
        rewardNotice.Init();
        rewardNotice.SetButtonAction("OK", () => { rewardNotice.SetAnimatorBool("Reward Notice", "active", false); Reward(); }) ;
    }

    public void SetAction0()
    {
        _index = 0;
    }

    public void SetAction1()
    {
        _index = 1;
    }

    public void SetAction2()
    {
        _index = 2;
    }

    public void Action()
    {   
        switch (_index)
        {
            case 0:
                {
                    ToMenu();
                    break;
                }
            case 1:
                {
                    Restart();
                    break;
                }
            case 2:
                {
                    Double();
                    break;
                }


        }
    }
    void Restart()
    {
        Hide();
        UIManager.main.ShopIO();
    }

    void Double()
    {
        rewardNotice.SetText("Message", "+" + GameManager.extraMineral);
        rewardNotice.gameObject.SetActive(true);
        Restart();
    }


    void ToMenu()
    {
        Hide();
        GameManager.musicManager.PlayAlbum("In Menu");
        UIManager.main.gameObject.SetActive(true);
        UIManager.main.SetBackground("menu_bg", Color.white);
    }
    // Create a generic delegate for the button click event.
}
