using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipObject : MonoBehaviour
{
    GameManager gm;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        gm.MidToGame();
    }
}
