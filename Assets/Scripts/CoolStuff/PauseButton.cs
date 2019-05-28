using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

    public Sprite soundOn;
    public Sprite soundOff;
    public Sprite musicOn;
    public Sprite musicOff;

    public GameObject SoundObject;
    public GameObject MusicObject;

    public GameObject canvasStuff;
    public GameObject pauseButton;
    public GameObject redBack;

    private AudioSource BackgroundMusic;
    private AudioSource Sounds;

    public GameObject QuitScreen;
    public GameObject LoadingImage;
    // Use this for initialization
    void Start () {
        BackgroundMusic = GameObject.FindGameObjectWithTag("BackgroundSound").GetComponent<AudioSource>();
        Sounds = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            PauseGame();
        }
    }

    public void PauseGame() {
        canvasStuff.SetActive(true);
        AudioListener.pause = true;
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        redBack.SetActive(true);
    }

    public void ResumeGame() {
        canvasStuff.SetActive(false);
        AudioListener.pause = false;
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        redBack.SetActive(false);
    }

    public void Music() {
        if (BackgroundMusic.mute) {
            BackgroundMusic.mute = false;
            if (MusicObject.GetComponent<Image>().sprite == musicOn) {
                MusicObject.GetComponent<Image>().sprite = musicOff;
            } else {
                MusicObject.GetComponent<Image>().sprite = musicOn;
            }
        } else {
            BackgroundMusic.mute = true;
            if (MusicObject.GetComponent<Image>().sprite == musicOn) {
                MusicObject.GetComponent<Image>().sprite = musicOff;
            } else {
                MusicObject.GetComponent<Image>().sprite = musicOn;
            }
        }
    }

    public void Sound() {
        if (Sounds.mute) {
            Sounds.mute = false;
            if (SoundObject.GetComponent<Image>().sprite == soundOn) {
                SoundObject.GetComponent<Image>().sprite = soundOff;
            } else {
                SoundObject.GetComponent<Image>().sprite = soundOn;
            }
        } else {
            Sounds.mute = true;
            if (SoundObject.GetComponent<Image>().sprite == soundOn) {
                SoundObject.GetComponent<Image>().sprite = soundOff;
            } else {
                SoundObject.GetComponent<Image>().sprite = soundOn;
            }
        }
    }

    public void RestartLevel() {
        Time.timeScale = 1;
        AudioListener.pause = false;
        LoadingImage.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu() {
        Time.timeScale = 1;
        AudioListener.pause = false;
        LoadingImage.SetActive(true);
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        QuitScreen.SetActive(true);
        canvasStuff.SetActive(false);
    }

    public void QuitNoButtonSelected() {
        QuitScreen.SetActive(false);
        canvasStuff.SetActive(true);
    }

    public void QuitYesButtonSelected() {
        LoadingImage.SetActive(true);
        Application.Quit();
    }

}
