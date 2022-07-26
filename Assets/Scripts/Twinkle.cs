using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twinkle : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Color tempColor;
    public bool brighter = true;
    public bool onTwinkle;
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (onTwinkle)
        {
            tempColor = spriteRenderer.color;

            if (brighter)
            {
                if (tempColor.a < 1)
                {
                    tempColor.a += Time.deltaTime;
                    spriteRenderer.color = tempColor;
                }
                else
                {
                    brighter = false;
                }
            }
            else
            {
                if (tempColor.a > 0)
                {
                    tempColor.a -= Time.deltaTime;
                    spriteRenderer.color = tempColor;
                }
                else
                {
                    brighter = true;
                }
            }
        }
    }

}
