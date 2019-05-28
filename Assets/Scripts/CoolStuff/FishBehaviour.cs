using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour {

    //private Rigidbody2D rb2d;
    private Vector3 positionA;
    private Vector3 positionB;
    private Vector3 nextPosition;

    //[SerializeField] private Transform childTransform;
    [SerializeField] private Transform transformB;

    [Range(1, 10)]
    public int speed = 2;

    public bool moveUp;

	// Use this for initialization
	void Start () {
        positionA = transform.localPosition;
        positionB = transformB.localPosition;
        nextPosition = positionB;
        moveUp = false;
        //rb2d = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update() {
        Move();
    }

    private void Move() {

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, nextPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, nextPosition) <= 0.1) {
            ChangeDestination();
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void ChangeDestination() {
        nextPosition = nextPosition != positionA ? positionA : positionB;
    }

    
}
