using DG.Tweening;
using Redcode.Pools;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

public class PowerUp : MonoBehaviour
{
    public float lifeSpan=Mathf.Infinity, travelTime=3,appearChance=100;
    public bool autoCollect, hover;
    public int poolIndex;
    // Start is called before the first frame update

    public virtual void Init(Vector2 screenPos)
    {
        transform.position = screenPos;
    }

    public virtual void Begin()
    {
        if (autoCollect)
            EffectStart();
        else
        {
            Invoke(nameof(Flicker), 0.75f * lifeSpan);
            Invoke(nameof(Disappear), lifeSpan);
        }
        if (hover)
        {
            Rect r = GameObject.Find("Canvas").GetComponent<Canvas>().pixelRect;
            Draw.Rec(r, Color.green);
            Move();
        }
    }


    public void Flicker()
    {
        GetComponent<Animator>().enabled = true;
    }

    public void Move()
    {       
        Invoke(nameof(Move), travelTime);
        Vector3 v= Vec.RandomInRect(GameObject.Find("Canvas").GetComponent<Canvas>().pixelRect);
        Debug.DrawLine(transform.position,v, Color.magenta,travelTime);
        transform.DOMove(v, travelTime);
    }

    public virtual void EffectStart()
    {
        if(TryGetComponent(out AudioSource source))
            source.Play();
        CancelInvoke();
        DOTween.Kill(transform);
        GetComponent<Animator>().enabled = false;
        Enviroment.pool.TakeToPool(poolIndex, GetComponent<RectTransform>());
    }
 
    public void Disappear()
    {
        CancelInvoke();
        DOTween.Kill(transform);
        GetComponent<Animator>().enabled = false;
        Enviroment.pool.TakeToPool(poolIndex, GetComponent<RectTransform>());
    }
    // Update is called once per frame
}
