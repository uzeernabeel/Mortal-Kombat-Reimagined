using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionBringBack : MonoBehaviour {

    private GameObject Player;
    //public GameObject fire;
    private Animator animator;
    //public float time;
    //public GameObject funnyNote1;
    //public GameObject funnyNote2;
    //public GameObject funnyNote3;
    public AudioClip getOverHereSound;
    private AudioSource audioSource;
    private GameObject sign;
 
    // Use this for initialization
    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
        //fire = (GameObject)("ScorpionFire");
        animator = GetComponent<Animator>();
        //time = 0f;

        //waiting for 3 seconds
        StartCoroutine(WaitExample());

        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();

        //Player.transform.position = new Vector3(transform.position.x + 3, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update() {
        
       

        /*time += Time.deltaTime;
        
        //if (time > 2) {
            animator.SetFloat("ScorpionState", 1);
            var temp = GameObject.Instantiate(fire);
            temp.transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
            Player.transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
        //}*/
    }

    IEnumerator WaitExample() {

        yield return new WaitForSeconds(1f);

        //after 3 seconds
        //Player.SetActive(true);
        Player.GetComponent<SpriteRenderer>().enabled = true;
        animator.SetInteger("ScorpionState", 1);

        var num = Random.Range(1, 4);
        sign = (GameObject)Instantiate(Resources.Load("scorpionSign" + num));
        sign.transform.position = new Vector3(transform.position.x + 4.16f, transform.position.y + 3.32f, 0);
        var fire = (GameObject)Instantiate(Resources.Load("ScorpionFire"));
        audioSource.PlayOneShot(getOverHereSound);
        fire.transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
        //temp.transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
        Player.transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
        //Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        GameController.instance.health = PlayerPrefs.GetInt("MaxHealth");
        StartCoroutine(WaitExample2());

    }

    IEnumerator WaitExample2() {

        yield return new WaitForSeconds(2f);
        animator.SetInteger("ScorpionState", 2);

        StartCoroutine(WaitExample3());

    }

    IEnumerator WaitExample3() {

        yield return new WaitForSeconds(2f);

        animator.SetInteger("ScorpionState", 3);
        var fire = (GameObject)Instantiate(Resources.Load("ScorpionFire"));
        fire.transform.position = new Vector3(transform.position.x - .1f, transform.position.y, 0);
        Destroy(sign);

    }

}
