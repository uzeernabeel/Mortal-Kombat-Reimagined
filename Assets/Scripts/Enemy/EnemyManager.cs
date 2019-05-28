using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    private PlayerManager playerManager;

    public BoxCollider2D collider2d;
    public bool playerInTerritory;

    private Animator animator;
    private Rigidbody2D rb2d;
    private GameObject player;
    private Transform target;

    public bool moveRight;

    public float speed = 1f;    //speed of the enemy Walking or Running.

    //gameObjects for lineCast reference.
    public Transform lookingForPlayerStart, lookingForPlayerEnd, lookingForPlayerEnd2,
        lookingForWallStart, lookingForWallEnd, lookingForWallEnd2,
        lookingForGapStart, lookingForGapEnd, lookingForGapEnd2;

    //bools to keep track of collision
    public bool playerIsNearMe;
    public bool playerIsNearMe2;
    public bool collisionWithWall;
    public bool collisionWithNothingness;

    //for easier animator control
    public float absVelX;
    public float absVelY;
    

    // Use this for initialization
    void Start () {
        playerInTerritory = false;
        collider2d = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        target = player.GetComponent<Transform>();
        playerManager = player.GetComponent<PlayerManager>();
    }

    private void FixedUpdate() {
        absVelX = Mathf.Abs(rb2d.velocity.x);
        absVelY = Mathf.Abs(rb2d.velocity.y);
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


        //Line Casts [wall, space, player]
        playerIsNearMe = Physics2D.Linecast(lookingForPlayerStart.position, lookingForPlayerEnd.position, 1 << 10);
        Debug.DrawLine(lookingForPlayerStart.position, lookingForPlayerEnd.position, Color.red);

        playerIsNearMe2 = Physics2D.Linecast(lookingForPlayerStart.position, lookingForPlayerEnd2.position, 1 << 10);
        Debug.DrawLine(lookingForPlayerStart.position, lookingForPlayerEnd2.position, Color.red);

        //wall
        collisionWithWall = Physics2D.Linecast(lookingForWallStart.position, lookingForWallEnd.position, 1 << 8);
        Debug.DrawLine(lookingForWallStart.position, lookingForWallEnd.position, Color.blue);

        collisionWithWall = Physics2D.Linecast(lookingForWallStart.position, lookingForWallEnd2.position, 1 << 8);
        Debug.DrawLine(lookingForWallStart.position, lookingForWallEnd2.position, Color.blue);

        //Nothingness
        collisionWithNothingness = Physics2D.Linecast(lookingForGapStart.position, lookingForGapEnd.position, 1 << 8);
        Debug.DrawLine(lookingForGapStart.position, lookingForGapEnd.position, Color.green);

        collisionWithNothingness = Physics2D.Linecast(lookingForGapStart.position, lookingForGapEnd2.position, 1 << 8);
        Debug.DrawLine(lookingForGapStart.position, lookingForGapEnd2.position, Color.green);

        //Do Something when we dectect collisions-----------------------------------------------------------------------------------------------
        
        // check Collisions with wall or nothingness
        if (collisionWithWall || !collisionWithNothingness) {
            moveRight = !moveRight;
        }

      
        //Collision with player
        if (playerIsNearMe || playerIsNearMe2) {
            speed = 3;
        } else {
            speed = 1;
        }

        if (playerInTerritory) {
            //transform.LookAt(target, Vector2.up);
            //if (target.position.x - transform.localScale.x > 0 && collisionWithNothingness && !collisionWithWall) {
            if (target.position.x > transform.position.x && collisionWithNothingness && !collisionWithWall) {
                moveRight = true;
            }
            //if (target.localScale.x - transform.localScale.x < 0 && collisionWithNothingness && !collisionWithWall) {
            if (target.position.x < transform.position.x && collisionWithNothingness && !collisionWithWall) {
                moveRight = false;
            }
        }

        //Debug.Log("absVelX = " + absVelX);

        if (absVelX > 0) {
            animator.SetBool("walkTime", true);
            animator.SetBool("runTime", false);
            animator.SetBool("standTime", false);
        } else if (absVelX > 1) {
            animator.SetBool("walkTime", false);
            animator.SetBool("runTime", true);
            animator.SetBool("standTime", false);
        } else {
            animator.SetBool("walkTime", false);
            animator.SetBool("runTime", false);
            animator.SetBool("standTime", true);
        }


	}

    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerInTerritory = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerInTerritory = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player" && playerManager.ComboStart) {
            speed = 0;
            Debug.Log("I am getting Hit!");
            animator.SetTrigger("beingHit");
        }

        if(collision.gameObject.tag == "SubZeroPower") {

        }

    }

}

/*public class EnemyTerritory : MonoBehaviour
 {
         public BoxCollider territory;
         GameObject player;
         bool playerInTerritory;
 
         public GameObject enemy;
         BasicEnemy basicenemy;
 
         // Use this for initialization
         void Start ()
         {
             player = GameObject.FindGameObjectWithTag ("Player");
             basicenemy = enemy.GetComponent <BasicEnemy> ();
             playerInTerritory = false;
         }
     
         // Update is called once per frame
         void Update ()
         {
             if (playerInTerritory = true)
             {
                 basicenemy.MoveToPlayer ();
             }
 
             if (playerInTerritory = false)
             {
                 basicenemy.Rest ();
             }
         }
 
         void OnTriggerEnter (Collider other)
         {
             if (other.gameObject == player)
             {
                 playerInTerritory = true;
             }
         }
     
         void OnTriggerExit (Collider other)
         {
             if (other.gameObject == player) 
             {
                     playerInTerritory = false;
             }
         }
 }
 
 public class BasicEnemy : MonoBehaviour
 {
         public Transform target;
         public float speed = 3f;
         public float attack1Range = 1f;
         public int attack1Damage = 1;
         public float timeBetweenAttacks;
 
 
         // Use this for initialization
         void Start ()
         {
             Rest ();
         }
     
         // Update is called once per frame
         void Update ()
         {
             
         }
 
         public void MoveToPlayer ()
         {
             //rotate to look at player
             transform.LookAt (target.position);
             transform.Rotate (new Vector3 (0, -90, 0), Space.Self);
         
             //move towards player
             if (Vector3.Distance (transform.position, target.position) > attack1Range) 
             {
                     transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
             }
         }
 
         public void Rest ()
         {
 
         }
 }
*/