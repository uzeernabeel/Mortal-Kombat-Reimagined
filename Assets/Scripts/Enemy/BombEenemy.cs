using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEenemy : MonoBehaviour {

    private CircleCollider2D circle2d;
    private BoxCollider2D box2d;
    public bool afterTouched;
    private GameObject Player;
    private SpriteRenderer spRenderer;
    private Animator animator;

	// Use this for initialization
	void Start () {
        circle2d = GetComponent<CircleCollider2D>();
        box2d = GetComponent<BoxCollider2D>();
        afterTouched = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        spRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        //circle2d.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.tag == "Player" && afterTouched) {
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, 10f);
        }

        if (collision.gameObject.tag == "Player") {
            spRenderer.color = new Color(1, 0, 0, 1);
            //gameObject.layer = 11;
            //gameObject.tag = "Enemy";
            //gameObject.layer = LayerMask.NameToLayer("Enemy");
            //box2d.gameObject.SetActive(false);
            box2d.enabled = false;
            StartCoroutine(WaitExample());
        }
    }

    IEnumerator WaitExample() {
        yield return new WaitForSeconds(1f);
        gameObject.tag = "Enemy";
        circle2d.enabled = true;
        afterTouched = true;
        GameController.instance.bombTouched = true;
        spRenderer.color = new Color(1, 1, 1, 1);
        animator.SetTrigger("blast");
    }

    private void OnDestroy() {
        Destroy(gameObject);
    }

    private void Done() {
        circle2d.enabled = false;
    }

}
