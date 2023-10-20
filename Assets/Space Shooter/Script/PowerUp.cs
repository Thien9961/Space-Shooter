using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    public float lifeSpan=Mathf.Infinity, bounciness;
    public bool autoCollect;
    // Start is called before the first frame update
    void Start()
    {

        if(autoCollect)
            EffectStart();
        else 
        {
            Invoke(nameof(Disappear), lifeSpan);
            gameObject.layer = 5;
            transform.DOLocalPath(RandomPath(), lifeSpan, PathType.Linear);
        }
            
    }

    public Vector3[] RandomPath()
    {
        
        Rect bound = GameObject.Find("Canvas").GetComponent<Canvas>().pixelRect;
        float x=GetComponent<Image>().rectTransform.rect.width;
        float y = GetComponent<Image>().rectTransform.rect.height;
        bound.width -= x / 2;
        bound.height -= y / 2;
        Vector3[] v = new Vector3[Random.Range(0, 10)];
        Vector2 v2;
        for(int i=0; i<v.Length; i++)
        {
            v2 = GameManager.RandomLocInRect(bound);
            v[i].x = v2.x;
            v[i].y = v2.y;
            v[i].z = 0;
        }
        return v;
    }

    public void EffectStart()
    {
        DOTween.Kill(transform);
        Destroy(gameObject); 
    }
    public void Disappear()
    {
        Debug.Log(name + "disappeared");
        Destroy(gameObject);
    }
    // Update is called once per frame

    private void FixedUpdate()
    {
        
    }
}
