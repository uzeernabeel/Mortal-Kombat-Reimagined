using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaoKahnBehavior : MonoBehaviour {

    [Range(1, 10)]
    public float bulletTime = 3;
    [Range(1, 10)]
    public float ArrowTime = 3;

    public AudioClip dieSound;
    public AudioClip hitSound;
    private AudioSource audioSource;

    public int TotalPlayerHitMe;
    public int playerHitMe;
    public bool power1Active;
    public bool power2Active;
    public bool power3Active;
    public bool dieTime;
    public bool bossDied;
    public int upDown;
    public bool isGrounded;
    private bool warning;

    public GameObject gate;

    private PlayerManager playerManager;

    public float time;
    public float time2;
    public float time3;
    public GameObject sheevaBulletPrefab;
    public GameObject sheevaArrowPrefab;
    private Vector2 sheevaBulletPosition;
    private Vector3 fallingPosition;
    //private CapsuleCollider2D box2d;

    private Animator animator;
    private Rigidbody2D rb2d;
    private GameObject player;
    private SpriteRenderer renderer2d;
    private CapsuleCollider2D collider2d;

    private int randomInteger = 1;
    public bool moveRight;
    public bool moveUp;
    private float speed = 12f;    //speed of the enemy Walking or Running.

    //bools to keep track of collision
    public bool playerIsNearMe;
    public bool playerIsNearMe2;

    public Transform lookingForPlayerStart, lookingForPlayerEnd, lookingForPlayerEnd2, lookingForNothingness, lookingForNothingness2;

    public bool collisionWithNothingness;
    [SerializeField] private bool comboingNow;
    public GameObject[] bonesPrefabs;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        renderer2d = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<CapsuleCollider2D>();
        //box2d = GetComponent<CapsuleCollider2D>();
        power1Active = true;
        power2Active = false;
        power3Active = false;
        playerHitMe = 0;
        time = 0;
        upDown = 0;
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        warning = true;
        //Invoke("ThrowArrow", 0.5f);
    }

    void ThrowArrow() {

    }

    // Update is called once per frame
    void Update() {

        if (playerHitMe >= 3) {
            playerHitMe = 0;
            time = 0;
            time2 = 0;
            time3 = 0;
            power1Active = false;
            //power2Active = true;
            power3Active = true;
        }

        if (time3 > 10f) {
            animator.SetBool("SlideAttackTime", false);
            //animator.SetBool("ShowWarning", false);
            power1Active = true;
            power2Active = false;
            power3Active = false;
            time = 0;
            time2 = 0;
            time3 = 0;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (!power1Active && !power2Active && !power3Active) {
            rb2d.velocity = Vector2.zero;
        }



        if (power1Active) {

            //rb2d.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

            if (playerHitMe <= 5) {
                time += Time.deltaTime;
                //randomInteger = Random.Range(1, 3);
                //randomInteger = 2;
                //Debug.Log(randomInteger);
            }

            if (randomInteger == 1) {
                if (time > bulletTime && playerHitMe <= 5) {

                    if (moveRight) {
                        sheevaBulletPosition = new Vector2(transform.position.x + .5f, transform.position.y + 1.4f);
                    } else {
                        sheevaBulletPosition = new Vector2(transform.position.x - .5f, transform.position.y + 1.4f);
                    }

                    if (moveRight) {
                        sheevaBulletPrefab.GetComponent<ShaoKahnBullet>().facingLeft = false;
                        //Debug.Log("Some how I ended up here!");
                    } else {
                        sheevaBulletPrefab.GetComponent<ShaoKahnBullet>().facingLeft = true;
                        //Debug.Log("Some how I ended up here2!");
                    }

                    Instantiate(sheevaBulletPrefab, sheevaBulletPosition, Quaternion.identity);
                    //animator.SetTrigger("PowerTime");
                    animator.SetBool("BulletTime", true);
                    animator.SetBool("ArrowTime", false);
                    //animator.SetBool("Power2", false);
                    //.SetBool("run", false);
                    randomInteger = 2;
                    time = 0;
                }
            } else if(randomInteger == 2) {
                if (time > ArrowTime && playerHitMe <= 5) {

                    if (moveRight) {
                        sheevaBulletPosition = new Vector2(transform.position.x + .5f, transform.position.y + 0.8f);
                    } else {
                        sheevaBulletPosition = new Vector2(transform.position.x - .5f, transform.position.y + 0.8f);
                    }

                    if (moveRight) {
                        sheevaArrowPrefab.GetComponent<ShaoKanhArrow>().facingLeft = false;
                        //Debug.Log("Some how I ended up here!");
                    } else {
                        sheevaArrowPrefab.GetComponent<ShaoKanhArrow>().facingLeft = true;
                        //Debug.Log("Some how I ended up here2!");
                    }

                    Instantiate(sheevaArrowPrefab, sheevaBulletPosition, Quaternion.identity);
                    //animator.SetTrigger("PowerTime");
                    animator.SetBool("ArrowTime", true);
                    animator.SetBool("BulletTime", false);
                    //animator.SetBool("Power2", false);
                    //animator.SetBool("run", false);
                    randomInteger = 1;
                    time = 0;
                }
            }

            //move in the direction enemy is facing
            if (moveRight) {
                transform.localScale = new Vector3(1f, 1f, 1f);
                //rb2d.velocity = new Vector2(speed, rb2d.velocity.y);

            } else {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                //rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
            }

            //Line Casts [wall, space, player]
            playerIsNearMe = Physics2D.Linecast(lookingForPlayerStart.position, lookingForPlayerEnd.position, 1 << 10);
            Debug.DrawLine(lookingForPlayerStart.position, lookingForPlayerEnd.position, Color.red);

            playerIsNearMe2 = Physics2D.Linecast(lookingForPlayerStart.position, lookingForPlayerEnd2.position, 1 << 10);
            Debug.DrawLine(lookingForPlayerStart.position, lookingForPlayerEnd2.position, Color.red);

            if (playerIsNearMe || playerIsNearMe2) {
                if (player.transform.position.x > transform.position.x) {
                    moveRight = true;
                }
                if (player.transform.position.x < transform.position.x) {
                    moveRight = false;
                }
            }
        }

        /*if (power2Active) {
            animator.SetBool("PowerTime", false);
            time2 += Time.deltaTime;

            if(time2 > 0 && time2 < 1f) {
                moveUp = true;
            }

            if(time2 >= 1f && time2 < 1.5f) {
                fallingPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            }

            if(time2 >= 1.5f && time2 < 2f) {
                moveUp = false;
            }

            if(time2 > 5f) {
                time2 = 0;
                upDown++;
            }

            if(transform.position.y < 5f && moveUp) {
                rb2d.velocity = new Vector2(0, 10f);
                animator.SetBool("Power2", true);
                Debug.Log("Still in the air somehow");
            } //else if(transform.position.y > 5f && !moveUp) {
                else if (!moveUp || transform.position.y < 5f || trasform.postion.y >= 5f) { 
                transform.position = fallingPosition;
                rb2d.velocity = new Vector2(0, -5f);
                Debug.Log("Not coming down somehow");
                //animator.SetBool("Power2", false);
            }
            
        }*/

        if (power3Active) {
            rb2d.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            time3 += Time.deltaTime;
            animator.SetBool("BulletTime", false);
            animator.SetBool("ArrowTime", false);
            animator.SetBool("SlideAttackTime", true);
            //animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

            if (!warning) {
                if (moveRight) {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                } else {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
                }
            }

            collisionWithNothingness = Physics2D.Linecast(lookingForPlayerStart.position, lookingForNothingness.position, 1 << 8);
            Debug.DrawLine(lookingForPlayerStart.position, lookingForNothingness.position, Color.green);

            collisionWithNothingness = Physics2D.Linecast(lookingForPlayerStart.position, lookingForNothingness2.position, 1 << 8);
            Debug.DrawLine(lookingForPlayerStart.position, lookingForNothingness2.position, Color.green);

            if (!collisionWithNothingness) {
                moveRight = !moveRight;
                animator.SetBool("SlideAttackTime", false);
            }
        }

    }

    private void SetWarning() {
        warning = true;
        //speed = 0;
        rb2d.velocity = Vector2.zero;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void ResetWarning() {
        warning = false;
        //animator.SetBool("ShowWarning", false);
        //animator.SetBool("SlideAttackTime", true);
        //Debug.Log("Reset Warning is working");
        rb2d.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        if (moveRight) {
            transform.localScale = new Vector3(1f, 1f, 1f);
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        } else {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "SubZeroBullet") {
            animator.SetBool("gotHit", true);
            audioSource.PlayOneShot(hitSound);
            //animator.SetTrigger("hit");
            playerHitMe++;
            TotalPlayerHitMe++;
        }

        if (TotalPlayerHitMe >= 10) {
            power1Active = false;
            power2Active = false;
            power3Active = false;

            rb2d.velocity = Vector2.zero;
            animator.SetBool("gotHit", false);
            animator.SetBool("ArrowTime", false);
            animator.SetBool("SlideAttackTime", false);
            animator.SetBool("BulletTime", false);
            animator.SetTrigger("die");
            //dieTime = true;
            audioSource.PlayOneShot(dieSound);
        }

    }

    private void ResetGotHit() {
        animator.SetBool("gotHit", false);
    }

    private void ResetBullet() {
        animator.SetBool("BulletTime", false);
    }

    private void ResetArrow() {
        animator.SetBool("ArrowTime", false);
    }

    private void ResetSlide() {
        //animator.SetBool("ShowWarning", true);
        animator.SetBool("SlideAttackTime", false);
    }

    private void Died() {

        var positionNow = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        collider2d.isTrigger = true;
        renderer2d.enabled = false;
        for (int i = 0; i < bonesPrefabs.Length; i++) {
            Instantiate(bonesPrefabs[i], positionNow, Quaternion.identity);
        }

        gate.SetActive(true);
        Invoke("Enabler", 5f);
    }

    void Enabler() {
        Destroy(gameObject);
    }

    private void OnDestroy() {
        Destroy(gameObject);
    }

}
