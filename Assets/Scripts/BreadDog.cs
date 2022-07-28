using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadDog : MonoBehaviour
{
    float timeCheck = 0, timeCheck2 = 0;
    float jumpTime, jumpLength;

    private void Start()
    {
        jumpTime = 0.3f;
        jumpLength = 1;
    }

    void Update()
    {
        timeCheck += Time.deltaTime;
        if (timeCheck > jumpTime)
        {
            timeCheck2 += Time.deltaTime;
            if(timeCheck2 < jumpLength / 2)
            {
                this.transform.position +=  new Vector3(0, 2 * Time.deltaTime, 0);
            }
            else if(timeCheck2 < jumpLength)
            {
                this.transform.position +=  -1 * new Vector3(0, 2 * Time.deltaTime, 0);
            }
            else
            {
                timeCheck = 0;
                timeCheck2 = 0;
            }
        }

    }
}
