using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleBirdBehavior : MonoBehaviour {

    private Rigidbody2D rb2d;
    private BoxCollider2D box2d;
    private bool gotHitFirstTime;
    private bool gotHitSecondTime;
    private Animator animator;
    private float dizzyTime = 0;
    private Vector2 enemyVelocity;
    private SpriteRenderer sRenderer;

    public bool moveRight;
    public float speed = 1f; //speed of the enemy Flying
    public Transform lookingForStart, lookingForWallEnd;
    public bool collisionWithWall;
    private bool cantGetMore;

    // Use this for initialization
    void Start () {
        box2d = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        enemyVelocity = rb2d.velocity;
        gotHitFirstTime = false;
        gotHitSecondTime = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (moveRight) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        }

        collisionWithWall = Physics2D.Linecast(lookingForStart.position, lookingForWallEnd.position, 1 << 8);
        Debug.DrawLine(lookingForStart.position, lookingForWallEnd.position, Color.blue);

        if (!collisionWithWall) {
            moveRight = !moveRight;
        }

        if (gotHitFirstTime) {
            dizzyTime += Time.deltaTime;
            if (gotHitSecondTime) {
                animator.SetTrigger("die");
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                sRenderer.color = new Color(1, 1, 1, 1);
                rb2d.velocity = Vector2.zero;
                animator.enabled = true;
            } else {
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                rb2d.velocity = Vector2.zero;
                sRenderer.color = new Color(0.435f, 0.3f, 1, 1);
                animator.enabled = false;
            }
        }

        if(dizzyTime > 5) {
            dizzyTime = 0;
            gotHitFirstTime = false;
            rb2d.constraints = RigidbodyConstraints2D.None;
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation
                | RigidbodyConstraints2D.FreezePositionY;
            rb2d.velocity = enemyVelocity;
            sRenderer.color = new Color(1, 1, 1, 1);
            animator.enabled = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "SubZeroBullet" && gotHitFirstTime) {
            gotHitSecondTime = true;
            //transform.localScale += new Vector3(2.5f, 2.5f, 0);
            if (!cantGetMore) {
                GameController.instance.enemiesKilled++;
                cantGetMore = true;
            }
        }

        if (collision.gameObject.tag == "SubZeroBullet") {
            gotHitFirstTime = true;
        }
    }

    void NoBox2d() {
        //rb2d.bodyType = RigidbodyType2D.Static;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        box2d.size = Vector2.zero;
    }

    private void OnDestroy() {
        Destroy(gameObject);
    }
}
