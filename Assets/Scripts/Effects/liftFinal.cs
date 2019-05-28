using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liftFinal : MonoBehaviour {

    private Vector3 positionA;
    private Vector3 positionB;
    private Vector3 nextPosition;

    //[SerializeField] private Transform childTransform;
    [SerializeField] private Transform transformB;

    [Range(1, 10)]
    public int speed = 2;

    private void Awake() {
        positionA = transform.localPosition;
        positionB = transformB.localPosition;
        nextPosition = positionB;
    }

    // Use this for initialization
    void Start () {
        //positionA = transform.localPosition;
        //positionB = transformB.localPosition;
        //nextPosition = positionB;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
    }

    private void Move() {

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, nextPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, nextPosition) <= 0.1) {
            ChangeDestination();
        }
    }

    private void ChangeDestination() {
        nextPosition = nextPosition != positionA ? positionA : positionB;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.collider.transform.SetParent(null);
        }
    }
}
