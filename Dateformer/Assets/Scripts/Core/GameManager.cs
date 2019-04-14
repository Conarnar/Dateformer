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

    public void TransitionEvent(string enemyName)
    {
        string sceneToLoad;
        switch (enemyName)
        {
            case "Spike-chan":
                sceneToLoad = "SpikeEvent" + (spikeAffinity.affinityLevel + 1);
                Transition(sceneToLoad);
                break;
            case "Turtle-chan":
                sceneToLoad = "TurtleEvent" + (enemyAffinity.affinityLevel + 1);
                Transition(sceneToLoad);
                break;
            case "Bullet-chan":
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
            case "Spike-chan":
                spikeAffinity.hasBeenClosed = true;
                break;
            case "Turtle-chan":
                enemyAffinity.hasBeenClosed = true;
                break;
            case "Bullet-chan":
                bulletAffinity.hasBeenClosed = true;
                break;
        }
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
}
