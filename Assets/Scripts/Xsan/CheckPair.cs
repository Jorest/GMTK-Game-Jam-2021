﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPair : MonoBehaviour
{
    public GameObject loveEffectPrefab;

    public GameManager managerScript;

    public void EvaluatePair(List<GameObject> monsters)
    {
        if(monsters.Count == 2 && monsters[0].GetComponent<MonsterType>().type == monsters[1].GetComponent<MonsterType>().type){
            foreach (var m in monsters)
            {
                Instantiate(loveEffectPrefab, m.transform.position, Quaternion.identity);
                Destroy(m);

                managerScript.pairsCount += 1;
            }
        }
    }
}
