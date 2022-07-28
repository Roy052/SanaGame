using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    bool up;
    MapObject mapObject;

    private void Start()
    {
        int temp = Random.Range(0, 2);
        if (temp == 0) up = true;
        else up = false;
        mapObject = this.GetComponent<MapObject>();
    }
    void Update()
    {
        this.transform.position += (up ? 1 : -1) * new Vector3(0, mapObject.speed / 3 * Time.deltaTime, 0);
    }
}
