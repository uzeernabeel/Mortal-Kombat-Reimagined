using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lift : MonoBehaviour {

    private CapsuleCollider2D playerCollider;
    [SerializeField] private BoxCollider2D platformCollider;
    [SerializeField] private BoxCollider2D platformTrigger;

    // Use this for initialization
    void Start () {

        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>();
        Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);

	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
        }
    }

    
}
