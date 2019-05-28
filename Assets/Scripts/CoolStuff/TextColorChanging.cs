using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextColorChanging : MonoBehaviour {

    private TextMesh text;

	// Use this for initialization
	void Start () {
        text = GetComponent<TextMesh>();
        Invoke("ColorGreen", 0.5f);
    }

    void ColorGreen() {
        text.color = Color.green;
        Invoke("ColorWhite", 0.5f);
    }

    void ColorWhite() {
        text.color = Color.white;
        Invoke("ColorGreen", 0.5f);
    }


}
