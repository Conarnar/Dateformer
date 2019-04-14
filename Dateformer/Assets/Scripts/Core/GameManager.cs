using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// last edited: evan
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public Affinity spikeAffinity = new Affinity();
    public Affinity bulletAffinity = new Affinity();
    public Affinity enemyAffinity = new Affinity();

    public int stageIndex = 1;

    GameObject pauseScreen; 
    private void Awake()
    {
        if (singleton != null)
            Destroy(this.gameObject);
        else
        {
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen"); 
    }

    public void TransitionEvent(string enemyName)
    {
        string sceneToLoad;
        switch (enemyName)
        {
            case "Spike":
                sceneToLoad = "SpikeEvent" + (spikeAffinity.affinityLevel + 1);
                Transition(sceneToLoad);
                break;
            case "Turtle":
                sceneToLoad = "TurtleEvent" + (enemyAffinity.affinityLevel + 1);
                Transition(sceneToLoad);
                break;
            case "Bullet":
                sceneToLoad = "BulletEvent" + (bulletAffinity.affinityLevel + 1);
                Transition(sceneToLoad);
                break;
        }
    }

    public void Transition(string sceneName)
    {
        StartCoroutine(FadeTransition(sceneName));
    }

    IEnumerator FadeTransition(string sceneName)
    {
        yield return Fader.singleton.FadeOut(1);
        yield return SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitForSeconds(.5f);
        yield return Fader.singleton.FadeIn(1); 
    }
    public void RaiseAffinity(string characterName)
    {
        switch (characterName)
        {
            case "Spike-chan":
                spikeAffinity.affinityLevel++;
                break;
            case "Turtle-chan":
                enemyAffinity.affinityLevel++;
                break;
            case "Bullet-chan":
                bulletAffinity.affinityLevel++;
                break;
        }
    }

    public void CloseRoute(string characterName)
    {
        switch (characterName)
        {
            case "Spike":
                spikeAffinity.hasBeenClosed = true;
                break;
            case "Turtle":
                enemyAffinity.hasBeenClosed = true;
                break;
            case "Bullet":
                bulletAffinity.hasBeenClosed = true;
                break;
        }
    }

    public void TransitionNextLevel()
    {
        stageIndex++;
        if (stageIndex > 1)
            stageIndex = 0;
        string stageLevel = "Stage" + (stageIndex + 1);
        Transition(stageLevel);
    }

    public void restart()
    {
        //restarts the game
        spikeAffinity = new Affinity();
        bulletAffinity = new Affinity();
        enemyAffinity = new Affinity();
    }

    [System.Serializable]
    public class Affinity
    {
        public int affinityLevel = 0;
        public bool hasBeenClosed = false;
    }

    private void Update()
    {
        //pause
        if (Input.GetButtonDown("Cancel"))
        {
            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
                if (pauseScreen != null)
                {
                    pauseScreen.SetActive(false);
                }
            }
            else {
                Time.timeScale = 0f;
                if (pauseScreen != null)
                {
                    pauseScreen.SetActive(true);
                }
            }
                
        }



    }
}
