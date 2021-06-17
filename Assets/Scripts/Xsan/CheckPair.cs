////come coming
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPair : MonoBehaviour
{
    public GameObject loveEffectPrefab;

    public GameManager managerScript;


    public List<AudioClip> alienHappy;
    public List<AudioClip> skeletonHappy;
    public List<AudioClip> ghostHappy;
    public List<AudioClip> demonHappy;
    public List<AudioClip> zillaHappy;
    public List<AudioClip> witchHappy;

    public List<AudioClip> alienAnnoyed;
    public List<AudioClip> skeletonAnnoyed;
    public List<AudioClip> ghostAnnoyed;
    public List<AudioClip> demonAnnoyed;
    public List<AudioClip> zillaAnnoyed;
    public List<AudioClip> witchAnnoyed;

    public void EvaluatePair(List<GameObject> monsters)
    {
        if (monsters.Count == 2)
        {
            bool correctMatch = false;

            if (monsters[0].GetComponent<MonsterType>().type == managerScript.monsterTypes[0] && monsters[1].GetComponent<MonsterType>().type == managerScript.monsterTypes[1])
            {
                correctMatch = true;
            }
            else if (monsters[0].GetComponent<MonsterType>().type == managerScript.monsterTypes[1] && monsters[1].GetComponent<MonsterType>().type == managerScript.monsterTypes[0])
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
                            AudioManager.Instance.PlaySFX(ghostHappy[Random.Range(0, ghostHappy.Count-1)]);
                            break;
                        case "alien":
                            AudioManager.Instance.PlaySFX(alienHappy[Random.Range(0, alienHappy.Count-1)]);
                            break;
                        case "demon"://actually skeleton
                            AudioManager.Instance.PlaySFX(skeletonHappy[Random.Range(0, skeletonHappy.Count-1)]);
                            break;
                        case "devil"://actually demon
                            AudioManager.Instance.PlaySFX(demonHappy[Random.Range(0, demonHappy.Count-1)]);
                            break;
                        case "monster":
                            AudioManager.Instance.PlaySFX(zillaHappy[Random.Range(0, zillaHappy.Count-1)]);
                            break;
                    }
                }

                ///////////
                managerScript.pairsCount += 2;
                

                AudioManager.Instance.PlaySFX(witchHappy[Random.Range(0, witchHappy.Count-1)]);

                return;//prevent annoyed sounds

             }
        }

             

        foreach (var m in monsters)
        {
            switch (m.GetComponent<MonsterType>().type)
            {
                case "ghost":
                    AudioManager.Instance.PlaySFX(ghostAnnoyed[Random.Range(0, ghostAnnoyed.Count-1)]);
                    break;
                case "alien":
                    AudioManager.Instance.PlaySFX(alienAnnoyed[Random.Range(0, alienAnnoyed.Count-1)]);
                    break;
                case "demon"://actually skeleton
                    AudioManager.Instance.PlaySFX(skeletonAnnoyed[Random.Range(0, skeletonAnnoyed.Count-1)]);
                    break;
                case "devil"://actually demon
                    AudioManager.Instance.PlaySFX(demonAnnoyed[Random.Range(0, demonAnnoyed.Count-1)]);
                    break;
                case "monster":
                    AudioManager.Instance.PlaySFX(zillaAnnoyed[Random.Range(0, zillaAnnoyed.Count-1)]);
                    break;
            }
        }

        AudioManager.Instance.PlaySFX(witchAnnoyed[Random.Range(0, witchAnnoyed.Count-1)]);
    
    }
}