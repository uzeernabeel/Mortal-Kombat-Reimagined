using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesVelocity : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float velocityX;
    public float velocityY;

    private float time;
    public float destroyTime = 3;
    //private GameObject player;

	// Use this for initialization
	void Start () {
        //player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(velocityX, velocityY);
	}

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void Update() {
        time += Time.deltaTime;

        if(time > destroyTime) {
            Destroy(gameObject);
        }
    }


}
