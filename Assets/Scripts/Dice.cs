using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public float angle;
    void Update()
    {
        angle += 10 * Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(0, 0, angle); 
    }
}
