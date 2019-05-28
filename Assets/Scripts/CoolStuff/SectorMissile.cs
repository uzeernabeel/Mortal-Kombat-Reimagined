using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorMissile : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rb2d;
    private Vector3 position;
    private BoxCollider2D box2d;
    private Vector2 size2d;

    private void Awake() {
        position = transform.position;
    }

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb2d.velocity = new Vector2(0, -5f);
        box2d = GetComponent<BoxCollider2D>();
        size2d = box2d.size;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*private void OnCollisionEnter2D(Collision2D collision) {
        animator.SetBool("Blast", true);
        rb2d.velocity = Vector2.zero;
        StartCoroutine(WaitExample());
    }*/

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player" || collision.gameObject.layer == 8) {
            animator.SetBool("Blast", true);
            box2d.size = Vector2.zero;
            rb2d.velocity = Vector2.zero;
            StartCoroutine(WaitExample());
        }
        //AfterBlast();
    }


    IEnumerator WaitExample() {

        yield return new WaitForSeconds(1f);

        rb2d.position = position;
        animator.SetBool("Blast", false);
        rb2d.velocity = new Vector2(0, -5f);
        box2d.size = size2d;
    }

    void AfterBlast() {
        rb2d.position = position;
        animator.SetBool("Blast", false);
        rb2d.velocity = new Vector2(0, -5f);
        box2d.size = size2d;
    }

    void NoBox2d() {
        box2d.size = Vector2.zero;
    }

}
