using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPowerDelay2 : MonoBehaviour {

    public float startDelay = 0.5f;
    Animator animator;

    // Use this for initialization
    void Start () {

        animator = GetComponent<Animator>();
        
        //StartCoroutine(DelayedAnimation());
 
        animator.Play("PowerAnimation");
        //animation.Play("Put Your Animation name here ");

    }

    // The delay coroutine
    IEnumerator DelayedAnimation() {
        yield return new WaitForSeconds(startDelay);
        animator.Play("PowerAnimation");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
