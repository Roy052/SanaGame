using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndObject : MonoBehaviour
{
    GameManager gm;
    public bool endOn;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        endOn = false;
    }
    private void OnMouseDown()
    {
        if(endOn)
            gm.EndToMenu();
    }
}
