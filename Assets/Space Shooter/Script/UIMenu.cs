using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    public Hashtable hashtable = new Hashtable();
    public TextMeshProUGUI[] text;
    public Button[] button;
    public Slider[] slider;
    public Toggle[] toggle;
    public Dropdown[] dropdown;
    public Image[] image;

    protected virtual void Start()
    {
        foreach (var item in text)
            hashtable.Add(item.name, item);
        foreach (var item in button)
            hashtable.Add(item.name, item);
        foreach (var item in slider)
            hashtable.Add(item.name, item);
        foreach (var item in toggle)
            hashtable.Add(item.name, item);
        foreach (var item in dropdown)
            hashtable.Add(item.name, item);
        foreach (var item in image)
            hashtable.Add(item.name, item);
    }

    public virtual void Preset()
    {

    }

    public virtual void Display(bool display)
    {
        Preset();
        gameObject.SetActive(display);
    }

    public void ObjectSetActive(string objectName, bool active)
    {
        var v = hashtable[objectName] as Component;
        v.gameObject.SetActive(active);
    }

    public void SetText(string textName, string message)
    {
        TextMeshProUGUI t = (TextMeshProUGUI)hashtable[textName];
        t.SetText(message);
    }

    public void ButtonSetText(string buttonName, string message)
    {
        Button b = (Button)hashtable[buttonName];  
        if (b.transform.childCount > 0)
            b.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = message;
    }

    public virtual void ButtonSetAction(string buttonName, UnityEngine.Events.UnityAction action)
    {
        Button b = (Button)hashtable[buttonName];
        b.onClick.RemoveAllListeners();
        b.onClick.AddListener(action);
    }

    public void SetImage(string imageName, Image image)
    {
        Image i = (Image)hashtable[imageName];
        i.sprite = image.sprite;
    }

}

