using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCallback : MonoBehaviour
{
    public virtual void Kill()
    {
        Destroy(gameObject);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Unhide()
    {
        gameObject.SetActive(true);
    }
    public virtual void Birth()
    {
        
    }
}
