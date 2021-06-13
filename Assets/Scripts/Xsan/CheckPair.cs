using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPair : MonoBehaviour
{
    public GameObject loveEffectPrefab;
    
    public GameManager managerScript;

    public AudioClip witchHappy;

    public AudioClip alien;
    public AudioClip skeleton;
    public AudioClip ghost;
    public AudioClip demon;
    public AudioClip other;

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
                    /////////////Jorest
                    switch (m.GetComponent<MonsterType>().type)
                    {
                        case "ghost":
                            AudioManager.Instance.PlaySFX(ghost);
                            break;
                        case "alien":
                            AudioManager.Instance.PlaySFX(alien);
                            break;
                        case "skeleton":
                            AudioManager.Instance.PlaySFX(skeleton);
                            break;
                        case "demon":
                            AudioManager.Instance.PlaySFX(demon);
                            break;
                        case "monster":
                            AudioManager.Instance.PlaySFX(other);
                            break;
                    }

                    AudioManager.Instance.PlaySFX(witchHappy);



                    ///////////
                    managerScript.pairsCount += 1;
                }
            }
        }
    }
}
