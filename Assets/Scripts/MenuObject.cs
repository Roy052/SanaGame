using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObject : MonoBehaviour
{
    GameManager gm;
    public MenuManager mm;
    public GameObject background, star;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(ShrinkEffect());
    }

    void Update()
    {
        
    }

    IEnumerator ShrinkEffect()
    {
        float timeTemp = 0;
        while(timeTemp < 1)
        {
            this.transform.localScale -= new Vector3(0.7f * Time.deltaTime, 0.7f * Time.deltaTime, 0);
            background.transform.localScale -= new Vector3(0.7f * Time.deltaTime, 0.7f * Time.deltaTime, 0);
            timeTemp += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnMouseDown()
    {
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
