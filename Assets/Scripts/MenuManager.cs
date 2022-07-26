using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text mainText, tabToStartText;
    public GameObject star;
    void Start()
    {
        Color tempColor;

        tempColor = mainText.color;
        tempColor.a = 0;
        mainText.color = tempColor;

        tempColor = tabToStartText.color;
        tempColor.a = 0;
        tabToStartText.color = tempColor;

        StartCoroutine(StartEffect());
    }

    void Update()
    {
        
    }

    IEnumerator StartEffect()
    {
        yield return new WaitForSeconds(1);
        float timeTemp = 0;
        Color tempColor;
        tempColor = mainText.color;
        tempColor.a = 0;
        mainText.color = tempColor;
        while (timeTemp < 1)
        {
            tempColor.a += Time.deltaTime;
            mainText.color = tempColor;
            timeTemp += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        timeTemp = 0;

        tempColor = tabToStartText.color;
        tempColor.a = 0;
        tabToStartText.color = tempColor;
        while (timeTemp < 1)
        {
            tempColor.a += Time.deltaTime;
            tabToStartText.color = tempColor;
            timeTemp += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        star.GetComponent<Twinkle>().onTwinkle = true;
    }

    public IEnumerator EndMenuEffect()
    {
        float timeTemp = 0;
        Color tempColor;
        tempColor = mainText.color;

        timeTemp = 0;
        while (timeTemp < 1)
        {
            tempColor.a -= Time.deltaTime;
            mainText.color = tempColor;
            tabToStartText.color = tempColor;
            timeTemp += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
