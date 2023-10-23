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
    public float lifeSpan=Mathf.Infinity, travelTime=3;
    public bool autoCollect, hover;
    public Path2D path;
    public int poolIndex;
    // Start is called before the first frame update
    void OnEnable()
    {
        if(autoCollect)
        {
            Invoke(nameof(EffectStart),Time.fixedDeltaTime);
            GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }   
        else
        {
            Debug.Log(autoCollect);
            Invoke(nameof(Flicker), 0.75f*lifeSpan);
            Invoke(nameof(Disappear), lifeSpan);
        }
        if (hover)
        {
            GetComponent<Animator>().speed *= 2;
            Rect bound = GameObject.Find("Canvas").GetComponent<Canvas>().pixelRect;
            Rect r = bound;
            Rec.Expand(ref r, -GetComponent<RectTransform>().rect.width, -GetComponent<RectTransform>().rect.height);
            Draw.Rec(r, Color.red);
            path = new Path2D(Path2D.RandomInsideRect(r, 5));
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
        transform.DOLocalPath(Vec.Conv(path.waypoint), travelTime);
    }

    public virtual void EffectStart()
    {
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
