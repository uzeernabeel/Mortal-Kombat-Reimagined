using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WallJump : AbstractBehaviors {

    public Vector2 jumpVelcity = new Vector2(1.5f, 3f);
    public bool jumpingOffWall;
    public float resetDelay = 0.2f;

    //public bool slide;

    private float timeElapsed = 0;

    private AudioSource audioSource;
    public AudioClip jumpSound;

    private void Start() {
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        //slide = GameObject.FindGameObjectWithTag("Player").GetComponent<WallSlide>().slidingDownTheWall;
        if(!collisionState.standing && collisionState.onWall) {
        //if (!collisionState.standing && slide) { 
#if UNITY_EDITOR
            var canJump = inputState.GetButtonValue(inputButtons[0]);
#else
            var canJump = CrossPlatformInputManager.GetButtonDown("Jump");
#endif

            if (canJump) {
                inputState.direction = inputState.direction == Directions.Right ? Directions.Left : Directions.Right;
                audioSource.PlayOneShot(jumpSound);
                body2d.velocity = new Vector2(jumpVelcity.x * (float)inputState.direction, jumpVelcity.y);

                ToggleScripts(false);
                jumpingOffWall = true;
            }
        }

        if (jumpingOffWall) {
            timeElapsed += Time.deltaTime;

            if(timeElapsed > resetDelay) {
                ToggleScripts(true);
                jumpingOffWall = false;
                timeElapsed = 0f;
            }
        }
	}
}
