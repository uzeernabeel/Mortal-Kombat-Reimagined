using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController {

    private GameObject player;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    // Use this for initialization
    void Start () {
        player.GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Kick() {

    }
}
