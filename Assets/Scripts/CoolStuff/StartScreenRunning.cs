using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenRunning : MonoBehaviour {
    [Range(1, 10)]
    public float runSpeed;

    private Rigidbody2D rb2d;
    public bool rightSide;

    private AudioSource audioSouce;
    public AudioClip[] names;

    private Animator animator;
    private Vector2 speed;
    private bool funStarted;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        audioSouce = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {

        if (rightSide && !funStarted)
            rb2d.velocity = new Vector2(runSpeed, 0);

        if (!rightSide && !funStarted)
            rb2d.velocity = new Vector2(-runSpeed, 0);

        if (rb2d.position.x > 70)
            rb2d.position = new Vector2(Random.Range(-68f, -12f), -4f);

        if (rb2d.position.x < -70)
            rb2d.position = new Vector2(Random.Range(68f, 12f), -4f);

        if (rb2d.position.x > 80f || rb2d.position.x < -80f)
            GameObject.DestroyObject(gameObject);

    }

    private void OnMouseDown() {
        if(gameObject.name == "Goro") {
            audioSouce.PlayOneShot(names[0]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if(gameObject.name == "Kabal") {
            audioSouce.PlayOneShot(names[1]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "Kano") {
            audioSouce.PlayOneShot(names[2]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "Mileena") {
            audioSouce.PlayOneShot(names[3]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "Motaro") {
            audioSouce.PlayOneShot(names[4]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "Reptile") {
            audioSouce.PlayOneShot(names[5]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "ShangTsung") {
            audioSouce.PlayOneShot(names[6]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "Sindel") {
            audioSouce.PlayOneShot(names[7]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "NoobSiabot") {
            audioSouce.PlayOneShot(names[8]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "Baraka") {
            audioSouce.PlayOneShot(names[9]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "Jacks") {
            audioSouce.PlayOneShot(names[11]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "KungLao") {
            audioSouce.PlayOneShot(names[12]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "LuiKang") {
            audioSouce.PlayOneShot(names[13]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "NightWolf") {
            audioSouce.PlayOneShot(names[14]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "Scorpion") {
            audioSouce.PlayOneShot(names[15]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "Sector") {
            audioSouce.PlayOneShot(names[16]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "Smoke") {
            audioSouce.PlayOneShot(names[17]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else if (gameObject.name == "Stryker") {
            audioSouce.PlayOneShot(names[18]);
            speed = rb2d.velocity;
            funStarted = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool("trick", true);
        } else {
            audioSouce.PlayOneShot(names[10]);
        }
    }

    public void AnimationDone() {
        rb2d.velocity = speed;
        animator.SetBool("trick", false);
        funStarted = false;
    }
}
