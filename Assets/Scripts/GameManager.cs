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
        bgmLoader.MusicStop();
        StartCoroutine( WaitForLoad(2));
    }

    public void GameToEnd()
    {
        bgmLoader.MusicStop();
        StartCoroutine(WaitForLoad(3));
    }

    public void EndToMenu()
    {
        bgmLoader.MusicStop();
        StartCoroutine(WaitForLoad(0));
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
