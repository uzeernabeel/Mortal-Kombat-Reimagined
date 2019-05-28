using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsLoad : MonoBehaviour {

    private void Awake() {

        PlayerPrefs.SetInt("Level1Enemies", 15);
        PlayerPrefs.SetInt("Level2Enemies", 15);
        PlayerPrefs.SetInt("Level3Enemies", 15);
        PlayerPrefs.SetInt("Level4Enemies", 15);
        PlayerPrefs.SetInt("Level5Enemies", 15);
        PlayerPrefs.SetInt("Level6Enemies", 15);
        PlayerPrefs.SetInt("Level7Enemies", 15);
        PlayerPrefs.SetInt("Level8Enemies", 15);
        PlayerPrefs.SetInt("Level9Enemies", 15);

        PlayerPrefs.SetInt("Level1Coins", 25);
        PlayerPrefs.SetInt("Level2Coins", 25);
        PlayerPrefs.SetInt("Level3Coins", 25);
        PlayerPrefs.SetInt("Level4Coins", 25);
        PlayerPrefs.SetInt("Level5Coins", 25);
        PlayerPrefs.SetInt("Level6Coins", 25);
        PlayerPrefs.SetInt("Level7Coins", 25);
        PlayerPrefs.SetInt("Level8Coins", 25);
        PlayerPrefs.SetInt("Level9Coins", 25);

        PlayerPrefs.SetInt("Level1Pumpkins", 4);
        PlayerPrefs.SetInt("Level2Pumpkins", 4);
        PlayerPrefs.SetInt("Level3Pumpkins", 4);
        PlayerPrefs.SetInt("Level4Pumpkins", 4);
        PlayerPrefs.SetInt("Level5Pumpkins", 4);
        PlayerPrefs.SetInt("Level6Pumpkins", 4);
        PlayerPrefs.SetInt("Level7Pumpkins", 4);
        PlayerPrefs.SetInt("Level8Pumpkins", 4);
        PlayerPrefs.SetInt("Level9Pumpkins", 4);

        /*for (int i = 1; i < 10; i++) {
            PlayerPrefs.SetInt("Level" + i + "Dolls", 3);
        }*/

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
