using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPair : MonoBehaviour
{
    public GameObject loveEffectPrefab;
    
    public void EvaluatePair(List<GameObject> monsters)
    {
        if(monsters.Count == 2){
            foreach (var m in monsters)
            {
                Instantiate(loveEffectPrefab, m.transform.position, Quaternion.identity);
                Destroy(m);
            }
        }
    }
}
