using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLift : MonoBehaviour {

    private Vector3 positionA;
    private Vector3 positionB;
    private Vector3 nextPosition;

    [SerializeField] private Transform childTransform;
    [SerializeField] private Transform transformB;

    [Range(1, 10)]
    public int speed = 2;

    // Use this for initialization
    void Start () {

        positionA = childTransform.transform.localPosition;
        positionB = transformB.transform.localPosition;
        nextPosition = positionB;

    }

    private void Update() {

        Move(); 
    }

    private void Move() {

        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPosition, speed * Time.deltaTime);

        if (Vector3.Distance(childTransform.localPosition, nextPosition) <= 0.1) {
            ChangeDestination();
        }
    }

    private void ChangeDestination() {
        nextPosition = nextPosition != positionA ? positionA : positionB;
    }
}
