using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Duck : AbstractBehaviors {

    public float scale = 0.5f;
    public bool ducking;
    public float centerOffSetY = 0f;

    private CapsuleCollider2D circleCollider;
    private Vector2 originalCenter;
    private Vector2 originalSize;

    protected override void Awake() {
        base.Awake();

        circleCollider = GetComponent<CapsuleCollider2D>();
        originalCenter = circleCollider.offset;
        originalSize = circleCollider.size;
        
    }

    protected virtual void OnDuck(bool value) {
        ducking = value;

        ToggleScripts(!ducking);
        
        var sizeY = circleCollider.size.y;
        var sizeX = circleCollider.size.x;

        float newOffSetY;
        //float sizeReciprocal;

        if (ducking) {
            //sizeReciprocal = scale;
            //newOffSetY = circleCollider.offset.y - size / 2 + centerOffSetY; 
            sizeY = 0.68f; 
            newOffSetY = 0.35f;
        } else {
            sizeX = originalSize.x;
            sizeY = originalSize.y;
            newOffSetY = originalCenter.y;
        }

        //sizeX = sizeX * sizeReciprocal;
        circleCollider.size = new Vector2(sizeX, sizeY);
        circleCollider.offset = new Vector2(circleCollider.offset.x, newOffSetY);
    }

	// Update is called once per frame
	void Update () {

//#if UNITY_EDITOR
        var canDuck = inputState.GetButtonValue(inputButtons[0]);
//#else
        //var canDuck = CrossPlatformInputManager.GetButtonDown(inputButtons[0]);
//#endif
        if(canDuck && collisionState.standing && !ducking) {
            OnDuck(true);
        } else if(ducking && !canDuck) {
            OnDuck(false);
        }

	}
}
