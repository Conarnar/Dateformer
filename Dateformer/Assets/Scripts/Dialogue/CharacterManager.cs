using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Responsible for adding and maintaining characters in the scene
public class CharacterManager : MonoBehaviour
{
    public static CharacterManager singleton;

    public RectTransform characterPanel;

    public List<Character> characters = new List<Character>();

    public Dictionary<string, int> characterDict = new Dictionary<string, int>();

    private void Awake()
    {
        singleton = this;
    }

    
    public Character GetCharacter(string characterName, bool createCharacterIfDoesNotExist = true)
    {
        int index = -1;
        if(characterDict.TryGetValue(characterName, out index))
        {
            return characters[index];
        }
        else if(createCharacterIfDoesNotExist)
        {
            return CreateCharacter(characterName); 
        }
        return null;
    }

    public Character CreateCharacter(string characterName)
    {
        Character newCharacter = new Character(characterName);
        characterDict.Add(characterName, characters.Count);
        characters.Add(newCharacter);

        return newCharacter;
    }
}
