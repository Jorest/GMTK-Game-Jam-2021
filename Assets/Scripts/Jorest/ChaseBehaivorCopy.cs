using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaivorCopy : MonoBehaviour
{
    GameObject[] food;
    private GameObject witch;
    public float moveSpeed = 1f ;
    public float range = 5f;
    [System.Serializable]
    public enum Behavior
    {
        Follow,
        Run,
        Nothing
    }

    public Behavior foodAction;
    public Behavior WitchAction;

    private Rigidbody2D rb;
    private Vector2 foodMovement;
    private Vector2 witchMovement;

    void Start()
    {
        food = GameObject.FindGameObjectsWithTag("Snack");
        witch = GameObject.Find("Witch");

        rb = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if ( closestSnack()!=null)
        {
            Vector3 foodDirection = closestSnack().transform.position - transform.position;
            foodDirection.Normalize();
            foodMovement = foodDirection;
        }
        

        Vector3 witchDirection = witch.transform.position - transform.position;
        witchDirection.Normalize();
        witchMovement = witchDirection;

        // float angle = Mathf.Atan2(foodDirection.y, foodDirection.x) * Mathf.Rad2Deg; // may be usefull later
        //  float angle2 = Mathf.Atan2(witchDirection.y, witchDirection.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;

    }

    private void FixedUpdate()
    {
       //food
        if (closestSnack() != null)
        {
            if (foodAction== Behavior.Follow)
            {
                moveCharacter(foodMovement);
            } else if (foodAction == Behavior.Run)
            {
                moveCharacter(-foodMovement);
            }
           
        }
        if (Vector2.Distance((Vector2)transform.position, (Vector2)witch.transform.position) < range)
        {
            if (WitchAction == Behavior.Follow)
            {
                moveCharacter(witchMovement);
            }
            else if (WitchAction == Behavior.Run)
            {
                moveCharacter(-witchMovement);
            }
        }

    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    GameObject closestSnack()
    {
        Vector2 monsterPos = new Vector2(transform.position.x,transform.position.y);
        float minDist = Mathf.Infinity;
        GameObject closeSnack = null;

        if (food.Length==0) { 
            return null; 
        }

        foreach (GameObject snack in food)
        {
            Vector2 snackpos = new Vector2(snack.transform.position.x, snack.transform.position.y);
            float dist = Vector2.Distance(monsterPos, snackpos);
            if (dist < range)
            {
                minDist = dist;
                closeSnack = snack;
            }
        }

        return closeSnack;
    }


    }
