using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using HoloToolkit.Unity.InputModule;

public class ToggleTextColor : MonoBehaviour {

    public bool isBlack = true;
    public GameObject subtitleText;
    private Text text;
    

	// Use this for initialization

    void Start()
    {
        text = subtitleText.GetComponent<Text>();
    }
	public void ToggleText()
    {
        if (isBlack)
        {
            text.color = new Color(1, 1, 1, 1);
            isBlack = false;
        } else
        {
            text.color = new Color(0, 0, 0, 1);
            isBlack = true;
        }
    }
}
