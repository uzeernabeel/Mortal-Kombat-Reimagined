using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour { //This Script is gonna act as a Singleton class to manage data between scenes

    public GameObject scorpion;
    public static GameController instance;
    public bool bombTouched;
    private GameObject player; //player reference
    public bool playerDied;

    //CheckPoints of the level
    private Transform playerStartPosition;
    public bool checkpoint1;
    private Transform checkpoint1Position;
    public bool checkpoint4;
    private Transform checkpoint4Position;

    public bool stageDone;
    public int levelNumber;

    //Points tracking 
    public int coins;
    public int brutalites;
    public float SpeedPower;
    public int pumpkins;
    public int enemiesKilled;
    public int dollsCollected;

    //Health tracking
    //[Range(1, 10)]
    //public int setHealth;
    public int health;

    private void Awake() {

        if (instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Use this for initialization
    void Start () {
        checkpoint1 = false;
        checkpoint4 = false;
        bombTouched = false;
        playerStartPosition = player.transform;
        coins = 0;
        health = PlayerPrefs.GetInt("MaxHealth");
        
    }
	
	// Update is called once per frame
	void Update () {

        if (health <= 0) {
            playerDied = true;
            health = PlayerPrefs.GetInt("MaxHealth");
            StartCoroutine(AfterDied());
            //player.SetActive(false);
        }

    }

    IEnumerator AfterDied() {

        yield return new WaitForSeconds(3f);

        if (playerDied) { // if player is dead wake him up at the last checkpoint.
            if (checkpoint4) {
                var clone = Instantiate(scorpion);
                Vector3 pos = new Vector3(checkpoint4Position.position.x + 2f, checkpoint4Position.position.y, checkpoint4Position.position.z);
                clone.transform.position = pos;
                playerDied = false;
                health = PlayerPrefs.GetInt("MaxHealth");
            } else if (checkpoint1) {
                var clone = Instantiate(scorpion);
                Vector3 pos = new Vector3(checkpoint1Position.position.x + 2f, checkpoint1Position.position.y, checkpoint1Position.position.z);
                clone.transform.position = pos;
                playerDied = false;
                health = PlayerPrefs.GetInt("MaxHealth");
            } else {
                var clone = Instantiate(scorpion);
                Vector3 pos = new Vector3(playerStartPosition.position.x + 2f, playerStartPosition.position.y, playerStartPosition.position.z);
                clone.transform.position = pos;
                playerDied = false;
                health = PlayerPrefs.GetInt("MaxHealth");
            }
        }
    }

    public Transform Checkpoint4Position {
        get { return checkpoint4Position; }
        set {checkpoint4Position = value;}
    }

    public Transform Checkpoint1Position {
        get {return checkpoint1Position;}
        set {checkpoint1Position = value;}
    }

    public string FormatTime(float value) {
        int t = (int)value;

        if (value > 999) {
            return String.Format("{0:D4}", t);
        } else if (value > 99) {
            return String.Format("{0:D3}", t);
        } else {
            return String.Format("{0:D2}", t);
        }
        //return value.ToString().
    }

    
}
