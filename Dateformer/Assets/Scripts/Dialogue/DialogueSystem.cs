using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem singleton;
    public DialogueElements elements;
    public GameObject speechPanel { get { return elements.speechPanel; } }
    public Text speakerNameText { get { return elements.speakerNameText; } }
    public Text speechText { get { return elements.speechText; } }
    public bool isSpeaking { get { return speaking != null; } }
    public bool isWaitingForUserInput = false;

    Coroutine speaking = null;
    string targetSpeech = "";

    private void Awake()
    {
        singleton = this;
    }

    public void Say(string speech, string speaker="")
    {
        StopSpeaking();
        speaking = StartCoroutine(Speaking(speech, speaker, false));
    }

    public void SayAdd(string speech, string speaker = "")
    {
        StopSpeaking();
        speechText.text = targetSpeech;
        speaking = StartCoroutine(Speaking(speech, speaker, true));
    }

    public void Close()
    {
        StopSpeaking();
        speechPanel.SetActive(false);
    }

    private void StopSpeaking()
    {
        if(isSpeaking)
            StopCoroutine(speaking);
        speaking = null;
    }

    string DetermineSpeaker(string s)
    {
        string retVal = speakerNameText.text;
        if (s != speakerNameText.text && s != "")
            retVal = (s.ToLower().Contains("Narrator")) ? "" : s;
        return retVal;
    }


    IEnumerator Speaking(string speech, string speaker, bool additive = false)
    {
        speechPanel.SetActive(true);
        targetSpeech = speech;

        if (!additive)
            speechText.text = "";
        else
            speech = speechText.text + targetSpeech;

        speakerNameText.text = DetermineSpeaker(speaker); //temporary
        isWaitingForUserInput = false;

        while(speechText.text != speech)
        {
            speechText.text += speech[speechText.text.Length];
            yield return new WaitForEndOfFrame();
        }
        isWaitingForUserInput = true;
        while (isWaitingForUserInput)
            yield return new WaitForEndOfFrame();

        StopSpeaking();
    }

    [System.Serializable]
    public class DialogueElements
    {
        /// <summary>
        /// Main panel containing all dialogue related elements on the UI
        /// </summary>
        public GameObject speechPanel;
        public Text speakerNameText;
        public Text speechText;
    }


}
