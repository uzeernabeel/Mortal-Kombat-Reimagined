using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossActionType {
    Idle,
    Walking,
    Running,
    Power,
    Jumping,
    Tired,
    Victory,
    Dead
}

public class Sheeva2 : MonoBehaviour {

    /*private BossActionType currentState = BossActionType.Idle;

    private float bulletTime = 1.65f;
    private int TotalPlayerHitMe;
    private int playerHitMe;

    public float time;
    public GameObject gate;

    public GameObject sheevaBulletPrefab;
    private Vector2 sheevaBulletPosition;
    private Vector3 fallingPosition;

    private Animator animator;
    private Rigidbody2D rb2d;
    private GameObject player;
    private SpriteRenderer renderer2d;
    private CapsuleCollider2D collider2d;
    private PlayerManager playerManager;

    public int upDown;
    public bool moveRight;
    public bool moveUp;
    private float speed = 5f;    //speed of the enemy Walking or Running.

    //bools to keep track of collision
    public bool playerIsNearMe;
    public bool playerIsNearMe2;

    public Transform lookingForPlayerStart, lookingForPlayerEnd, lookingForPlayerEnd2, lookingForNothingness, lookingForNothingness2;

    public bool collisionWithNothingness;
    [SerializeField] private bool comboingNow;
    public GameObject[] bonesPrefabs;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        renderer2d = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<CapsuleCollider2D>();
        playerHitMe = 0;
        time = 0;
        upDown = 0;
    }
	
	// Update is called once per frame
	void Update () {

        switch (currentState) {

            case BossActionType.Idle:
                HandleIdleState();
                break;

            case BossActionType.Walking:
                HandleWalkingState();
                break;

            case BossActionType.Running:
                HandleRunningState();
                break;

            case BossActionType.Power:
                HandlePowerState();
                break;

            case BossActionType.Jumping:
                HandleJumpingState();
                break;

            case BossActionType.Tired:
                HandleTiredState();
                break;

            case BossActionType.Victory:
                HandleVictoryState();
                break;

            case BossActionType.Dead:
                HandleDeadState();
                break;

        }




    }

    private void HandleDeadState() {
        
    }

    private void HandleVictoryState() {
        throw new NotImplementedException();
    }

    private void HandleTiredState() {
        throw new NotImplementedException();
    }

    private void HandleJumpingState() {
        throw new NotImplementedException();
    }

    private void HandlePowerState() {
        throw new NotImplementedException();
    }

    private void HandleRunningState() {
        throw new NotImplementedException();
    }

    private void HandleWalkingState() {
        throw new NotImplementedException();
    }

    private void HandleIdleState() {
        throw new NotImplementedException();
    }*/
}
