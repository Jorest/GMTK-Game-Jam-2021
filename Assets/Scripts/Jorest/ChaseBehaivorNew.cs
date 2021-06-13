using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaivorNew : MonoBehaviour
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

    private MonsterMovement monsterMovementScript;
    private SpriteRenderer spriteRenderer;
    private bool delayActive = true;

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.1f);
        delayActive = false;

    }

    void Start()
    {
        
        witch = GameObject.Find("Witch");

        rb = this.GetComponent<Rigidbody2D>();

        monsterMovementScript = GetComponent<MonsterMovement>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        monsterMovementScript.enabled = true;
        StartCoroutine(StartDelay());
    }

    // Update is called once per frame
    void Update()
    {
        food = GameObject.FindGameObjectsWithTag("Snack");
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
   

        if (!delayActive)
        {
            bool witchA = true;
            if (Vector2.Distance((Vector2)transform.position, (Vector2)witch.transform.position) < range)
            {
                if (WitchAction == Behavior.Follow)
                {
                    moveCharacter(witchMovement);
                    monsterMovementScript.enabled = false;
                }
                else if (WitchAction == Behavior.Run)
                {
                    moveCharacter(-witchMovement);
                    monsterMovementScript.enabled = false;
                }
                else
                {
                  //  monsterMovementScript.enabled = true;
                    witchA = false;
                }
            }
            else
            {
                
                monsterMovementScript.enabled = true;

                
            }


            //food
            if (closestSnack() != null)
            {
                if (foodAction == Behavior.Follow)
                {
                    moveCharacter(foodMovement);
                    monsterMovementScript.enabled = false;

                }
                else if (foodAction == Behavior.Run)
                {
                    moveCharacter(-foodMovement);
                    monsterMovementScript.enabled = false;

                }
                else if (!witchA)
                {
                    monsterMovementScript.enabled = true;

                }

            }

        }

    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));

        if (direction.x > 0)
        {
            spriteRenderer.flipX = true; //oposite to the witch
        }
        else
        {
            spriteRenderer.flipX = false; //oposite to the witch
        }
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
            if (snack != null){

                Vector2 snackpos = new Vector2(snack.transform.position.x, snack.transform.position.y);
                float dist = Vector2.Distance(monsterPos, snackpos);
                if (dist < range)
                {
                    minDist = dist;
                    closeSnack = snack;
                }
            }
        }

        return closeSnack;
    }


    }
