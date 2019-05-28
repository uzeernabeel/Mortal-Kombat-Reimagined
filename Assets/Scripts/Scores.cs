using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour {

    private float stageCompletionTime;
    //public int levelNumber;

    public Text timeText;
    public Text timeScore;

    public Text enemiesText;
    public Text enemiesScore;

    public Text coinsText;
    public Text coinsScore;

    public Text pumpkinsText;
    public Text pumpkinsScore;

    public Text dollsText;
    public Text dollsScore;

    public Text totalScore;

    // Use this for initialization
    void Start () {

        stageCompletionTime = 0f;
        //levelNumber = 1;
    }
	
	// Update is called once per frame
	void Update () {
        stageCompletionTime += Time.deltaTime;

        if (GameController.instance.stageDone) {
            AfterStageDoneCalculations();
        }
    }

    private void AfterStageDoneCalculations() {

        GameController.instance.stageDone = false;

        //time calculations
        int minutes = Mathf.FloorToInt(stageCompletionTime / 60f);
        int seconds = Mathf.FloorToInt(stageCompletionTime - minutes * 60);
        String niceTimeString = string.Format("{0:0}:{1:00}", minutes, seconds);
        timeText.text = niceTimeString;

        int total = (minutes * 100) + (seconds * 10);
        timeScore.text = total.ToString();

        //enemies calculations
        //String tempEnemyText = GameController.instance.enemiesKilled + "/" + PlayerPrefs.GetInt("Level1Enemies");
        String temp1 = "Level" + GameController.instance.levelNumber + "Enemies";
        enemiesText.text = GameController.instance.enemiesKilled + "/" + PlayerPrefs.GetInt(temp1);
        //int tempEnemyScore = GameController.instance.enemiesKilled * 500;
        //enemiesScore.text = tempEnemyScore.ToString();
        enemiesScore.text = (GameController.instance.enemiesKilled * 500).ToString();

        //coins calculations
        coinsText.text = GameController.instance.coins + "/" + PlayerPrefs.GetInt("Level" + GameController.instance.levelNumber + "Coins");
        coinsScore.text = (GameController.instance.coins * 100).ToString();

        //pumpkins calculations
        pumpkinsText.text = GameController.instance.pumpkins + "/" + PlayerPrefs.GetInt("Level" + GameController.instance.levelNumber + "Pumpkins");
        pumpkinsScore.text = (GameController.instance.pumpkins * 750).ToString();

        //dolls calculations
        dollsText.text = GameController.instance.dollsCollected + "/3";// + PlayerPrefs.GetInt("Level" + levelNumber + "Dolls");
        dollsScore.text = (GameController.instance.dollsCollected * 1000).ToString();

        //total calculation
        totalScore.text = ((minutes * 100) + (seconds * 10) +
                            (GameController.instance.enemiesKilled * 500) +
                            (GameController.instance.coins * 100) +
                            (GameController.instance.pumpkins * 750) +
                            (GameController.instance.dollsCollected * 1000)
                            ).ToString();
    }
}
