using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarakaBullet : MonoBehaviour {

    //public float offset = 0.1f;
    //public GameObject baraka;
    //private GameObject baraka;
    private float initialVelocity;
    private float startVelX;
    private Rigidbody2D rb2d;
    private BoxCollider2D box2d;
    public bool facingLeft = true;

    private void Awake() {
        //baraka = GameObject.FindGameObjectWithTag("BarakaPower");
        rb2d = GetComponent<Rigidbody2D>();
        box2d = GetComponent<BoxCollider2D>();
        initialVelocity = 7;
        startVelX = 0;
    }

    // Use this for initialization
    void Start() {

        //if (baraka != null) {
            /*if (baraka.transform.localScale.x == 1) {
                startVelX = -initialVelocity * baraka.transform.localScale.x;
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                //Debug.Log("I am ending up here");
            } else {
                startVelX = -initialVelocity * baraka.transform.localScale.x;
                //Debug.Log("I am ending up here 2");
            }
        } else {
            //Debug.Log("I am ending up here");
            startVelX = 0;
        }*/

            //startVelX = initialVelocity * baraka.transform.localScale.x;

            if (facingLeft) {
                startVelX = -initialVelocity;
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            } else {
                startVelX = initialVelocity;
            }

            rb2d.velocity = new Vector2(startVelX, rb2d.velocity.y);

        //}

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            OnDestroy();
        }
    }

    private void OnDestroy() {
        Destroy(gameObject);
    }

    private void SpeedZero() {
        rb2d.velocity = new Vector2(0, 0);
        box2d.size = new Vector2(0, 0);
    }

    private void WasZero() {
        box2d.size = new Vector2(0.37f, 0.37f);
    }

    private void IsZero() {
        box2d.size = new Vector2(0, 0f);
    }
}

