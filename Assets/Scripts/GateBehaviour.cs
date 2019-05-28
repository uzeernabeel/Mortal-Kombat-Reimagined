using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateBehaviour : MonoBehaviour {

    private GameObject player;
    private Rigidbody2D rb2d;
    private bool touched;

    public GameObject endStageCanvas;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = player.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (touched) {
            
            if (player.transform.position.x < transform.position.x) {
                //Debug.Log("been there done that");
                rb2d.velocity = new Vector2(15f, rb2d.velocity.y);
            }

            if (player.transform.position.x >= transform.position.x) {
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                //Debug.Log("been there done that : 2");
                player.GetComponent<Animator>().SetBool("win", true);
                rb2d.velocity = Vector2.zero;
                touched = false;
                //GameController.instance.stageDone = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            touched = true;

            if (GameObject.Find("MobileSingleStickControl").activeSelf == true)
                GameObject.Find("MobileSingleStickControl").SetActive(false);
            if (GameObject.Find("MobileAircraftControls").activeSelf == true)
                GameObject.Find("MobileAircraftControls").SetActive(false);

            GameController.instance.stageDone = true;
            
            StartCoroutine(WaitExample2());

            PlayerPrefs.SetInt("Stage" + (GameController.instance.levelNumber + 1), 1);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator WaitExample2() {
        yield return new WaitForSeconds(3f); //after 3 seconds start new stage.
        endStageCanvas.SetActive(true);
    }

    public void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
}
