using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSM : MonoBehaviour
{
    public GameObject sanaObject;
    public Text[] mentions, nickname;
    GameManager gm;
    GameObject[] stars;
    int[] mentionRandomList;
    Mention_Info mention_Info;
    public EndObject endObject;
    float textSizeDiff;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        stars = GameObject.FindGameObjectsWithTag("Star");
        mention_Info = new Mention_Info();
        mentionRandomList = new int[5];

        int temp;
        bool isExist;

        
        for(int i = 0; i < mentions.Length; i++)
        {
            while (true)
            {
                temp = Random.Range(0, mention_Info.mentions.Length);
                isExist = false;
                for (int j = 0; j < i; j++)
                {
                    if (mentionRandomList[j] == temp)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist == false)
                {
                    mentionRandomList[i] = temp;
                    break;
                }
            }
        }
        Debug.Log(mentionRandomList[0] + ", " + mentionRandomList[1] + ", " + mentionRandomList[2]);
        
        //mentionRandomList = new int[5] { 6, 10, 11, 0, 1 };
        textSizeDiff = 16.5f;

        StartCoroutine(EndEffect());
    }
    
    IEnumerator EndEffect()
    {
        yield return new WaitForSeconds(0.5f);
        float timeTemp = 0;
        Color tempColor;
        for(int i = 0; i < mentions.Length; i++)
        {
            tempColor = mentions[i].color;
            tempColor.a = 0;
            mentions[i].color = tempColor;
            nickname[i].color = tempColor;
            mentions[i].text = mention_Info.mentions[mentionRandomList[i]];
            mentions[i].fontSize = 50 - (int)(mentions[i].text.Length / textSizeDiff);
            nickname[i].text = "- " + mention_Info.nicknames[mentionRandomList[i]] + " -";
            
            while (tempColor.a < 1)
            {
                tempColor.a += 0.8f * Time.deltaTime;
                mentions[i].color = tempColor;
                nickname[i].color = tempColor;
                timeTemp += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        yield return new WaitForSeconds(3);

        while (mentions[0].fontSize > 15)
        {
            for (int i = 0; i < mentions.Length; i++)
            {
                StartCoroutine(FadeManager.FadeOut(mentions[i], 1));
                StartCoroutine(FadeManager.FadeOut(nickname[i], 1));
            }
            yield return new WaitForSeconds(1);

            for (int i = 0; i < mentions.Length; i++)
            {
                mentions[i].fontSize -= 6;
                nickname[i].fontSize -= 2;
                if (mentions[i].fontSize < 15)
                    mentions[i].text = "";
                if (nickname[i].fontSize < 15)
                    nickname[i].text = "";
            }

            for (int i = 0; i < mentions.Length; i++)
            {
                StartCoroutine(FadeManager.FadeIn(mentions[i], 1));
                StartCoroutine(FadeManager.FadeIn(nickname[i], 1));
            }

            yield return new WaitForSeconds(1);

            bool breakpoint = false;

            for (int i = 0; i < mentions.Length; i++)
                if (mentions[i].fontSize < 20) breakpoint = true;

            if (breakpoint) break;

            yield return new WaitForSeconds(1);
        }

        for (int i = 0; i < mentions.Length; i++)
        {
            mentions[i].text = "";
            nickname[i].text = "";
            StartCoroutine(FadeManager.FadeOut(mentions[i], 1));
            StartCoroutine(FadeManager.FadeOut(nickname[i], 1));
        }

        yield return new WaitForSeconds(1);

        for(int i = 0; i < stars.Length; i++)
        {
            stars[i].GetComponent<Twinkle>().onTwinkle = true;
        }

        yield return new WaitForSeconds(1);

        tempColor = sanaObject.GetComponent<SpriteRenderer>().color;
        tempColor.a = 0;
        while (tempColor.a < 1)
        {
            tempColor.a += 0.8f * Time.deltaTime;
            sanaObject.GetComponent<SpriteRenderer>().color = tempColor;
            timeTemp += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        endObject.endOn = true;
        yield return new WaitForSeconds(7);
    }
}
