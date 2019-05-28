using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanoPower : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip snowDieSound;
    public AudioClip brutalityDieSound;

    [Range(1, 10)]
    public float bulletTime = 3;

    private Animator animator;
    private Transform target;
    private PlayerManager playerManager;

    public bool moveRight;
    public float time = 0f;    //time for the bullet.
    public GameObject barakaPowerPrefab;
    public Vector2 barakaPowerPosition;

    public Transform lookingForPlayerStart, lookingForPlayerEnd, lookingForPlayerEnd2;

    //bools to keep track of collision
    public bool playerIsNearMe;
    public bool playerIsNearMe2;

    //variable for playerPower
    private float dizzyTime = 0;
    private bool gotHitFirstTime;
    private bool gotHitSecondTime;

    //variable for playerCombo
    private Rigidbody2D rb2d;
    public GameObject[] bonesPrefabs;
    private CapsuleCollider2D collider2d;
    private SpriteRenderer renderer2d;
    public bool comboHappening;
    private bool cantGetMore;
    [SerializeField] private bool comboingNow;

    // Use this for initialization
    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<CapsuleCollider2D>();
        renderer2d = GetComponent<SpriteRenderer>();
        comboHappening = false;
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

        time += Time.deltaTime;

        if (gotHitFirstTime) {
            dizzyTime += Time.deltaTime;
            if (gotHitSecondTime)
                animator.SetBool("dizzyTime", false);
            else
                animator.SetBool("dizzyTime", true);
        }

        if (dizzyTime > 5) {
            dizzyTime = 0;
            animator.SetBool("dizzyTime", false);
            gotHitFirstTime = false;
        }

        if (gotHitSecondTime) {
            animator.SetTrigger("die");
        }

        if (comboingNow) {
            //rb2d.velocity = Vector2.zero;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        //power coming out refrece to player direction
        if (moveRight)
            barakaPowerPosition = new Vector2(transform.position.x - 3.35f, transform.position.y + 1.25f);
        else
            barakaPowerPosition = new Vector2(transform.position.x + 3.35f, transform.position.y + 1.25f);

        if (time > bulletTime && !gotHitFirstTime) {

            if (moveRight) {
                barakaPowerPrefab.GetComponent<BarakaBullet>().facingLeft = false;
                //Debug.Log("Some how I ended up here!");
            } else {
                barakaPowerPrefab.GetComponent<BarakaBullet>().facingLeft = true;
                //Debug.Log("Some how I ended up here2!");
            }

            if (!comboHappening) {
                Instantiate(barakaPowerPrefab, barakaPowerPosition, Quaternion.identity);
                animator.SetTrigger("powerTime");
            }

            time = 0;
        }

        if (moveRight) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            //rb2d.velocity = new Vector2(speed, rb2d.velocity.y); //No Speed

        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
            //rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        }

        //Line Casts [wall, space, player]
        playerIsNearMe = Physics2D.Linecast(lookingForPlayerStart.position, lookingForPlayerEnd.position, 1 << 10);
        Debug.DrawLine(lookingForPlayerStart.position, lookingForPlayerEnd.position, Color.red);

        playerIsNearMe2 = Physics2D.Linecast(lookingForPlayerStart.position, lookingForPlayerEnd2.position, 1 << 10);
        Debug.DrawLine(lookingForPlayerStart.position, lookingForPlayerEnd2.position, Color.red);

        //Collision with player
        if (playerIsNearMe || playerIsNearMe2) {
            if (target.position.x > transform.position.x) {
                moveRight = true;
            }
            if (target.position.x < transform.position.x) {
                moveRight = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player" && playerManager.ComboStart) {
            rb2d.velocity = Vector2.zero;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            gotHitFirstTime = false;
            gotHitSecondTime = false;
            animator.SetBool("dizzyTime", false);
            animator.SetTrigger("beingHit");
            comboingNow = true;
            comboHappening = true;
            time = 0;
            if (!cantGetMore) {
                GameController.instance.enemiesKilled++;
                cantGetMore = true;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "SubZeroBullet" && gotHitFirstTime) {
            gotHitSecondTime = true;
            animator.SetBool("dizzyTime", false);
            if (!cantGetMore) {
                audioSource.PlayOneShot(snowDieSound);
                GameController.instance.enemiesKilled++;
                cantGetMore = true;
            }
        }

        if (collision.gameObject.tag == "SubZeroBullet") {
            gotHitFirstTime = true;
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "SubZeroBullet") {
            gotHitFirstTime = true;
        }
    }*/

    private void Died() {

        var positionNow = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        collider2d.isTrigger = true;
        renderer2d.enabled = false;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        collider2d.size = Vector2.zero;
        audioSource.PlayOneShot(brutalityDieSound);
        for (int i = 0; i < bonesPrefabs.Length; i++) {
            Instantiate(bonesPrefabs[i], positionNow, Quaternion.identity);
        }

        Invoke("Enabler", 7f);
    }

    void Enabler() {
        Destroy(gameObject);
    }

    private void OnDestroy() {
        Destroy(gameObject);
    }

    void NoBox2d() {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        collider2d.size = Vector2.zero;
    }
}
