using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPower : MonoBehaviour {

    //public float itsTime;

    public float initialVelocity = 20;

    public float offset = 0.1f;
    public bool right;
    public Animator animator;
    private GameObject player;
    private Rigidbody2D rb2d;
    private BoxCollider2D box2d;

    private float startVelX;

    [SerializeField]private float time_of_end;

    private AudioSource audioSource;
    public AudioClip playerPowerTouched;


    private void Awake() {
        
        // player = GetComponent<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        box2d = GetComponent<BoxCollider2D>();
    }

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        if (player != null) {
            if (player.transform.localScale.x > 0) {
                 startVelX = initialVelocity * transform.localScale.x;
            } else {
                 startVelX = initialVelocity * (- transform.localScale.x);
                transform.localScale = new Vector3(-.611f, transform.localScale.y, transform.localScale.z);
            }
        }

        rb2d.velocity = new Vector2(startVelX, rb2d.velocity.y);

    }
	
	// Update is called once per frame
	void Update () {
        time_of_end += Time.deltaTime;
        //power coming out refrece to player direction

        if(time_of_end > 1f) {
            time_of_end = 0;
            animator.SetTrigger("touched");
        }
        

        //if (player.GetComponent<InputState>().direction == Directions.Right)
            //gameObject.transform.position = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
        //else
            //gameObject.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            //gameObject.transform.position = new Vector3(transform.position.x - offset, transform.position.y, transform.position.z);

        //if(transform.position.x > camera.)
    }

    private void OnDestroy() {
        Destroy(gameObject);
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    void SpeedZero() {
        rb2d.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Pumpkins") {
            //rb2d.velocity = Vector2.zero;
            audioSource.PlayOneShot(playerPowerTouched, 0.75f);
            animator.SetTrigger("touched");
            box2d.size = Vector2.zero;
        }
        //Destroy(gameObject);
    }


}
