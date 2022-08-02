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
        {"Once upon a time,","There is SANA who is speaker of space"}, 
        {"After meeting with the council", "She joined the council."},
        {"As a member of the council", "They laughed and cried together."},
        {"But now it's time for her", "to graduate and embark on a long journey."}
    };
    string[,] texts_Korean =
    {
        {"옛날옛적에,","공간의 대변인인 사나가 있었다."},
        {"의회와 만난 이후로", "그녀는 의회에 합류했다."},
        {"의회의 일원으로", "그들은 함께 웃고 울었다."},
        {"하지만 그녀는 시간이 되어", "졸업을 하며 먼 여행을 떠난다."}
    };
    int textLength, count = -1;
    int languageType = 0;
    void Start()
    {
        StartCoroutine(TextEffect());
        textLength = texts.Length / 2;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        languageType = gm.languageType;
    }

    IEnumerator TextEffect()
    {
        yield return new WaitForSeconds(1);
        float timeTemp = 0;
        Color tempColor;
        count = 0;
        while(count < textLength)
        {
            tempColor = firstText.color;
            tempColor.a = 0;
            firstText.color = tempColor;
            if (languageType == 0)
                firstText.text = texts[count, 0];
            else
                firstText.text = texts_Korean[count, 0];
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
            if (languageType == 0)
                secondText.text = texts[count, 1];
            else
                secondText.text = texts_Korean[count, 1];

            timeTemp = 0;
            while (timeTemp < 1)
            {
                tempColor.a += Time.deltaTime;
                secondText.color = tempColor;
                timeTemp += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(2);

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

    public void LanguageChange(int val)
    {
        languageType = val;
        if(count != -1)
        {
            if (languageType == 0)
            {
                firstText.text = texts[count, 0];
                secondText.text = texts[count, 1];
            }
            else
            {
                firstText.text = texts_Korean[count, 0];
                secondText.text = texts_Korean[count, 1];
            }
        }
    }
}
