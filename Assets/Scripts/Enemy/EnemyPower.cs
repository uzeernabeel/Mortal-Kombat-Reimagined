using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPower : MonoBehaviour {

    [Range(1, 10)]
    public float bulletTime = 3;
    public GameObject PowerPrefab;
    public float addX;
    public float addY;

    public Transform lookingForPlayerStart, lookingForPlayerEnd, lookingForPlayerEnd2;

    private Animator animator;
    private Transform target;
    private PlayerManager playerManager;

    private bool moveRight;
    private float time = 0f;    //time for the bullet.
    private Vector2 PowerPosition;
    //bools to keep track of collision
    private bool playerIsNearMe;
    private bool playerIsNearMe2;

    //variable for playerPower
    private float dizzyTime = 0;
    private bool gotHitFirstTime;
    private bool gotHitSecondTime;

    //variable for playerCombo
    private Rigidbody2D rb2d;
    public GameObject[] bonesPrefabs;
    public GameObject[] bloodPrefabs;
    private CapsuleCollider2D collider2d;
    private SpriteRenderer renderer2d;
    private bool comboHappening;


    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<CapsuleCollider2D>();
        renderer2d = GetComponent<SpriteRenderer>();
        comboHappening = false;
    }
	
	// Update is called once per frame
	void Update () {
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

        //power coming out refrece to player direction
        if (moveRight)
            PowerPosition = new Vector2(transform.position.x + addX, transform.position.y + addY);
        else
            PowerPosition = new Vector2(transform.position.x - addX, transform.position.y + addY);

        if (time > bulletTime && !gotHitFirstTime) {

            if (moveRight) {
                PowerPrefab.GetComponent<ReptileBullet2>().facingLeft = false;
                //Debug.Log("Some how I ended up here!");
            } else {
                PowerPrefab.GetComponent<ReptileBullet2>().facingLeft = true;
                //Debug.Log("Some how I ended up here2!");
            }

            if (!comboHappening) {
                Instantiate(PowerPrefab, PowerPosition, Quaternion.identity);
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
            rb2d.bodyType = RigidbodyType2D.Static;
            //Debug.Log("Kabal is getting Hit!");
            animator.SetTrigger("beingHit");
            comboHappening = true;
            time = 0;
        }

        if (collision.gameObject.tag == "SubZeroBullet") {
            gotHitFirstTime = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "SubZeroBullet" && gotHitFirstTime) {
            gotHitSecondTime = true;
            animator.SetBool("dizzyTime", false);
        }

        if (collision.gameObject.tag == "SubZeroBullet") {
            gotHitFirstTime = true;
        }
    }

    private void Died() {

        var positionNow = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        collider2d.isTrigger = true;
        renderer2d.enabled = false;
        for (int i = 0; i < bonesPrefabs.Length; i++) {
            Instantiate(bonesPrefabs[i], positionNow, Quaternion.identity);
        }

        Instantiate(bloodPrefabs[0], positionNow, Quaternion.identity);
        var tempPos1 = new Vector3(positionNow.x, positionNow.y + 0.05f, positionNow.z);
        Instantiate(bloodPrefabs[1], tempPos1, Quaternion.identity);
        

        Invoke("Enabler", 5f);
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
