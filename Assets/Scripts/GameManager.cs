using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int gameState = 0; //0 : Menu, 1 : Mid, 2 : Game, 3 : End
    public int languageType = 0;
    PauseMenuManager pauseMenuManager;
    BGMLoader bgmLoader;
    bool ispauseON = false;
    private static GameManager gameManagerInstance;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        bgmLoader = GameObject.Find("BGMLoader").GetComponent<BGMLoader>();
        pauseMenuManager = GameObject.FindGameObjectWithTag("PauseMenuManager").GetComponent<PauseMenuManager>();
        pauseMenuManager.PauseOFF();
        /*
        if (!PlayerPrefs.HasKey("LanguageType"))
        {
            PlayerPrefs.SetInt("LanguageType", languageType);
        }
        else
            languageType = PlayerPrefs.GetInt("LanguageType");
        LanguageChange(languageType);*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ispauseON)
            {
                pauseMenuManager.PauseOFF();
                
            }
            else
            {
                pauseMenuManager.PauseON();
            }
            ispauseON = !ispauseON;
        }
    }

    public void MenuToMid()
    {
        gameState = 1;
        SceneManager.LoadScene(1);
    }

    public void MidToGame()
    {
        gameState = 2;
        bgmLoader.MusicStop();
        StartCoroutine( WaitForLoad(2));
        
    }

    public void GameToEnd()
    {
        gameState = 3;
        bgmLoader.MusicStop();
        StartCoroutine(WaitForLoad(3));
    }

    public void EndToMenu()
    {
        gameState = 0;
        bgmLoader.MusicStop();
        StartCoroutine(WaitForLoad(0));
    }

    public void GameToMenu()
    {
        gameState = 0;
        bgmLoader.MusicStop();
        StartCoroutine(WaitForLoad(0));
    }

    public void LanguageChange(int val)
    {
        languageType = val;
        GameObject sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
        if (sceneManager != null)
        {
            if (gameState == 0)
            {
                sceneManager.GetComponent<MenuManager>().LangaugeChange(val);
            }
            else if(gameState == 1)
            {
                sceneManager.GetComponent<MidSM>().LanguageChange(val);
            }
        }
        
    }

    IEnumerator WaitForLoad(int scenenum)
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(scenenum);
        if(scenenum == 2) bgmLoader.MidMusicON();
        else if(scenenum == 3) bgmLoader.EndMusicON();
        else bgmLoader.StartMusicON();
    }
}
