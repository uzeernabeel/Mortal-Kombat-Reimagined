using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Jump : AbstractBehaviors {

    public float jumpSpeed = 20f;
    public float jumpDelay = .1f;
    public int jumpCount = 2;
    public GameObject dustEffectPrefab;

    protected float lastJumpTime = 0;
    protected int jumpsRemaining = 0;

    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip jumpSnowSound;

    // Use this for initialization
    void Start () {
        audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        //var canJump = inputState.GetButtonValue(inputButtons[0]);
        //var holdTime = inputState.GetButtonHoldTime(inputButtons[0]);

#if UNITY_EDITOR
        var canJump = Input.GetButtonDown("Jump");
#else
        var canJump = CrossPlatformInputManager.GetButtonDown("Jump");
#endif
        var holdTime = inputState.GetButtonHoldTime(inputButtons[0]);


        if (collisionState.standing) {
            if (canJump && holdTime < .1f) {
                jumpsRemaining = jumpCount - 1;
                OnJump();
            }
        } else {
            if (canJump && holdTime < .1f && Time.time - lastJumpTime > jumpDelay) {
                if (jumpsRemaining > 0) {
                    OnJump();
                    jumpsRemaining--;
                    audioSource.PlayOneShot(jumpSnowSound);
                    var clone = Instantiate(dustEffectPrefab);
                    clone.transform.position = transform.position;
                }
            }
        }
	}

    protected virtual void OnJump() {
        audioSource.PlayOneShot(jumpSound);
        var vel = body2d.velocity;
        lastJumpTime = Time.time;
        body2d.velocity = new Vector2(vel.x, jumpSpeed);
    }
}
