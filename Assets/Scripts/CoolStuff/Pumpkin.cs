using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour {

    //private PlayerManager playerManager;
    private Animator animator;
    [SerializeField]private GameObject Number;
    public bool smashed;
    private Vector3 position;
    private bool cantGetMore;
   
    private CircleCollider2D circle2d;
    private Rigidbody2D rb2d;

    private AudioSource audioSource;
    public AudioClip pumpkinSmashSound;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        //playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        cantGetMore = false;
        circle2d = GetComponent<CircleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (smashed) {
            animator.SetTrigger("PumpkinSmashed");
        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "SubZeroBullet") { 
            smashed = true;
            Instantiate(Number, position, Quaternion.identity);
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            circle2d.isTrigger = true;
            //circle2d.radius = 0;
            if (!cantGetMore) {
                GameController.instance.pumpkins++;
                audioSource.PlayOneShot(pumpkinSmashSound);
                cantGetMore = true;
            }
        }
    }

    private void OnDestroy() {
        Destroy(gameObject);
    }
}
