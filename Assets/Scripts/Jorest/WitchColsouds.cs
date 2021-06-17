using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchColsouds : MonoBehaviour
{

    // alien
    // skeleton
    // zombie
    // demon
    // zilla
    public List<AudioClip> alien;
    public List<AudioClip> skeleton;
    public List<AudioClip> ghost;
    public List<AudioClip> demon;
    public List<AudioClip> zilla;

    public List<AudioClip> witchbumps;
    public AudioClip music;

    public float timeBetweenSFX = 0.5f;
    private float cooldown;
    private bool enable = true;




    void Start()
    {
        AudioManager.Instance.PlayMusic(music, 0.25f);
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
                    AudioManager.Instance.PlaySFX(ghost[Random.Range(0, ghost.Count-1)]);
                    break;
                case "alien":
                    AudioManager.Instance.PlaySFX(alien[Random.Range(0, alien.Count-1)]);
                    break;
                case "demon"://actually skeleton
                    AudioManager.Instance.PlaySFX(skeleton[Random.Range(0, skeleton.Count-1)]);
                    break;
                case "devil"://actually demon
                    AudioManager.Instance.PlaySFX(demon[Random.Range(0, demon.Count-1)]);
                    break;
                case "monster":
                    AudioManager.Instance.PlaySFX(zilla[Random.Range(0, zilla.Count-1)]);
                    break;
            }

            AudioManager.Instance.PlaySFX(witchbumps[Random.Range(0, witchbumps.Count-1)]);
        }


    }
}
