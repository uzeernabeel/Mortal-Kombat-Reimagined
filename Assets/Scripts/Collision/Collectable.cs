using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public string targetTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == targetTag) {
            OnCollect(collision.gameObject);
            OnDestroy();
        }
    }

    protected virtual void OnCollect(GameObject target) {

    }

    protected virtual void OnDestroy() {
        Destroy(gameObject);
    }
}
