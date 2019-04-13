using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Transition(string sceneName)
    {
        StartCoroutine(FadeTransition(sceneName));
    }

    IEnumerator FadeTransition(string sceneName)
    {
        yield return Fader.singleton.FadeOut(1);
        yield return SceneManager.LoadSceneAsync(sceneName);
        Debug.Log("scene loaded");
        yield return new WaitForSeconds(.5f);
        Debug.Log("have waited");
        yield return Fader.singleton.FadeIn(1);
        Debug.Log("fade should be done");
    }
    public void RaiseAffinity(string characterName)
    {
        switch (characterName)
        {
            case "Spike-chan":
                spikeAffinity.affinityLevel++;
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
        }
    }

    [System.Serializable]
    public class Affinity
    {
        public int affinityLevel = 0;
        public bool hasBeenClosed = false;
    }
}
