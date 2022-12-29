using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour
{
    public float speed = 5;
    public Sprite normalSana,bigSana ;
    public Sprite[] hitSanas;
    public bool movable;
    public Slider limiterBar;
    bool alreadyHit = false, bigsanaMode = false;
    Rigidbody2D rb2d;
    Vector2 movement;

    GameSM gameSM;
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        gameSM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSM>();
        movable = true;
        limiterBar.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(bigsanaMode == false && limiterBar.value >= 1)
        {
            StartCoroutine(BecomeBigSana());
            gameSM.BigSanaSfx();
        }
    }

    void FixedUpdate(){

        if(movable)
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);
    
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Limiter")
        {
            limiterBar.value += 0.2f;
            Destroy(other.gameObject);
            gameSM.LimiterSfx();
        }
        if(alreadyHit == false && other.gameObject.tag == "Obstacle"){
            alreadyHit = true;
            StartCoroutine( BecomeHitSana(other.name[0] - '0'));
            switch(other.name[0] - '0')
            {
                case 0:
                    gameSM.TouchCuteSfx();
                    break;
                case 1:
                    gameSM.EatBerrySfx();
                    break;
                case 2:
                    gameSM.HitSanaSfx();
                    break;
                case 3:
                    gameSM.GiftSfx();
                    break;
            }
                
        }
    }

    public IEnumerator BecomeBigSana(){

        alreadyHit = true; bigsanaMode = true;

        float currentSpeed = gameSM.speed;
        gameSM.speed = 15;
        gameSM.bigSanaModeON = true;

        this.GetComponent<SpriteRenderer>().sprite = bigSana;
        this.transform.localScale = new Vector3(1,1,1);

        Vector3 tempMovement = new Vector3(0, 0, 0);
        if (this.transform.position.x > 0) tempMovement.x = -0.7f;
        else tempMovement.x = 0.7f;
        if (this.transform.position.y > 0) tempMovement.y = -0.5f;
        else tempMovement.y = 0.5f;
        this.transform.position += tempMovement;

        
        yield return new WaitForSeconds(5);
        
        float tempTime = 0;
        Color tempColor = this.GetComponent<SpriteRenderer>().color;
        for(int i = 0; i < 3; i++){
            while(tempTime < 0.5f){
                tempTime += Time.deltaTime;
                tempColor.a -= Time.deltaTime;
                this.GetComponent<SpriteRenderer>().color = tempColor;
                yield return new WaitForEndOfFrame();
            }
            while(tempTime < 1){
                tempTime += Time.deltaTime;
                tempColor.a += Time.deltaTime;
                this.GetComponent<SpriteRenderer>().color = tempColor;
                yield return new WaitForEndOfFrame();
            }
            tempTime = 0;
        }

        //After Big Sana Mode
        limiterBar.value = 0;
        alreadyHit = false; 
        bigsanaMode = false;
        gameSM.speed = currentSpeed;
        gameSM.bigSanaModeON = false;

        this.GetComponent<SpriteRenderer>().sprite = normalSana;
        this.transform.localScale = new Vector3(0.4f,0.4f,1);
    }

    public IEnumerator BecomeHitSana(int num){

        StartCoroutine(gameSM.ResetSpeed());

        this.GetComponent<SpriteRenderer>().sprite = hitSanas[num];
        Color tempColor = this.GetComponent<SpriteRenderer>().color;
        tempColor.a = 0.7f;
        this.GetComponent<SpriteRenderer>().color = tempColor;

        yield return new WaitForSeconds(1);
        this.GetComponent<SpriteRenderer>().sprite = normalSana;
        tempColor = this.GetComponent<SpriteRenderer>().color;
        tempColor.a = 1;
        this.GetComponent<SpriteRenderer>().color = tempColor;

        alreadyHit = false;
    }
}
