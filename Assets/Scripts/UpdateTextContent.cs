using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTextContent : MonoBehaviour {

    public Text text;
    public Text showText;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        showText.text = text.text;
	}

    public void SetText(string str)
    {
        text.text = str;
    }




}
