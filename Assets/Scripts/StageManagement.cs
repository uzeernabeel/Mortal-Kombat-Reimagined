using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManagement : MonoBehaviour {

    public GameObject loadingScreen;

    public GameObject SnowChunck;
    public GameObject DesertChunck;
    public GameObject VolcanoChunck;

    public GameObject snow1;
    public GameObject snow2;
    public GameObject snow3;
    public GameObject snow4;

    public GameObject desert1;
    public GameObject desert2;
    public GameObject desert3;
    public GameObject desert4;

    public GameObject volcano1;
    public GameObject volcano2;
    public GameObject volcano3;
    public GameObject volcano4;

    private void Awake() {
        if(PlayerPrefs.GetInt("Stage2") == 1) {
            snow2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        if (PlayerPrefs.GetInt("Stage3") == 1) {
            snow3.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        if (PlayerPrefs.GetInt("Stage4") == 1) {
            snow4.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        if (PlayerPrefs.GetInt("Stage5") == 1) {
            desert1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        if (PlayerPrefs.GetInt("Stage6") == 1) {
            desert2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        if (PlayerPrefs.GetInt("Stage7") == 1) {
            desert3.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        if (PlayerPrefs.GetInt("Stage8") == 1) {
            desert4.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        if (PlayerPrefs.GetInt("Stage9") == 1) {
            volcano1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        if (PlayerPrefs.GetInt("Stage10") == 1) {
            volcano2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        if (PlayerPrefs.GetInt("Stage11") == 1) {
            volcano3.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        if (PlayerPrefs.GetInt("Stage12") == 1) {
            volcano4.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Snow1Clicked() {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(1);
    }
    public void Snow2Clicked() {
        if (PlayerPrefs.GetInt("Stage2") == 1) {
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(2);
        }
    }
    public void Snow3Clicked() {
        if (PlayerPrefs.GetInt("Stage3") == 1) {
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(3);
        }
    }
    public void Snow4Clicked() {
        if (PlayerPrefs.GetInt("Stage4") == 1) {
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(4);
        }
    }

    public void Desert1Clicked() {
        if (PlayerPrefs.GetInt("Stage5") == 1) {
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(5);
        }
    }
    public void Desert2Clicked() {
        if (PlayerPrefs.GetInt("Stage6") == 1) {
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(6);
        }
    }
    public void Desert3Clicked() {
        if (PlayerPrefs.GetInt("Stage7") == 1) {
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(7);
        }
    }
    public void Desert4Clicked() {
        if (PlayerPrefs.GetInt("Stage8") == 1) {
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(8);
        }
    }

    public void Volcano1Clicked() {
        if (PlayerPrefs.GetInt("Stage9") == 1) {
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(9);
        }
    }
    public void Volcano2Clicked() {
        if (PlayerPrefs.GetInt("Stage10") == 1) {
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(10);
        }
    }
    public void Volcano3Clicked() {
        if (PlayerPrefs.GetInt("Stage11") == 1) {
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(11);
        }
    }
    public void Volcano4Clicked() {
        if (PlayerPrefs.GetInt("Stage12") == 1) {
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(12);
        }
    }

    public void RightButton() {
        if (SnowChunck.activeSelf) {
            SnowChunck.SetActive(false);
            DesertChunck.SetActive(true);
            VolcanoChunck.SetActive(false);
        } else if (DesertChunck.activeSelf) {
            SnowChunck.SetActive(false);
            DesertChunck.SetActive(false);
            VolcanoChunck.SetActive(true);
        } else {
            SnowChunck.SetActive(true);
            DesertChunck.SetActive(false);
            VolcanoChunck.SetActive(false);
        }
    }

    public void LeftButton() {
        if (SnowChunck.activeSelf) {
            SnowChunck.SetActive(false);
            DesertChunck.SetActive(false);
            VolcanoChunck.SetActive(true);
        } else if (DesertChunck.activeSelf) {
            SnowChunck.SetActive(true);
            DesertChunck.SetActive(false);
            VolcanoChunck.SetActive(false);
        } else {
            SnowChunck.SetActive(false);
            DesertChunck.SetActive(true);
            VolcanoChunck.SetActive(false);
        }
    }
}
