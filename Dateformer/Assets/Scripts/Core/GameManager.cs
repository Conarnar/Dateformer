using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void RaiseAffinity(string characterName)
    {
        switch (characterName)
        {
            case "Spike-chan":
                spikeAffinity.affinityLevel++;
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
