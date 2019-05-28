using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseMotaro : MonoBehaviour {

    public GameObject gate;

    private Animator animator;
    private Rigidbody2D rb2d;
    [SerializeField] private float time;

    [Range(0, 10)]
    public int speed;
    private bool moveRight;

    public Transform lookingForPlayerStart, lookingForNothingness, lookingForNothingness2;
    public bool collisionWithNothingness;

    //public GameObject[] bonesPrefabs;

    [SerializeField] private int playerHitMe;
    //[SerializeField] private int TotalPlayerHitMe;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gate = GameObject.FindGameObjectWithTag("gate");
    }
	
	// Update is called once per frame
	void Update () {
 
        if (moveRight) {
            transform.localScale = new Vector3(1f, 1f, 1f);
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        } else {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        }

        collisionWithNothingness = Physics2D.Linecast(lookingForPlayerStart.position, lookingForNothingness.position, 1 << 8);
        Debug.DrawLine(lookingForPlayerStart.position, lookingForNothingness.position, Color.green);

        collisionWithNothingness = Physics2D.Linecast(lookingForPlayerStart.position, lookingForNothingness2.position, 1 << 8);
        Debug.DrawLine(lookingForPlayerStart.position, lookingForNothingness2.position, Color.green);

        if (!collisionWithNothingness) {
            moveRight = !moveRight;
        }

        if (playerHitMe == 4) {
            speed = 5;
        }

        if (playerHitMe == 8) {
            speed = 7;
        }

        if (playerHitMe >= 10) {
            //StartCoroutine(WaitExample2());
            animator.SetTrigger("die");
            gate.SetActive(true);
            rb2d.velocity = Vector2.zero;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            speed = 0;
        }

    }

    IEnumerator WaitExample2() {

        yield return new WaitForSeconds(1f);
       
        //nDestroy();
        //StartCoroutine(WaitExample3());

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "SubZeroBullet") {
            animator.SetBool("beingHit", true);
            //animator.SetTrigger("hit");
            playerHitMe++;
            //TotalPlayerHitMe++;
        }

        if (playerHitMe >= 7) {
            rb2d.velocity = Vector2.zero;
            //animator.SetBool("dizzyTime", true);
            //animator.SetBool("spin", false);
            animator.SetBool("beingHit", false);
            //dieTime = true;
        }

    }

    private void NoGettingHit() {
        animator.SetBool("beingHit", false);
    }

    private void NoStart() {
        animator.SetBool("start", false);
    }

    private void OnDestroy() {
        Destroy(gameObject);
    }


}
