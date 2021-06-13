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
    public AudioClip ghost;
    public AudioClip demon;
    public AudioClip other;

    public AudioClip witchbumps;

    public float timeBetweenSFX = 0.5f;
    private float cooldown;
    private bool enable = true;




    void Start()
    {
        cooldown = timeBetweenSFX;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enable)
        {
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0)
        {
            enable = true;
            cooldown = timeBetweenSFX;
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Monster" && enable == true)
        {
            
            GameObject monster = col.gameObject;
            enable = false;
            string type = monster.GetComponent<MonsterType>().type;

            switch (type)
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

            AudioManager.Instance.PlaySFX(witchbumps);
        }


    }
}
