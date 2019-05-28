using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint3 : MonoBehaviour {

    //private GameObject gameController;
    //private GameController gameController;

    private void Start() {
        //gameController = GameObject.FindGameObjectWithTag("GameController"); 
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {
            transform.GetChild(0).gameObject.SetActive(true);
            //GameController.instance.checkpoint3 = true;
        }

    }

}
