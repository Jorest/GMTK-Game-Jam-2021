using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPair : MonoBehaviour
{
    public GameObject loveEffectPrefab;

    public GameManager managerScript;

    public void EvaluatePair(List<GameObject> monsters)
    {
        if(monsters.Count == 2){
            bool correctMatch = false;

            if (monsters[0].GetComponent<MonsterType>().type == managerScript.monsterTypes[0] && monsters[1].GetComponent<MonsterType>().type == managerScript.monsterTypes[1])
            {
                correctMatch = true;
            }
            else if(monsters[0].GetComponent<MonsterType>().type == managerScript.monsterTypes[1] && monsters[1].GetComponent<MonsterType>().type == managerScript.monsterTypes[0])
            {
                correctMatch = true;
            }

            if (correctMatch)
            {
                managerScript.RandomizePair();

                foreach (var m in monsters)
                {
                    Instantiate(loveEffectPrefab, m.transform.position, Quaternion.identity);
                    Destroy(m);

                    managerScript.pairsCount += 1;
                }
            }
        }
    }
}
