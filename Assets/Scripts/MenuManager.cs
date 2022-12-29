using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text mainText, tabToStartText;
    public GameObject star;
    BGMLoader bgmLoader;
    public MenuObject menuObject;
    int languageType = 0;
    GameManager gm;

    [SerializeField] GameObject hat, tree;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        languageType = gm.languageType;
        bgmLoader = GameObject.Find("BGMLoader").GetComponent<BGMLoader>();
        Color tempColor;

        tempColor = mainText.color;
        tempColor.a = 0;
        mainText.color = tempColor;
        tree.GetComponent<SpriteRenderer>().color = tempColor;
        hat.GetComponent<SpriteRenderer>().color = tempColor;

        tempColor = tabToStartText.color;
        tempColor.a = 0;
        tabToStartText.color = tempColor;

        StartCoroutine(StartEffect());

        bgmLoader.StartMusicON();
    }

    IEnumerator StartEffect()
    {
        yield return new WaitForSeconds(1.5f);
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
        menuObject.onStart = true;

        StartCoroutine(ChristmasEffectStart());
    }

    IEnumerator ChristmasEffectStart()
    {
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(FadeManager.FadeIn(hat.GetComponent<SpriteRenderer>(), 1));
        StartCoroutine(FadeManager.FadeIn(tree.GetComponent<SpriteRenderer>(), 1));
    }

    IEnumerator ChristmasEffectEnd()
    {
        yield return new WaitForSeconds(1);
        while (tree.transform.position.x > -20)
        {
            tree.transform.position -= new Vector3(3f * Time.deltaTime, 0, 0);
            hat.transform.position -= new Vector3(3f * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator EndMenuEffect()
    {
        float timeTemp = 0;
        Color tempColor;
        tempColor = mainText.color;

        StartCoroutine(ChristmasEffectEnd());
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
    
    public void LangaugeChange(int val)
    {
        if(val == 0)
        {
            mainText.text = "Sana's" + "\nLong Long Journey";
            tabToStartText.text = "- Tab To Start -";
        }
        else
        {
            mainText.text = "사나의" + "\n머나먼 여행";
            tabToStartText.text = "- 탭하고 시작하기 -";
        }
    }
}
