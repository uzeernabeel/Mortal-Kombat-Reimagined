using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrutalityPowerBehaviour : MonoBehaviour {

    public bool cantGetMore;
    private Text brutalityText;
    private AudioSource audioSource;
    public AudioClip brutalityCollectSound;
    private GameObject brutalityButton;

    // Use this for initialization
    void Start () {
        brutalityText = GameObject.FindGameObjectWithTag("BrutalityText").GetComponent<Text>();
        cantGetMore = false;
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        brutalityButton = GameObject.FindGameObjectWithTag("BrutalityButton");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (!cantGetMore) {
                GameController.instance.brutalites++;
                brutalityButton.GetComponent<Image>().color = new Color(1, 0, 0, 1);
                brutalityText.text = GameController.instance.brutalites.ToString();
                audioSource.PlayOneShot(brutalityCollectSound, 0.75f);
                cantGetMore = true;
            }
            OnDestroy();
        }
    }

    private void OnDestroy() {
        Destroy(gameObject);
    }
}
