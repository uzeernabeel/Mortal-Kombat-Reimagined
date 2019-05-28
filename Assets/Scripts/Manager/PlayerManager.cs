using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerManager : MonoBehaviour {

    private InputState inputState;
    private Walk walkBehavior;
    private Animator animator;
    private CollisionState collisionState;
    private Rigidbody2D rb2d;
    private Duck duckBehavior;
    public GameObject snowPowerPrefab;
    public GameObject scorpion;
    public Vector2 snowPowerPosition;

    public bool ComboStart;
    public bool comboHappen;
    public bool punching;
    public bool kicking;
    public bool combo;
    public bool isDead;
    public bool gotHit;
    public bool fallenBelow;
    public bool downKick;

    public float gotHitTime = 0;
    public float time;
    public float shootDelay = 0.5f;
    public float timeElapsed = 0;
    public float brutalityTime = 0f;
    private float deadTime = 0f;

    private AudioSource audioSource;
    public AudioClip powerSound;
    public AudioClip playerGotHit;
    public AudioClip playerBrutalitySound;
    public AudioClip funnyPhotoSound;
    private Text brutalityText;
    private GameObject brutalitySign;
    private GameObject funnyPhoto;

    public GameObject[] health;
    private int hearts;

    public AudioClip[] sounds;

    private GameObject brutalityButton;
   

    private void Awake() {

        gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        //transform.localScale -= new Vector3(0.25f, 0.25f, 0.25f);
        inputState = GetComponent<InputState>();
        walkBehavior = GetComponent<Walk>();
        animator = GetComponent<Animator>();
        collisionState = GetComponent<CollisionState>();
        duckBehavior = GetComponent<Duck>();
        rb2d = GetComponent<Rigidbody2D>();
        
        PlayerPrefs.SetInt("MaxHealth", 5);

       
        hearts = PlayerPrefs.GetInt("MaxHealth");
        for(int i = 0; i < hearts; i++) {
            health[i].SetActive(true);
        }
        
        isDead = false;
        ComboStart = false;
        kicking = false;
        punching = false;
        combo = false;
        comboHappen = false;
        downKick = false;
    }

    // Use this for initialization
    void Start() {
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        brutalityText = GameObject.FindGameObjectWithTag("BrutalityText").GetComponent<Text>();
        brutalityButton = GameObject.FindGameObjectWithTag("BrutalityButton");
        brutalityButton.GetComponent<Image>().color = new Color(1, 0, 0, 0.25f);
    }

    // Update is called once per frame
    void Update() {

        /* //jump
         if (inputState.absVelY > 0 && !(collisionState.standing)) {
             animator.SetBool("IsGrouded", false);            
         }
         animator.SetBool("IsDead", isDead);//dead   
         animator.SetFloat("Speed", inputState.absVelX);//run
         animator.SetBool("Sliding", !collisionState.standing && collisionState.onWall);//sliding
         animator.SetBool("Duck", duckBehavior.ducking);//ducking


         //fire projectile
         if (Input.GetButtonDown("Fire2")) {
             animator.SetTrigger("Fire");
             Instantiate(snowPowerPrefab, snowPowerPosition, Quaternion.identity);
         }*/

        /*if(transform.position.y < -15f) {
            //rb2d.isKinematic = true;
            
        = true;
            AfterFallen();
        }*/

        //player Died ------------------------------------------------------------------------------------------------------------------------------------------ End
        if (GameController.instance.playerDied) {
            isDead = true;
            Color tempColor = GetComponent<SpriteRenderer>().color;
            tempColor.a = 1f;
            GetComponent<SpriteRenderer>().color = tempColor;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (gotHit) {
            gotHitTime += Time.deltaTime;
            if(gotHitTime > 5) {
                gotHit = false;
                gotHitTime = 0;
                Color tempColor = GetComponent<SpriteRenderer>().color;
                tempColor.a = 1;
                GetComponent<SpriteRenderer>().color = tempColor;
                Physics2D.IgnoreLayerCollision(10, 11, false);
            }
        }

        if (collisionState.standing) {
            ChangeAnimationState(0);
        }
        if (inputState.absVelX > 0) {
            ChangeAnimationState(1);
            time = 0;
        }

        if (inputState.absVelX == 0) {
            time += Time.deltaTime;
            if (time > 20) {
                ChangeAnimationState(6);
            }
        }

        if (comboHappen) {
            ChangeAnimationState(7);
        }

        //if(inputState.absVelY == 0 && !collisionState.onWall) {
        //    ChangeAnimationState(2);
        //} 

        if (inputState.absVelY > 0 && !(collisionState.standing)) {
            ChangeAnimationState(2);
        }

        if (collisionState.standing && collisionState.onWall) {
            ChangeAnimationState(0);
        }

        animator.speed = walkBehavior.running ? walkBehavior.runMultiplier : 1;

        if (duckBehavior.ducking) {
            ChangeAnimationState(3);
        }

        if (!collisionState.standing && collisionState.onWall) {
            ChangeAnimationState(4);
        }

        //power coming out refrece to player direction
        if (inputState.direction == Directions.Right)
            snowPowerPosition = new Vector2(transform.position.x, transform.position.y + 0.75f);
        else
            snowPowerPosition = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.75f);

        //Power
#if UNITY_EDITOR
        if (Input.GetButtonDown("Fire2")) {
#else
        if (CrossPlatformInputManager.GetButtonDown("Fire2")) {
#endif

            audioSource.PlayOneShot(powerSound, 0.25f);
            animator.SetInteger("AnimState", 5);
            //Debug.Log("C is pressed");
            if (snowPowerPrefab != null && timeElapsed > shootDelay) {
                Instantiate(snowPowerPrefab, snowPowerPosition, Quaternion.identity);
                timeElapsed = 0;
            }

        }

        timeElapsed += Time.deltaTime;

        //combo ------------------------------------------------------------------------------------------------------------------------------------------ Start
#if UNITY_EDITOR
        if (Input.GetButtonDown("Fire3")) {
#else
        if (CrossPlatformInputManager.GetButtonDown("Fire3")) {
#endif
            if (GameController.instance.brutalites > 0) { 
                audioSource.PlayOneShot(playerBrutalitySound, 0.75f);
                //ChangeAnimationState(7);
                ComboStart = true;
                Color tempColor = new Color(1f, .28f, .28f, 1f);
                GetComponent<SpriteRenderer>().color = tempColor;

                //handle if pressed brutality power when hit by an enemy
                Physics2D.IgnoreLayerCollision(10, 11, false);
                gotHit = false;
                gotHitTime = 0;

                //Debug.Log("v is pressed");
            } else {
                //play other sound.

            }
        }

        if (ComboStart) {
            brutalityTime += Time.deltaTime;
            if (inputState.direction == Directions.Right) {
                rb2d.velocity = new Vector2(7, rb2d.velocity.y);
            } else {
                rb2d.velocity = new Vector2(-7, rb2d.velocity.y);
            }
        }

        if(brutalityTime > 1.5f) {
            brutalityTime = 0;
            ComboStart = false;
            if (!comboHappen) {
                Color tempColor = new Color(1f, 1f, 1f, 1f);
                GetComponent<SpriteRenderer>().color = tempColor;
            }
        }


#if UNITY_EDITOR
        if (Input.GetButtonDown("Fire4")) {
#else
        if (CrossPlatformInputManager.GetButtonDown("Fire4")) {
#endif
            ChangeAnimationState(11);
            downKick = true;
            //ComboStart = true;
           
        }

        /*if( Input.GetButtonDown("Fire3")) {
            ChangeAnimationState(9);
            //punching = true;
        }

        // the high punch my favorite
#if UNITY_EDITOR
        if (duckBehavior.ducking && Input.GetButtonDown("Fire3")) {
#else
        if (duckBehavior.ducking && CrossPlatformInputManager.GetButtonDown("Fire3")) {
#endif
            ChangeAnimationState(8);
            Debug.Log("High Punch");
        }*/

        if (isDead) {
            ChangeAnimationState(10);
            deadTime += Time.deltaTime;
            for(int i = 0; i < PlayerPrefs.GetInt("MaxHealth"); i++) {
                health[i].SetActive(true);
            }
        }

        if(deadTime > 1) {
            deadTime = 0;
            isDead = false;
        }
    }

    /*private void AfterFallen() {
        rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
        var clone = Instantiate(scorpion);
        clone.transform.position = new Vector3(1f, 5f, 0);
        transform.position = new Vector3(10f, 15f, 0);
        fallenBelow = false;
    }*/

    void NotKicking() {
        //animator.ResetTrigger("kick");
        kicking = false;
    }

    void NotPunching() {
        //animator.ResetTrigger("punch");
        punching = false;
    }

    void NotCombo() {
        //animator.ResetTrigger("highPunch");
        combo = false;
    }

    void ChangeAnimationState(int value) {
        animator.SetInteger("AnimState", value);
    }

    void powerFunction() {
        ChangeAnimationState(5);
        //animator.SetBool("Power", true);
        Debug.Log("B is pressed");
    }

    void NoDownKick() {
        downKick = false;
    }

    void ZeroSpeed() {
        //rb2d.velocity = Vector2.zero;
        comboHappen = false;
        Color tempColor = new Color(1f, 1f, 1f, 1f);
        Physics2D.IgnoreLayerCollision(10, 11, false);
        GetComponent<SpriteRenderer>().color = tempColor;
        rb2d.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        brutalitySign = (GameObject)Instantiate(Resources.Load("heroicbrutality"));
        brutalitySign.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 3.5f, 0);
        audioSource.PlayOneShot(funnyPhotoSound);
        funnyPhoto = (GameObject)Instantiate(Resources.Load("funnyPhoto"));
        funnyPhoto.transform.position = new Vector3(Camera.main.transform.position.x + 6.806f, Camera.main.transform.position.y - 3.384f, 0);
        Invoke("Done", 2f);
    }

    public void Done() {
        Destroy(brutalitySign);
        Destroy(funnyPhoto);
    }

    public void Kick() {
        kicking = true;

        if (combo) {
            animator.SetTrigger("highPunch");
            combo = false;
            kicking = false;
            return;
        }

        if (punching) {
            animator.SetTrigger("punch");
            combo = true;
            punching = false;
            kicking = false;
            return;
        }
            animator.SetTrigger("kick");
            punching = true;

        /*if (kicking) {
            animator.SetTrigger("punch");
        } else if (combo) {
            animator.SetTrigger("highPunch");
        } else {
            animator.SetTrigger("kick");
            kicking = true;
            Debug.Log("We are here ha!");
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "JumpingPad") {
            //rb2d.AddForce(new Vector2(0f, 7f));
            Debug.Log("High Jump Hi!");
            rb2d.AddForce(new Vector2(0f, 20f), ForceMode2D.Impulse);
        }

        if (collision.gameObject.tag == "Enemy" && ComboStart) {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            GameController.instance.brutalites--;
            Physics2D.IgnoreLayerCollision(10, 11, true);
            if (GameController.instance.brutalites <= 0) {
                brutalityButton.GetComponent<Image>().color = new Color(1, 0, 0, 0.25f);
                //brutalityButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            brutalityText.text = GameController.instance.brutalites.ToString();
            comboHappen = true;
        } else if (collision.gameObject.tag == "Enemy" && !ComboStart) {
            audioSource.PlayOneShot(playerGotHit);
            gotHit = true;
            //hearts--;
            GameController.instance.health--;
            health[GameController.instance.health].SetActive(false);
            Color tempColor = GetComponent<SpriteRenderer>().color;
            tempColor.a = .47f;
            GetComponent<SpriteRenderer>().color = tempColor;
            ChangeAnimationState(12);
            //adding force to body to bounce back
            if (inputState.direction == Directions.Right) {
                //rb2d.velocity = new Vector2(0, 5f);
                rb2d.velocity = new Vector2(-1f, 10f);
                //rb2d.AddForce(new Vector2(-5f, 5f));
                //rb2d.AddForce(new Vector2(transform.position.x - 5f, 5f), ForceMode2D.Impulse);

            } else {
                //rb2d.velocity = new Vector2(0, 5f);
                rb2d.velocity = new Vector2(1f, 10f);
                //rb2d.AddForce(new Vector2(5f, 5f));
                //rb2d.AddForce(new Vector2(transform.position.x + 5f, 5f), ForceMode2D.Impulse);

            }

            Physics2D.IgnoreLayerCollision(10, 11, true);
            //Physics2D.IgnoreLayerCollision(7, 4, true);
            //Physics2D.IgnoreLayerCollision(7, 5, true);
            //Physics2D.IgnoreLayerCollision(7, 6, true);

        }

        //if (GameController.instance.stageDone) {
        //    animator.SetInteger("AnimeState", 11);
        //}

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "bullet" || collision.gameObject.tag == "Enemy") {
            audioSource.PlayOneShot(playerGotHit);
            gotHit = true;
            //hearts--;
            GameController.instance.health--;
            health[GameController.instance.health].SetActive(false);
            Color tempColor = GetComponent<SpriteRenderer>().color;
            tempColor.a = .47f;
            GetComponent<SpriteRenderer>().color = tempColor;
            ChangeAnimationState(12);
            //adding force to body to bounce back
            if (inputState.direction == Directions.Right) {
                rb2d.velocity = new Vector2(-1f, 10f);
            } else {
                rb2d.velocity = new Vector2(1f, 10f);
            }

            Physics2D.IgnoreLayerCollision(10, 11, true);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision) {
        else if (collision.gameObject.tag == "Bomb" && GameController.instance.bombTouched) {
            rb2d.velocity = new Vector2(-1f, 10f);
        }
    }*/

    void comboStartFalse() {
        ComboStart = false;
    }

    private void OnDestroy() {
        Destroy(gameObject);
    }

    void AfterDead() {
        //gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;
        isDead = false;
    }

    void Sound0() { audioSource.PlayOneShot(sounds[0]); }
    void Sound1() { audioSource.PlayOneShot(sounds[1]); }
    void Sound2() { audioSource.PlayOneShot(sounds[2]); }
    void Sound3() { audioSource.PlayOneShot(sounds[3]); }
    void Sound4() { audioSource.PlayOneShot(sounds[4]); }
    void Sound5() { audioSource.PlayOneShot(sounds[5]); }
    void Sound6() { audioSource.PlayOneShot(sounds[6]); }
    void Sound7() { audioSource.PlayOneShot(sounds[7]); }
    void Sound8() { audioSource.PlayOneShot(sounds[8]); }


}
