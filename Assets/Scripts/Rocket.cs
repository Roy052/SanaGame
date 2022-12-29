using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 3;
    GameSM gameSM;

    private void Start()
    {
        gameSM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSM>();
    }
    void Update()
    {
        speed = gameSM.speed / 1.6f;
        this.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        if (this.transform.position.y > 7)
            Destroy(this.gameObject);
    }
}
