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
    public GameObject star, limiterGet;
    int starCount = 0;

    float lengthBackground = 14f;
    float colorChange = 0;
    float timeCheck = 0, timeCheck1 = 0;
    List<GameObject> obstacleList;
    float border = 4.6f;
    float nextTime = 0, limiterNextTime = 0;
    bool speedUp = true;

    public double endDistance;
    double distance = 0;
    bool gameEnd = false;
    public Text distanceText;

    public GameObject[] walls;

    GameManager gm;

    public bool bigSanaModeON = false;
    public AudioClip[] sfxClips;
    AudioSource sfxaudioSource;

    public GameObject[] planets;
    readonly float[] planetLocation = { 77.79f, 628.32f, 1280.58f, 2719.73f, 4351.40f };
    int planetCount = 0;

    public Slider distanceSlider;

    //rocketTime
    [SerializeField] GameObject rocketPrefab, dangerPrefab;
    [SerializeField] int rocketGenMin, rocketGenMax;
    float rocketTime = 0;
    float timeCheck2 = 0;
    void Start()
    {
        rocketTime = Random.Range(rocketGenMin, rocketGenMax);
        obstacleList = new List<GameObject>();
        nextTime = Random.Range(0, 1.2f);
        limiterNextTime = Random.Range(0, 2.4f);
        speed = 3f;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sfxaudioSource = this.GetComponent<AudioSource>();
        distanceSlider.enabled = false;
    }

    void Update()
    {
        if(gameEnd == false && distance > endDistance)
        {
            StartCoroutine(GameToEndEffect());
            gameEnd = true;
        }

        if (gameEnd == false)
            distance += 2 * speed * Time.deltaTime;
        else
            distance = endDistance;

        distanceText.text = ((int) distance).ToString();
        distanceSlider.value = (float) (distance / endDistance);

        if (planetCount < planetLocation.Length && distance > planetLocation[planetCount])
        {
            GameObject temp = Instantiate(planets[planetCount],
                new Vector3(12, 0, 0), Quaternion.identity);
            planetCount++;
        }

        beforeBackground.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        currentBackground.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);

        if (beforeBackground.transform.position.x < -lengthBackground * 2 + 0.7f)
        {
            Destroy(beforeBackground);
            beforeBackground = currentBackground;
            currentBackground = Instantiate(currentBackground, new Vector3(2 * lengthBackground, 0, 0), Quaternion.identity);
        }

        if (speedUp == true && speed < 15)
            speed += 0.3f * Time.deltaTime;

        timeCheck += Time.deltaTime;
        if (gameEnd == false && timeCheck >= 1 + nextTime - (speed / 15.0))
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
            nextTime = Random.Range(0.5f, 1.7f);
            starCount++;
            if(starCount >= 3)
            {
                StartCoroutine(StarGenerate(2, 10));
                starCount = 0;
            }
        }

        timeCheck1 += Time.deltaTime;
        if(gameEnd == false && bigSanaModeON == false && timeCheck1 >= 4 + limiterNextTime - (speed / 15.0))
        {
            GameObject temp =
                Instantiate(limiterGet,
                new Vector3(12, Random.Range(-border, border), 0), Quaternion.identity);
            temp.GetComponent<Dice>().angle = Random.Range(0, 359);
            timeCheck1 = 0;
            limiterNextTime = Random.Range(1f, 3.4f);
        }

        timeCheck2 += Time.deltaTime;
        if (gameEnd == false && timeCheck2 >= rocketTime - (speed / 15.0))
        {
            StartCoroutine(RocketLaunch());
            timeCheck2 = 0;
            rocketTime = Random.Range(rocketGenMin, rocketGenMax);
        }
    }

    IEnumerator RocketLaunch()
    {
        Vector3 rocketPos = new Vector3(0, 0, 0);
        rocketPos.x = Random.Range(-8.5f, 6f);
        rocketPos.y = -4.85f;
        GameObject tempDanger = Instantiate(dangerPrefab, rocketPos, Quaternion.identity);
        
        yield return new WaitForSeconds(2);
        Destroy(tempDanger);
        rocketPos.y = -6.7f;
        Instantiate(rocketPrefab, rocketPos, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        colorChange += Time.fixedDeltaTime / (150000 * 20);

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

    IEnumerator StarGenerate(float start, float end)
    {
        yield return new WaitForSeconds(Random.Range(start,end));

        GameObject temp =
                Instantiate(star,
                new Vector3(12, Random.Range(-border, border), 0), Quaternion.identity);
        temp.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        temp.GetComponent<Twinkle>().onTwinkle = true;
    }

    public void EatBerrySfx()
    {
        sfxaudioSource.clip = sfxClips[0];
        sfxaudioSource.Play();
    }

    public void LimiterSfx()
    {
        sfxaudioSource.clip = sfxClips[1];
        sfxaudioSource.Play();
    }

    public void BigSanaSfx()
    {
        sfxaudioSource.clip = sfxClips[2];
        sfxaudioSource.Play();
    }

    public void TouchCuteSfx()
    {
        sfxaudioSource.clip = sfxClips[3];
        sfxaudioSource.Play();
    }

    public void HitSanaSfx()
    {
        sfxaudioSource.clip = sfxClips[4];
        sfxaudioSource.Play();

    }

    public void GiftSfx()
    {
        sfxaudioSource.clip = sfxClips[5];
        sfxaudioSource.Play();
    }
}
