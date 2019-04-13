using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    public Character character;


    [SerializeField] int index = 0;
    [SerializeField] bool additive = false;
    public List<Behavior> characterLines = new List<Behavior>();


    // Start is called before the first frame update
    void Start()
    {
        character.dialogue.Say(characterLines[index].dialogue, character.characterName);
        index++;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!character.dialogue.isSpeaking || character.dialogue.isWaitingForUserInput)
            {
                if (index >= characterLines.Count)
                {
                    character.dialogue.Close();
                    GameManager.singleton.RaiseAffinity(character.characterName);
                    return;
                }
                character.Say(characterLines[index].dialogue);
                character.SetSprite((int)characterLines[index].currentMood);
                index++;
            }
        }
    }

    [System.Serializable]
    public class Behavior
    {
        public string dialogue;
        public Mood currentMood;
    }
}
