using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum UIElementType
{
    STATIC,
    FLOATING,
    DYNAMIC,
}

public class Btn : MonoBehaviour
{
    public delegate void Action();
    public Action action;
    public bool logicSignal;
    public UIElementType type;
    public TextMeshProUGUI text;
    
    protected virtual void Start()
        {
            if(transform.childCount>0)
                text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        public virtual void True()
        {
            logicSignal = true;
        }

        public virtual void False()
        {
            logicSignal = false;
        }
       
        
        protected virtual void Update()
        {   
        switch (type)
        {
            case UIElementType.STATIC:
                {
                    if (logicSignal)
                        action();
                    break;
                }
            case UIElementType.FLOATING:
                {
                    
                    if (logicSignal)
                    {
                        action();
                        transform.position = new Touch().position;
                    }    
                    break;
                }
            case UIElementType.DYNAMIC:
                {
                    transform.position = new Touch().position;
                    if (!logicSignal)
                        gameObject.SetActive (false);
                    break;
                }
        }
    }
}
