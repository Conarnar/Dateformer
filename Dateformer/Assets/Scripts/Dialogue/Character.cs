﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class Character : MonoBehaviour
{
    public string characterName;
    public RectTransform root;
    public Image renderer;

    public Sprite[] images;

    public DialogueSystem dialogue;

    private void Start()
    {
        CharacterManager cm = CharacterManager.singleton;
        root = GetComponent<RectTransform>();
        renderer = GetComponentInChildren<Image>();
        dialogue = DialogueSystem.singleton;
        CharacterManager.singleton.characterDict.Add(characterName, cm.characters.Count);
        cm.characters.Add(this);
    }

    public Character(string _name)
    {
        CharacterManager cm = CharacterManager.singleton;
        GameObject prefab = Resources.Load("Characters/Character[" + _name + "]") as GameObject;
        GameObject ob = GameObject.Instantiate(prefab, cm.characterPanel);
        root = ob.GetComponent<RectTransform>();
        characterName = _name;
        renderer = ob.GetComponent<Image>();
        dialogue = DialogueSystem.singleton;
    }

    public void Say(string speech)
    {
        dialogue.Say(speech, characterName);
    }


    public void SetSprite(int index)
    {
        renderer.sprite = images[index];
    }

}

public enum Mood { neutral, happy, sad, blush, angry};