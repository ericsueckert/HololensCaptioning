using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using UnityEngine.XR.WSA.Input;

public class SubtitleManager : MonoBehaviour {

    GestureRecognizer gestureRecognizer;
    bool isSleeping = true;

    public Text statusText;
    public Text subtitleText;
    public RawImage statusImage;

    DictationRecognizer dictationRecognizer;

	// Use this for initialization
	void Start () {
        gestureRecognizer = new GestureRecognizer();

        gestureRecognizer.Tapped += TapEvent;

        gestureRecognizer.StartCapturingGestures();
	}

    private void Awake()
    {
        dictationRecognizer = new DictationRecognizer();

        dictationRecognizer.DictationHypothesis += DictationRecognizerHypothesis;
        dictationRecognizer.DictationResult += DictationRecognizerResult;
        dictationRecognizer.DictationComplete += DictationRecognizerComplete;
        
    }

    private void DictationRecognizerComplete(DictationCompletionCause cause)
    {
        SetSleeping();

        dictationRecognizer.Stop();
    }

    private void DictationRecognizerResult(string text, ConfidenceLevel confidence)
    {
        //this.subtitleText.text = text;
        SetListening();
    }

    private void DictationRecognizerHypothesis(string text)
    {
        this.subtitleText.text = text;
        SetThinking();
    }

    private void TapEvent(TappedEventArgs obj)
    {
        isSleeping = !isSleeping;

        if (isSleeping)
        {
            SetSleeping();
            dictationRecognizer.Stop();
        }
        else
        {
            SetListening();
            dictationRecognizer.Start();
        }
    }
    

    private void SetSleeping()
    {
        //var sleepingTexture = (Texture2D)Resources.Load("sleeping");
        //this.statusImage = sleepingTexture;
        this.statusText.text = "Sleeping";
        this.subtitleText.text = "";
    }

    private void SetListening()
    {
        //var listeningTexture = (Texture2D)Resources.Load("listening");
        //this.statusImage = listeningTexture;
        this.statusText.text = "Listening";
    }

    private void SetThinking()
    {
        //var thinkingTexture = (Texture2D)Resources.Load("thinking");
        //this.statusImage = thinkingTexture;
        this.statusText.text = "Thinking";
    }
}
