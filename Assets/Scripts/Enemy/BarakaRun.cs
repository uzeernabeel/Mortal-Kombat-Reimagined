using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarakaRun : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip snowDieSound;
    public AudioClip brutalityDieSound;

    private Animator animator;
    private Rigidbody2D rb2d;
    private PlayerManager playerManager;
    private CapsuleCollider2D collider2d;
    private SpriteRenderer renderer2d;
    public GameObject[] bonesPrefabs;

    public bool moveRight;
    public float speed = 1f;    //speed of the enemy Walking or Running.

    private float dizzyTime = 0;
    private bool gotHitFirstTime;
    private bool gotHitSecondTime;
    private Vector2 enemyVelocity;

    //gameObjects for lineCast reference.
    public Transform lookingForStart, lookingForWallEnd, lookingForWallEnd2, lookingForGapEnd, lookingForGapEnd2;

    //bools to keep track of collision
    public bool collisionWithWall;
    public bool collisionWithNothingness;
    [SerializeField]private bool comboingNow;
    private bool cantGetMore;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        collider2d = GetComponent<CapsuleCollider2D>();
        renderer2d = GetComponent<SpriteRenderer>();
        enemyVelocity = rb2d.velocity;
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {

        //move in the direction enemy is facing
        if (moveRight) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        }

        //wall
        collisionWithWall = Physics2D.Linecast(lookingForStart.position, lookingForWallEnd.position, 1 << 8);
        Debug.DrawLine(lookingForStart.position, lookingForWallEnd.position, Color.blue);

        collisionWithWall = Physics2D.Linecast(lookingForStart.position, lookingForWallEnd2.position, 1 << 8);
        Debug.DrawLine(lookingForStart.position, lookingForWallEnd2.position, Color.blue);

        //Nothingness
        collisionWithNothingness = Physics2D.Linecast(lookingForStart.position, lookingForGapEnd.position, 1 << 8);
        Debug.DrawLine(lookingForStart.position, lookingForGapEnd.position, Color.green);

        collisionWithNothingness = Physics2D.Linecast(lookingForStart.position, lookingForGapEnd2.position, 1 << 8);
        Debug.DrawLine(lookingForStart.position, lookingForGapEnd2.position, Color.green);

        // check Collisions with wall or nothingness
        if (collisionWithWall || !collisionWithNothingness) {
            moveRight = !moveRight;
        }

        if (gotHitFirstTime) {
            dizzyTime += Time.deltaTime;
            if (gotHitSecondTime)
                animator.SetBool("dizzyTime", false);
            else {
                animator.SetBool("dizzyTime", true);
                rb2d.velocity = Vector2.zero;
            }
        }

        if (dizzyTime > 5) {
            dizzyTime = 0;
            animator.SetBool("dizzyTime", false);
            rb2d.velocity = enemyVelocity;
            gotHitFirstTime = false;
        }

        if (gotHitSecondTime) {
            animator.SetTrigger("die");
            rb2d.velocity = Vector2.zero;
        }

        if (comboingNow) {
            //rb2d.velocity = Vector2.zero;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Player" && playerManager.ComboStart) {
            //speed = 0;
            rb2d.velocity = Vector2.zero;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            gotHitFirstTime = false;
            gotHitSecondTime = false;
            animator.SetBool("dizzyTime", false);
            animator.SetTrigger("beingHit");
            comboingNow = true;
            if (!cantGetMore) {
                GameController.instance.enemiesKilled++;
                cantGetMore = true;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "SubZeroBullet" && gotHitFirstTime) {
            gotHitSecondTime = true;
            if (!cantGetMore) {
                audioSource.PlayOneShot(snowDieSound);
                GameController.instance.enemiesKilled++;
                cantGetMore = true;
            }
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
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        collider2d.size = Vector2.zero;
        audioSource.PlayOneShot(brutalityDieSound);
        for (int i = 0; i < bonesPrefabs.Length; i++) {
            Instantiate(bonesPrefabs[i], positionNow, Quaternion.identity);
        }

        Invoke("Enabler", 10f);
    }

    void Enabler() {
        Destroy(gameObject);
    }

    void NoBox2d() {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        collider2d.size = Vector2.zero;
    }

    private void OnDestroy() {
        Destroy(gameObject);
    }

}
