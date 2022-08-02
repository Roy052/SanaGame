using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    GameManager gm;

    private static PauseMenuManager pauseMenuManagerInstance;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (pauseMenuManagerInstance == null)
        {
            pauseMenuManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void PauseON()
    {
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseOFF()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;

    }


    public void ToMenu()
    {
        gm.GameToMenu();
        PauseOFF();
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
