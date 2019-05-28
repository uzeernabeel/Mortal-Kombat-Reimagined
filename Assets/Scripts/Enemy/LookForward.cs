using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForward : MonoBehaviour {

    public Transform sightStart, sightEnd;
    public bool collision;
    public string layer = "Solid";
    public bool needsCollision = true;
    
    public float walkspeed = 1f;
    public float runSpeed = 2f;
    private Rigidbody2D rb2d;
    private SpriteRenderer renderer2d;

    public bool OK = false;
    

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        renderer2d = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        //collision = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer(layer));
        collision = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << 8);

        //needsCollision = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << 8);

        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);


        //speeding the direction enemy is facing
        if(renderer2d.flipX == false)
            rb2d.velocity = new Vector2(gameObject.transform.localScale.x, 0) * walkspeed;
        else
            rb2d.velocity = new Vector2(-gameObject.transform.localScale.x, 0) * walkspeed;



        if (collision) {

            //facing the direction of the speed
            if (renderer2d.flipX == false) {
                renderer2d.flipX = true;
            } else {
                renderer2d.flipX = false;
            }

            if(rb2d.velocity.x < 0) {
                rb2d.velocity = new Vector2(gameObject.transform.localScale.x, 0) * walkspeed;
            } else {
            rb2d.velocity = new Vector2(-gameObject.transform.localScale.x, 0) * walkspeed;
            }   
    

            //renderer2d.flipX == true ? fasle : true;

            /*if(transform.localScale.x < 0) {
                rb2d.velocity = new Vector2(gameObject.transform.localScale.x, 0) * walkspeed;
                localScaleX = 0.65f;
                
            } else {
                Debug.Log("I am here");
                rb2d.velocity = new Vector2(gameObject.transform.localScale.x, 0) * -walkspeed;
                localScaleX = -0.65f;
            }*/

            //transform.localScale = new Vector3(transform.localScale.x == 0.65f ? -0.65f : 0.65f, transform.localScale.y, 1);
            //transform.localScale += new Vector3(1, 1, 1);

            //transform.localScale += new Vector3(localScaleX, transform.localScale.y, 1);

        } 
    }

    public void ChangeSpeedFaster() {
        OK = true;
        walkspeed = 2f;
    }

    public void ChangeSpeedSlower() {
        OK = false;
        walkspeed = 1f;
    }
}
