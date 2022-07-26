using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSM : MonoBehaviour
{
    public float speed;
    public Sprite background;
    public GameObject beforeBackground, currentBackground;
    public GameObject[] obstaclePrefabArray;
    float lengthBackground = 14f;
    float colorChange = 0;
    float timeCheck = 0;
    List<GameObject> obstacleList;
    float border = 4.6f;
    float nextTime = 0;
    bool speedUp = true;

    double distance = 0, endDistance = 4365;
    bool gameEnd = false;
    public Text distanceText;

    public GameObject[] walls;

    GameManager gm;

    public GameObject[] planets;
    readonly float[] planetLocation = { 77.79f, 628.32f, 1280.58f,2719.73f, 4357.85f };
    int planetCount = 0;
    void Start()
    {
        obstacleList = new List<GameObject>();
        nextTime = Random.Range(0, 1.2f);
        speed = 3f;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(gameEnd == false && distance > endDistance)
        {
            StartCoroutine(GameToEndEffect());
            gameEnd = true;
        }

        if (gameEnd == false)
            distance += speed * Time.deltaTime;
        else
            distance = endDistance;

        distanceText.text = ((int) distance).ToString();

        if (planetCount < planetLocation.Length && distance > planetLocation[planetCount])
        {
            GameObject temp = Instantiate(planets[planetCount],
                new Vector3(12, 0, 0), Quaternion.identity);
            planetCount++;
        }

        beforeBackground.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        currentBackground.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);

        if (beforeBackground.transform.position.x < -lengthBackground * 2)
        {
            Destroy(beforeBackground);
            beforeBackground = currentBackground;
            currentBackground = Instantiate(currentBackground, new Vector3(2 * lengthBackground, 0, 0), Quaternion.identity);
        }

        if (speedUp == true && speed < 20)
            speed += 0.5f * Time.deltaTime;

        timeCheck += Time.deltaTime;
        if (gameEnd == false && timeCheck >= 1 + nextTime - (speed / 20.0))
        {
            int typetemp = Random.Range(0, obstaclePrefabArray.Length);
            GameObject temp =
                Instantiate(obstaclePrefabArray[typetemp],
                new Vector3(12, Random.Range(-border, border), 0), Quaternion.identity);
            if(typetemp == 0)
            {
                temp.GetComponent<Dice>().angle = Random.Range(0, 359);
            }
            obstacleList.Add(temp);
            timeCheck = 0;
            nextTime = Random.Range(0, 2.2f);
        }
        
    }

    private void FixedUpdate()
    {
        colorChange += Time.fixedDeltaTime / 360000;

        Color tempColor = beforeBackground.GetComponent<SpriteRenderer>().color;
        tempColor.r -= colorChange; tempColor.g -= colorChange; tempColor.b -= colorChange;
        beforeBackground.GetComponent<SpriteRenderer>().color = tempColor;
        currentBackground.GetComponent<SpriteRenderer>().color = tempColor;

    }

    public IEnumerator ResetSpeed()
    {
        float tempTime = 0, tempSpeed = speed;
        speedUp = false;
        while (tempTime < 1)
        {
            speed -= (tempSpeed - 3) * Time.deltaTime;
            tempTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        speedUp = true;
        Debug.Log("ResetSpeed");
    }

    IEnumerator GameToEndEffect()
    {
        for (int i = 0; i < walls.Length; i++)
            walls[i].SetActive(false);

        GameObject character = GameObject.Find("Character");
        character.GetComponent<CharacterMove>().movable = false;
        while(character.transform.position.x < 15)
        {
            character.transform.position += new Vector3(5 * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }

        gm.GameToEnd();
    }
}
