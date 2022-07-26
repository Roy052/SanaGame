using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public float speed = 3f;
    GameSM gameSM;

    private void Start()
    {
        gameSM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSM>();
    }
    void Update()
    {
        speed = gameSM.speed;
        this.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        if (this.transform.position.x <= -20)
            Destroy(this.gameObject);
    }
}
