using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftVanish : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.x > 20) {
            Destroy(gameObject);
        }
	}
}
