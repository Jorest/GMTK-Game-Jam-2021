using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchColsouds : MonoBehaviour
{

    // alien
    // skeleton
    // zombie
    // demon
    // other
    public AudioClip alien;
    public AudioClip skeleton;
    public AudioClip zombie;
    public AudioClip demon;
    public AudioClip other;






    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Monster")
        {
            
            GameObject monster = col.gameObject;
            
            string type = monster.GetComponent<MonsterType>().type;

            switch (type)
            {
                case "zombie":
                    AudioManager.Instance.PlaySFX(zombie);
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
                case "other":
                    AudioManager.Instance.PlaySFX(other);
                    break;
            }

        }


    }
}
