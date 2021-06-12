using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchFood : MonoBehaviour
{
    List<GameObject> food;
    public float moveSpeed = 1f ;
    public float range = 100f;
    [System.Serializable]
    public enum PieceType
    {
        Witch,
        Food,
        Any
    }
    public PieceType pieceType;
    private Rigidbody2D rb;
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        food = new List<GameObject>();
        food.Add(GameObject.Find("Witch"));
        rb = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if ( closestSnack()==null)
        {
            return;
        }
        
        Vector3 direction = closestSnack().transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //rb.rotation = angle;
        direction.Normalize();
        movement = direction;
     

    }

    private void FixedUpdate()
    {
        if (closestSnack() != null)
        {
            moveCharacter(movement);
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

        if (food.Count==0) { 
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
