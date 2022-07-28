using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : MonoBehaviour
{
    float timeCheck = 0;
    float randomTime, randomDirection;

    private void Start()
    {
        randomTime = Random.Range(0.3f, 1);
        randomDirection = Random.Range(0, 2);
    }

    void Update()
    {
        timeCheck += Time.deltaTime;
        if(timeCheck < randomTime)
        {
            this.transform.position += (randomDirection == 0 ? 1 : -1) * new Vector3(0, Time.deltaTime, 0);
        }
        else
        {
            randomTime = Random.Range(0.3f, 1);
            randomDirection = Random.Range(0, 2);
            timeCheck = 0;
        }
            
    }
}
