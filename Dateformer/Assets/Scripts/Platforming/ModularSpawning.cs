﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularSpawning : MonoBehaviour
{
    [SerializeField] bool spawnLowestAffinity;
    [SerializeField] bool spawnMiddleAffinity;
    [SerializeField] bool spawnHighestAffinity;

    void Awake()
    {
        GameManager manager = GameManager.singleton;
        int[] affinities = new int[] { manager.spikeAffinity.affinityLevel, manager.bulletAffinity.affinityLevel, manager.enemyAffinity.affinityLevel };
        List<int> orderedAffinities = new List<int>();

        orderedAffinities.AddRange(affinities);
        orderedAffinities.Sort();

        List<int> choices = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            if (affinities[i] == orderedAffinities[0] && spawnLowestAffinity)
            {
                choices.Add(i);
            }
            else if (affinities[i] == orderedAffinities[1] && spawnMiddleAffinity)
            {
                choices.Add(i);
            }
            else if (affinities[i] == orderedAffinities[2] && spawnHighestAffinity)
            {
                choices.Add(i);
            }
        }

        if (choices.Count == 0)
        {
            choices.AddRange(new int[] { 0, 1, 2 });
        }

        switch (choices[Random.Range(0, choices.Count)])
        {
            case 0:
                // spike
                break;
            case 1:
                // bullet
                break;
            case 2:
                // enemy
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
