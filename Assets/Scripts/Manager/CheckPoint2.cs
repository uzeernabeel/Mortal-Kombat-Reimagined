using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint2 : MonoBehaviour {
    private AudioSource audioSource;
    public AudioClip checkPoint;
    //private GameObject gameController;
    //private GameController gameController;

    private void Start() {
        //gameController = GameObject.FindGameObjectWithTag("GameController"); 
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {
            audioSource.PlayOneShot(checkPoint);
            transform.GetChild(0).gameObject.SetActive(true);
            //GameController.instance.checkpoint2 = true;
        }

    }

}
