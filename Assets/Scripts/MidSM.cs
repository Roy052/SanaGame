using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MidSM : MonoBehaviour
{
    public Text firstText, secondText;
    public GameObject sanaObject, background;
    public Sprite[] sanaChange;
    GameManager gm;
    string[,] texts = { 
        {"Once time ago,","There is SANA who is speaker of space"}, 
        {"A", "A"},
        {"B", "B"},
        {"C", "C"}
    };
    int textLength;
    void Start()
    {
        StartCoroutine(TextEffect());
        textLength = texts.Length / 2;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    IEnumerator TextEffect()
    {
        yield return new WaitForSeconds(1);
        float timeTemp = 0;
        Color tempColor;
        int count = 0;
        while(count < textLength)
        {
            tempColor = firstText.color;
            tempColor.a = 0;
            firstText.color = tempColor;
            firstText.text = texts[count, 0];
            timeTemp = 0;
            while (timeTemp < 1)
            {
                tempColor.a += Time.deltaTime;
                firstText.color = tempColor;
                timeTemp += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            tempColor = secondText.color;
            tempColor.a = 0;
            secondText.color = tempColor;
            secondText.text = texts[count, 1];

            timeTemp = 0;
            while (timeTemp < 1)
            {
                tempColor.a += Time.deltaTime;
                secondText.color = tempColor;
                timeTemp += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(1);

            tempColor = firstText.color;

            timeTemp = 0;
            while (timeTemp < 1)
            {
                tempColor.a -= Time.deltaTime;
                firstText.color = tempColor;
                secondText.color = tempColor;
                timeTemp += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            count++;
        }

        while (sanaObject.transform.position.x < 0)
        {
            sanaObject.transform.position += new Vector3(3f * Time.deltaTime, 0, 0);
            background.transform.position += new Vector3(3f * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }

        for(int i = 0; i < sanaChange.Length; i++)
        {
            yield return new WaitForSeconds(0.7f);
            sanaObject.GetComponent<SpriteRenderer>().sprite = sanaChange[i];
            tempColor = sanaObject.GetComponent<SpriteRenderer>().color;
            tempColor.a = 0;
            sanaObject.GetComponent<SpriteRenderer>().color = tempColor;
            while (tempColor.a < 1)
            {
                tempColor.a += Time.deltaTime * 2;
                sanaObject.GetComponent<SpriteRenderer>().color = tempColor;
                yield return new WaitForEndOfFrame();
            }
        }
        

        yield return new WaitForSeconds(0.7f);

        gm.MidToGame();
    }
}
