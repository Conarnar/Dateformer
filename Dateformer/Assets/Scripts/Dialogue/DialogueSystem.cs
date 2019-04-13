using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem singleton;
    public DialogueElements elements;
    public GameObject speechPanel { get { return elements.speechPanel; } }
    public GameObject choicePanel { get { return elements.choicePanel; } }

    public int choice1Index;
    public int choice2Index;

    public Button choice2Button { get { return elements.button; } }

    public Text choice1Text { get { return elements.choice1Text; } }
    public Text choice2Text { get { return elements.choice2Text; } }

    public Text speakerNameText { get { return elements.speakerNameText; } }
    public Text speechText { get { return elements.speechText; } }
    public bool isSpeaking { get { return speaking != null; } }
    public bool isWaitingForUserInput = false;

    public bool isWaitingForResponse = false;


    Coroutine speaking = null;
    string targetSpeech = "";

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        choice1Index = elements.choice1Button;
        choice2Index = elements.choice2Button;
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

    public void PromptForAnswer(string choice1, string choice2, int choice1NextIndex, int choice2NextIndex)
    {
        choice1Text.text = choice1;
        choice2Text.text = choice2;
        choice1Index = choice1NextIndex;
        choice2Index = choice2NextIndex;
        choicePanel.SetActive(true);

        if (choice2 == "")
            choice2Button.gameObject.SetActive(false);
        else
            choice2Button.gameObject.SetActive(true);
    }

    public void CloseChoicePanel()
    {
        choicePanel.SetActive(false);
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

        public GameObject choicePanel;
        public int choice1Button;
        public int choice2Button;
        public Text choice1Text;
        public Text choice2Text;
        
        public Button button;
    }

}


