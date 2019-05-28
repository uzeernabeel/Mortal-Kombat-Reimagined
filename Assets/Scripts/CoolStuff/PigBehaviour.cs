using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigBehaviour : MonoBehaviour {

    private Rigidbody2D rb2d;
    //private CapsuleCollider2D box2d;
    private bool gotHitFirstTime;
    private bool gotHitSecondTime;
    private Animator animator;
    private float dizzyTime = 0;
    private Vector2 enemyVelocity;
    private SpriteRenderer sRenderer;

    public bool moveRight;
    [Range(1, 10)]
    public int speed = 3;
    public int regularSpeed;
    //public int highSpeed;
    public Transform lookingForStart, lookingForWallEnd, lookingForWallEnd2, lookingForGapEnd2;
    public Transform lookingForPlayerEnd, lookingForPlayerEnd2;
    public bool collisionWithWall;
    public bool collisionWithNothingness;
    //bools to keep track of collision
    public bool playerIsNearMe;
    public bool playerIsNearMe2;
    private bool cantGetMore;

    // Use this for initialization
    void Start () {
        //box2d = GetComponent<CapsuleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        enemyVelocity = rb2d.velocity;
        gotHitFirstTime = false;
        gotHitSecondTime = false;
        //highSpeed = speed + 2;
        regularSpeed = speed;
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

        //wall
        collisionWithWall = Physics2D.Linecast(lookingForStart.position, lookingForWallEnd.position, 1 << 8);
        Debug.DrawLine(lookingForStart.position, lookingForWallEnd.position, Color.blue);

        collisionWithWall = Physics2D.Linecast(lookingForStart.position, lookingForWallEnd2.position, 1 << 8);
        Debug.DrawLine(lookingForStart.position, lookingForWallEnd2.position, Color.blue);

        collisionWithNothingness = Physics2D.Linecast(lookingForStart.position, lookingForGapEnd2.position, 1 << 8);
        Debug.DrawLine(lookingForStart.position, lookingForGapEnd2.position, Color.green);

        //Line Casts [wall, space, player]
        playerIsNearMe = Physics2D.Linecast(lookingForStart.position, lookingForPlayerEnd.position, 1 << 10);
        Debug.DrawLine(lookingForStart.position, lookingForPlayerEnd.position, Color.red);

        playerIsNearMe2 = Physics2D.Linecast(lookingForStart.position, lookingForPlayerEnd2.position, 1 << 10);
        Debug.DrawLine(lookingForStart.position, lookingForPlayerEnd2.position, Color.red);

        if (collisionWithWall || !collisionWithNothingness) {
            moveRight = !moveRight;
        }

        if (playerIsNearMe || playerIsNearMe2) {
            speed = regularSpeed;
            animator.SetBool("run", true);
        } else {
            speed = 0;
            animator.SetBool("run", false);
        }

        if (gotHitFirstTime) {
            dizzyTime += Time.deltaTime;
            if (gotHitSecondTime) {
                animator.SetBool("die", true);
                animator.SetBool("run", false);
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

        if (dizzyTime > 5) {
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
            if (!cantGetMore) {
                GameController.instance.enemiesKilled++;
                cantGetMore = true;
            }
        }

        if (collision.gameObject.tag == "SubZeroBullet") {
            gotHitFirstTime = true;
        }
    }

    
    private void OnDestroy() {
        Destroy(gameObject);
    }
}
