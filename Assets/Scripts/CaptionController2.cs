using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using UnityEngine.XR.WSA.Input;
using HoloToolkit.Unity.InputModule;

public class CaptionController2 : MonoBehaviour {

    GestureRecognizer gestureRecognizer;
    public bool isSleeping = false;

    public Text statusText;
    public Text subtitleText;
    public GameObject thinkingImage;
    public GameObject listeningImage;
    public GameObject sleepingImage;

    DictationRecognizer dictationRecognizer;

    public bool IsEnabled = true;

    /// <summary>
    /// Does the GameObject currently have focus?
    /// </summary>
    public bool HasGaze { get; protected set; }

    // Use this for initialization
    void Start()
    {

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

    //public virtual void OnFocusEnter()
    //{
    //    if (!IsEnabled)
    //    {
    //        return;
    //    }

    //    HasGaze = true;
    //}

    //public virtual void OnFocusExit()
    //{
    //    HasGaze = false;
    //}

    public void ToggleCaptioning()
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
        //this.statusText.text = "Sleeping";
        sleepingImage.SetActive(true);
        thinkingImage.SetActive(false);
        listeningImage.SetActive(false);
        this.subtitleText.text = "";
    }

    private void SetListening()
    {
        //var listeningTexture = (Texture2D)Resources.Load("listening");
        //this.statusImage = listeningTexture;
        sleepingImage.SetActive(false);
        thinkingImage.SetActive(false);
        listeningImage.SetActive(true);
        //this.statusText.text = "Listening";
        this.subtitleText.text = "";
    }

    private void SetThinking()
    {
        //var thinkingTexture = (Texture2D)Resources.Load("thinking");
        thinkingImage.SetActive(true);
        listeningImage.SetActive(false);
        //this.statusText.text = "Thinking";
    }
}
