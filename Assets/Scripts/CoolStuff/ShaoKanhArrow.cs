using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaoKanhArrow : MonoBehaviour {

    public float initialVelocity;
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

        if (facingLeft) {
            startVelX = -initialVelocity;
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        } else {
            startVelX = initialVelocity;
        }

        rb2d.velocity = new Vector2(startVelX, rb2d.velocity.y);

    }

    private void OnDestroy() {
        Destroy(gameObject);
    }

    private void SpeedZero() {
        rb2d.velocity = new Vector2(0, 0);
        box2d.size = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        OnDestroy();
    }
}
