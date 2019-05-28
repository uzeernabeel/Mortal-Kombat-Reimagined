using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour {

    //private Animator animator;
    private Vector3 position;
    [SerializeField] private GameObject point;
    public bool cantGetMore;

    private Text coinsText;

    private AudioSource audioSource;
    public AudioClip coinCollectSound;

	// Use this for initialization
	void Start () {
        coinsText = GameObject.FindGameObjectWithTag("CoinText").GetComponent<Text>();
        position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
        //animator = GetComponent<Animator>();
        cantGetMore = false;
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Instantiate(point, position, Quaternion.identity);
            if (!cantGetMore) {
                GameController.instance.coins++;
                coinsText.text = GameController.instance.coins.ToString();
                audioSource.PlayOneShot(coinCollectSound, 0.5f);
                cantGetMore = true;
            }
            OnDestroy();
        }
    }

    private void OnDestroy() {
        Destroy(gameObject);
    }
}
