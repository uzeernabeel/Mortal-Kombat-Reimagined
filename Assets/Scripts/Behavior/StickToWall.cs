using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToWall : AbstractBehaviors {

    public bool onWallDectected;

    protected float defaultGravityScale;
    protected float defaultDrag;

	// Use this for initialization
	void Start () {
        defaultGravityScale = body2d.gravityScale;
        defaultDrag = body2d.drag;
	}
	
	// Update is called once per frame
	protected virtual void Update () {

        if (collisionState.onWall) {
            if (!onWallDectected) {
                OnStick();
                //ToggleScripts(false);
                onWallDectected = true;
            }
        } else {
            if (onWallDectected) {
                OffWall();
                //ToggleScripts(true);
                onWallDectected = false;
            }
        }

	}

    protected virtual void OnStick() {
        if(!collisionState.standing && body2d.velocity.y > 0) {
            body2d.gravityScale = 0;
            body2d.drag = 100;
        }
    }

    protected virtual void OffWall() {
        if(body2d.gravityScale != defaultGravityScale) {
            body2d.gravityScale = defaultGravityScale;
            body2d.drag = defaultDrag;
        }
    }
}
