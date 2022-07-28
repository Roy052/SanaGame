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
        
        while(mentions[0].fontSize > 2)
        {
            for (int i = 0; i < mentions.Length; i++)
            {
                mentions[i].fontSize -= 1;
                nickname[i].fontSize -= 1;
                if (nickname[i].fontSize == 1)
                    nickname[i].text = "";
            }
            
            yield return new WaitForFixedUpdate();
        }
        for (int i = 0; i < mentions.Length; i++)
        {
            mentions[i].text = "";
            nickname[i].text = "";
        }
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
