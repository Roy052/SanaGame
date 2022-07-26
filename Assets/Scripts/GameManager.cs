using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int gameState = 0; //0 : Menu, 1 : Mid, 2 : Game, 3 : End
    BGMLoader bgmLoader;

    private void Start()
    {
        bgmLoader = GameObject.Find("BGMLoader").GetComponent<BGMLoader>();
    }

    public void MenuToMid()
    {
        SceneManager.LoadScene(1);
    }

    public void MidToGame()
    {
        SceneManager.LoadScene(2);
        bgmLoader.MidMusicON();
    }

    public void GameToEnd()
    {
        bgmLoader.MusicStop();
        SceneManager.LoadScene(3);
        bgmLoader.EndMusicON();
    }
}
