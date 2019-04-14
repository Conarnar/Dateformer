using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    public Character character;

    [SerializeField] string sceneToLoad;
    [SerializeField] int dialogueIndex = 0;
    [SerializeField] bool additive = false;
    public List<Behavior> characterLines = new List<Behavior>();

    [SerializeField] int choiceIndex = 0;
    public List<Choices> choices = new List<Choices>();

    bool endTriggered = false;
    bool startedDialogue = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Pause");
        
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
        character.dialogue.Say(characterLines[dialogueIndex].dialogue, character.characterName);
        character.SetSprite((int)characterLines[dialogueIndex].currentMood);
        startedDialogue = true;
        if (characterLines[dialogueIndex].promptForResponse)
        {
            choiceIndex = characterLines[dialogueIndex].choiceIndex;
            character.dialogue.PromptForAnswer(choices[choiceIndex].choice1Text, choices[choiceIndex].choice2Text, choices[choiceIndex].choice1NextIndex, choices[choiceIndex].choice2NextIndex);
            character.dialogue.isWaitingForResponse = true;
        }
        dialogueIndex++;
    }


    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !character.dialogue.isWaitingForResponse && !endTriggered && startedDialogue)
        {
            if (!character.dialogue.isSpeaking || character.dialogue.isWaitingForUserInput)
            {
                if (characterLines[dialogueIndex].isEndLine)
                {
                    character.dialogue.Close();
                    if (characterLines[dialogueIndex].closeRoute)
                        GameManager.singleton.CloseRoute(character.characterName);
                    else
                        GameManager.singleton.RaiseAffinity(character.characterName);

                    endTriggered = true;
                    GameManager.singleton.Transition(sceneToLoad);
                    return;
                }
                character.Say(characterLines[dialogueIndex].dialogue);
                character.SetSprite((int)characterLines[dialogueIndex].currentMood);
                if (characterLines[dialogueIndex].promptForResponse)
                {
                    choiceIndex = characterLines[dialogueIndex].choiceIndex;
                    character.dialogue.PromptForAnswer(choices[choiceIndex].choice1Text, choices[choiceIndex].choice2Text, choices[choiceIndex].choice1NextIndex, choices[choiceIndex].choice2NextIndex);
                    character.dialogue.isWaitingForResponse = true;     
                    return;
                }
                if(!characterLines[dialogueIndex].isEndLine)
                    dialogueIndex = characterLines[dialogueIndex].nextDialogueIndex;
                
            }
        }
    }

    public void AnswerChoiceOne()
    {
        dialogueIndex = character.dialogue.choice1Index;
        character.dialogue.isWaitingForResponse = false;
        character.dialogue.CloseChoicePanel();
        character.Say(characterLines[dialogueIndex].dialogue);
        character.SetSprite((int)characterLines[dialogueIndex].currentMood);

        if (characterLines[dialogueIndex].promptForResponse)
        {
            choiceIndex = characterLines[dialogueIndex].choiceIndex;
            character.dialogue.PromptForAnswer(choices[choiceIndex].choice1Text, choices[choiceIndex].choice2Text, choices[choiceIndex].choice1NextIndex, choices[choiceIndex].choice2NextIndex);
            character.dialogue.isWaitingForResponse = true;
        }

        if (!characterLines[dialogueIndex].isEndLine)
            dialogueIndex = characterLines[dialogueIndex].nextDialogueIndex;
    }

    public void AnswerChoiceTwo()
    {
        dialogueIndex = character.dialogue.choice2Index;
        character.dialogue.isWaitingForResponse = false;
        character.dialogue.CloseChoicePanel();
        character.Say(characterLines[dialogueIndex].dialogue);
        character.SetSprite((int)characterLines[dialogueIndex].currentMood);

        if (characterLines[dialogueIndex].promptForResponse)
        {
            choiceIndex = characterLines[dialogueIndex].choiceIndex;
            character.dialogue.PromptForAnswer(choices[choiceIndex].choice1Text, choices[choiceIndex].choice2Text, choices[choiceIndex].choice1NextIndex, choices[choiceIndex].choice2NextIndex);
            character.dialogue.isWaitingForResponse = true;
            return;
        }

        if (!characterLines[dialogueIndex].isEndLine)
            dialogueIndex = characterLines[dialogueIndex].nextDialogueIndex;
    }

    [System.Serializable]
    public class Behavior
    {
        public int nextDialogueIndex;
        public string dialogue;
        public Mood currentMood;
        public bool promptForResponse;
        public int choiceIndex;
        public bool isEndLine;
        public bool closeRoute = false;
    }

    [System.Serializable]
    public class Choices
    {
        public string choice1Text = "";
        public int choice1NextIndex;

        public string choice2Text = "";
        public int choice2NextIndex;
    }
}
