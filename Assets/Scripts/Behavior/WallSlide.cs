using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : StickToWall {

    public float slideVelocity = -1.5f;
    public float slideMultiplier = 1.5f;
    public bool slidingDownTheWall;

	// Update is called once per frame
	override protected void Update () {
        base.Update();

        if (onWallDectected) {
            var velY = slideVelocity;
            slidingDownTheWall = true;
            if (inputState.GetButtonValue(inputButtons[0]))
                velY *= slideMultiplier;

            body2d.velocity = new Vector2(body2d.velocity.x, velY);
        } 
    }

    protected override void OnStick() {
        body2d.velocity = Vector2.zero;
    }

    protected override void OffWall() {
        //does nothing just for avoiding the original stuff from happening.
        //Debug.Log("I am off the wall!");
        slidingDownTheWall = false;
    }
}
