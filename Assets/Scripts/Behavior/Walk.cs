using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Walk : AbstractBehaviors {

    public float speed = 10f;
    public float runMultiplier = 1.5f;
    public bool running;
    public bool run2;
    private Text speedText;

    private GameObject speedButton;

    // Use this for initialization
    void Start () {
        speedText = GameObject.FindGameObjectWithTag("SpeedText").GetComponent<Text>();
        speedButton = GameObject.FindGameObjectWithTag("SpeedButton");
    }

    // Update is called once per frame
    void Update() {

        running = false;

        var right = inputState.GetButtonValue(inputButtons[0]);
        var left = inputState.GetButtonValue(inputButtons[1]);
        //var run = inputState.GetButtonValue(inputButtons[2]);

        if(GameController.instance.SpeedPower <= 0 && run2) {
            //speedButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            speedButton.GetComponent<Image>().color = new Color(1, 1, 0, 0.25f);
            run2 = false;
            Physics2D.IgnoreLayerCollision(10, 11, false);
            Color tempColor = new Color(1, 1, 1, 1);
            GetComponent<SpriteRenderer>().color = tempColor;
        }

        //checking speed runner
#if UNITY_EDITOR
        if (Input.GetButtonDown("Fire1") && !run2 && GameController.instance.SpeedPower > 0) {
#else
        if(CrossPlatformInputManager.GetButtonDown("Fire1") && !run2 && GameController.instance.SpeedPower > 0){
#endif
            run2 = true;
            Physics2D.IgnoreLayerCollision(10, 11, true);
            Color tempColor = new Color(1, 1, 0.13725f, 1);
            GetComponent<SpriteRenderer>().color = tempColor;
        //}

#if UNITY_EDITOR
        } else if (Input.GetButtonDown("Fire1") && run2) {
#else
        } else if(CrossPlatformInputManager.GetButtonDown("Fire1") && run2) {
#endif
            run2 = false;
            Physics2D.IgnoreLayerCollision(10, 11, false);
            Color tempColor = new Color(1, 1, 1, 1);
            GetComponent<SpriteRenderer>().color = tempColor;

        }

        if (right || left) {
            var tmpSpeed = speed;

            if(run2 && runMultiplier > 0) {
                GameController.instance.SpeedPower -= Time.deltaTime;
                if (GameController.instance.SpeedPower >= 0)
                    speedText.text = FormatTime(GameController.instance.SpeedPower);
                else
                    speedText.text = "0";
                tmpSpeed *= runMultiplier;
                running = true;
            }

            var velX = tmpSpeed * (float)inputState.direction;
            body2d.velocity = new Vector2(velX, body2d.velocity.y);
        }

    }

    public string FormatTime(float value) {
        int t = (int)value;

        if (value > 999) {
            return String.Format("{0:D4}", t);
        } else if (value > 99) {
            return String.Format("{0:D3}", t);
        } else {
            return String.Format("{0:D2}", t);
        }
        //return value.ToString().
    }
}
