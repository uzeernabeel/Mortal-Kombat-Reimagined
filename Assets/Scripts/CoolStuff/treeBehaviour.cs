using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeBehaviour : MonoBehaviour {

    private Animator animator;
    public AudioClip touchSound;
    private AudioSource audioSource;

    private bool touched;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            animator.SetBool("playerTouched", true);
            if (!touched) {
                audioSource.PlayOneShot(touchSound);
                touched = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            animator.SetBool("playerTouched", false);
            touched = false;
        }
    }
}
