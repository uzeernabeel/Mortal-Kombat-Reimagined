using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float walkspeed = 10f;
    public float runSpeed = 20f;
    private Rigidbody2D rb2d;
    public float x;
    public float y;

    //private LookForward lf;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        //lf = GetComponent<LookForward>();
	}
	
	// Update is called once per frame
	void Update () {

        x = transform.localScale.x;
        y = transform.localScale.y;

        rb2d.velocity = new Vector2(gameObject.transform.localScale.x, 0) * walkspeed;
        
	}
}
