using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public enum Buttons {
    Right,
    Left, 
    Up, 
    Down,
    A,
    B
}

public enum Condition {
    GreaterThan,
    LessThan
}

[System.Serializable]
public class InputAxisState {
    public string axisName;
    public float offValue;
    public Buttons button;
    public Condition condition;

    public bool value {

        get {
#if UNITY_EDITOR
            var val = Input.GetAxis(axisName);
#else
            var val = CrossPlatformInputManager.GetAxis(axisName);
#endif
            switch (condition) {
                case Condition.GreaterThan:
                    return val > offValue;
                case Condition.LessThan:
                    return val < offValue;
            }
            return false;
        }
    }
}

public class InputManager : MonoBehaviour {

    public InputAxisState[] inputs;
    public InputState inputState;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach(var input in inputs){
            //CrossPlatformInputManager.SetButtonDown(Buttons.)
            inputState.SetButtonValue(input.button, input.value);
        }
	}
}
