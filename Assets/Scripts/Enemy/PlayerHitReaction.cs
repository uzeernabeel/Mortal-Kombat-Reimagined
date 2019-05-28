using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitReaction : MonoBehaviour {

    //needed components!
    private Animator animator;
    private CapsuleCollider2D collider2d;
    private SpriteRenderer renderer2d;
    private Rigidbody2D rb2d;
    private PlayerManager playerManager;
    public GameObject[] bonesPrefabs;

    //time variables
    //private float time = 0;
    private float dizzyTime = 0;
    private bool gotHitFirstTime;
    private bool gotHitSecondTime;
    //private Vector2 enemyVelocity;

    // Use this for initialization
    void Start () {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        animator = GetComponent<Animator>();
        collider2d = GetComponent<CapsuleCollider2D>();
        renderer2d = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        //enemyVelocity = rb2d.velocity;
    }
	
	// Update is called once per frame
	void Update () {

        if (gotHitFirstTime) {
            dizzyTime += Time.deltaTime;
            if (gotHitSecondTime)
                animator.SetBool("dizzyTime", false);
            else {
                animator.SetBool("dizzyTime", true);
                rb2d.velocity = Vector2.zero;
            }
        }

        if (dizzyTime > 5f) {
            dizzyTime = 0;
            animator.SetBool("dizzyTime", false);
            //rb2d.velocity = enemyVelocity;
            gotHitFirstTime = false;
        }

        if (gotHitSecondTime) {
            animator.SetTrigger("die");
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Player" && playerManager.ComboStart) {
            //rb2d.bodyType = RigidbodyType2D.Static;
            Debug.Log("Kabal is getting Hit!");
            animator.SetTrigger("beingHit");
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

        Invoke("Enabler", 5f);
    }

    void Enabler() {
        Destroy(gameObject);
    }

    private void OnDestroy() {
        Destroy(gameObject);
    }
}
