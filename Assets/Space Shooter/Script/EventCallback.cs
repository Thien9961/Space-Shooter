using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCallback : MonoBehaviour
{
    public void Kill()
    {
        Destroy(gameObject);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Unhide()
    {
        gameObject.SetActive(true);
    }
    public void Birth()
    {
        
    }
}
