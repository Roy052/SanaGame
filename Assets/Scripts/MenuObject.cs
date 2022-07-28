using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObject : MonoBehaviour
{
    GameManager gm;
    public MenuManager mm;
    public GameObject background, star;
    public bool onStart;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        onStart = false;
        StartCoroutine(ShrinkEffect());
    }

    void Update()
    {
        
    }

    IEnumerator ShrinkEffect()
    {
        float timeTemp = 0;
        while(timeTemp < 2f)
        {
            this.transform.localScale -= new Vector3(0.7f * 0.5f *  Time.deltaTime, 0.7f * 0.5f * Time.deltaTime, 0);
            background.transform.localScale -= new Vector3(0.7f * 0.5f * Time.deltaTime, 0.7f * 0.5f * Time.deltaTime, 0);
            timeTemp += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnMouseDown()
    {
        if(onStart)
        StartCoroutine(BeforeStart());
        
    }
    
    IEnumerator BeforeStart()
    {
        StartCoroutine(mm.EndMenuEffect());
        yield return new WaitForSeconds(1);
        while(this.transform.position.x > -8)
        {
            this.transform.position -= new Vector3(3f * Time.deltaTime, 0, 0);
            background.transform.position -= new Vector3(3f * Time.deltaTime, 0, 0);
            star.transform.position -= new Vector3(3f * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }
        
        gm.MenuToMid();
    }
}
