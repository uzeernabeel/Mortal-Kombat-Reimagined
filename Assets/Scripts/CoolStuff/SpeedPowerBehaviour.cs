using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedPowerBehaviour : MonoBehaviour {

    public bool cantGetMore;
    private Text speedText;
    private AudioSource audioSource;
    public AudioClip speedCollectSound;
    private GameObject SpeedButton;

    // Use this for initialization
    void Start() {
        speedText = GameObject.FindGameObjectWithTag("SpeedText").GetComponent<Text>();
        cantGetMore = false;
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        SpeedButton = GameObject.FindGameObjectWithTag("SpeedButton");
        SpeedButton.GetComponent<Image>().color = new Color(1, 1, 0, 0.25f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (!cantGetMore) {
                GameController.instance.SpeedPower += 5;
                SpeedButton.GetComponent<Image>().color = new Color(1, 1, 0, 1);
                speedText.text = GameController.instance.SpeedPower.ToString();
                audioSource.PlayOneShot(speedCollectSound, 0.75f);
                cantGetMore = true;
            }
            OnDestroy();
        }
    }

    private void OnDestroy() {
        Destroy(gameObject);
    }

}
