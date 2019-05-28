using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenManager : MonoBehaviour {

    public GameObject CreditScreen;
    public GameObject StartStuff;
    public GameObject Stages;
    public GameObject Players;
    public GameObject QuitScreen;

    private AudioSource audioSource;
    public AudioClip Laugh;

	// Use this for initialization
	void Start () {
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log("randNum : " + Random.Range(1, 4));

        if (Input.GetKey(KeyCode.Escape)) {
            QuitScreen.SetActive(true);
            Stages.SetActive(false);
            //Players.SetActive(false);
            StartStuff.SetActive(false);
            CreditScreen.SetActive(false);
        }
		
	}

    public void NoButtonSelected() {
        QuitScreen.SetActive(false);
        StartStuff.SetActive(true);
        //Time.timeScale = 1;
    }

    public void YesButtonSelected() {
        Application.Quit();
    }

    public void CreditScreenOn() {
        Stages.SetActive(false);
        Players.SetActive(false);
        StartStuff.SetActive(false);
        CreditScreen.SetActive(true);
    }

    public void CreditScreenOff() {
        Stages.SetActive(false);
        Players.SetActive(true);
        StartStuff.SetActive(true);
        CreditScreen.SetActive(false);
    }

    public void StartStuffOn() {
        StartStuff.SetActive(true);
    }

    public void StartStuffOff() {
        StartStuff.SetActive(false);
    }

    public void StagesOn() {
        Stages.SetActive(true);
        Players.SetActive(false);
        StartStuff.SetActive(false);
        CreditScreen.SetActive(false);

        //BackgroundMusic.SetActive(false);
        //AudioSource.PlayClipAtPoint(Laugh, transform.position);
        audioSource.PlayOneShot(Laugh);
        
    }

    public void StagesOff() {
        Stages.SetActive(false);
        Players.SetActive(true);
        StartStuff.SetActive(true);
    }

    public void PlayersOn() {
        Players.SetActive(true);
    }

    public void PlayersOff() {
        Players.SetActive(false);
    }
}
