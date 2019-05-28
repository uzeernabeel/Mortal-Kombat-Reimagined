using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidGaps : MonoBehaviour {

    public Transform sightStart, sightEnd;
    public string layer = "Solid";
    public bool needsCollision = false;

    public float walkspeed = 10f;
    public float runSpeed = 20f;

    private Rigidbody2D rb2d;
    private SpriteRenderer renderer2d;

    // Use this for initialization
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        renderer2d = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {

        needsCollision = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << 8);
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);

        //speeding the direction enemy is facing
        if (renderer2d.flipX == false)
            rb2d.velocity = new Vector2(gameObject.transform.localScale.x, 0) * walkspeed;
        else
            rb2d.velocity = new Vector2(-gameObject.transform.localScale.x, 0) * walkspeed;

        if (!needsCollision) {

            //facing the direction of the speed
            if (renderer2d.flipX == false) {
                renderer2d.flipX = true;
            } else {
                renderer2d.flipX = false;
            }

            if (rb2d.velocity.x < 0) {
                rb2d.velocity = new Vector2(gameObject.transform.localScale.x, 0) * walkspeed;
            } else {
                rb2d.velocity = new Vector2(-gameObject.transform.localScale.x, 0) * walkspeed;
            }

        }
    }
}
