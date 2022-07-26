using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5;
    public Sprite normalSana,bigSana,hitSana;
    public bool movable;
    bool alreadyHit = false;
    Rigidbody2D rb2d;
    Vector2 movement;

    GameSM gameSM;
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        gameSM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSM>();
        movable = true;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate(){

        if(movable)
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);
    
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(alreadyHit == false && other.gameObject.tag == "Obstacle"){
            alreadyHit = true;
            StartCoroutine( BecomeHitSana());
        }
    }

    public IEnumerator BecomeBigSana(){
        this.GetComponent<SpriteRenderer>().sprite = bigSana;
        this.transform.localScale = new Vector3(1,1,1);

        yield return new WaitForSeconds(3);
        
        float tempTime = 0;
        Color tempColor = this.GetComponent<SpriteRenderer>().color;
        for(int i = 0; i < 3; i++){
            while(tempTime > 0.5f){
                tempTime += Time.deltaTime;
                tempColor.a -= Time.deltaTime;
                this.GetComponent<SpriteRenderer>().color = tempColor;
                yield return new WaitForEndOfFrame();
            }
            while(tempTime > 1){
                tempTime += Time.deltaTime;
                tempColor.a += Time.deltaTime;
                this.GetComponent<SpriteRenderer>().color = tempColor;
                yield return new WaitForEndOfFrame();
            }
            tempTime = 0;
        }
        this.GetComponent<SpriteRenderer>().sprite = normalSana;
        this.transform.localScale = new Vector3(0.4f,0.4f,1);
    }

    public IEnumerator BecomeHitSana(){

        StartCoroutine(gameSM.ResetSpeed());

        this.GetComponent<SpriteRenderer>().sprite = hitSana;
        Color tempColor = this.GetComponent<SpriteRenderer>().color;
        tempColor.a = 0.5f;
        this.GetComponent<SpriteRenderer>().color = tempColor;

        yield return new WaitForSeconds(1);
        this.GetComponent<SpriteRenderer>().sprite = normalSana;
        tempColor = this.GetComponent<SpriteRenderer>().color;
        tempColor.a = 1;
        this.GetComponent<SpriteRenderer>().color = tempColor;

        alreadyHit = false;
    }
}
