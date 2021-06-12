using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float normalMoveSpeed;
    public Vector2 newTargetTimeRange;
    public float moveRange;

    private Transform spriteTransform;
    private SpriteRenderer spriteRenderer;
    public Sprite frontSprite;
    public Sprite backSprite;

    private Rigidbody2D body;

    private Vector3 target;
    private Vector3 newTarget;
    private Vector2 moveDirection;
    private float moveSpeed;

    private IEnumerator WalkSine()
    {
        float spriteRotation = 0f;
        float rotationBorder = 5f;
        int sideMultiplier = 1;
        float rotationSpeed = 0f;

        while (true)
        {
            rotationSpeed = moveSpeed * 2.5f;

            if (moveDirection.magnitude > 0.05f)
            {
                if (spriteRotation > rotationBorder)
                {
                    sideMultiplier = -1;
                }
                else if (spriteRotation < -rotationBorder)
                {
                    sideMultiplier = 1;
                }

                spriteRotation += rotationSpeed * sideMultiplier * Time.deltaTime;

                spriteTransform.rotation = Quaternion.Euler(0, 0, spriteRotation);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                spriteRotation = Mathf.Lerp(spriteRotation, 0, 0.1f);

                spriteTransform.rotation = Quaternion.Euler(0, 0, spriteRotation);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private IEnumerator Wandering()
    {
        newTarget = transform.position + new Vector3(Random.Range(-moveRange, moveRange), Random.Range(-moveRange, moveRange), 0);

        while (true)
        {
            //moveSpeed = normalMoveSpeed * 0.5f;

            yield return new WaitForSeconds(Random.Range(1.5f, 3f));
            newTarget = transform.position + new Vector3(Random.Range(-moveRange, moveRange), Random.Range(-moveRange, moveRange), 0);
        }
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteTransform = transform.GetChild(0);
        spriteRenderer = spriteTransform.GetComponent<SpriteRenderer>();

        target = transform.position;

        moveSpeed = normalMoveSpeed;

        StartCoroutine(WalkSine());
        StartCoroutine(Wandering());
    }

    void FixedUpdate()
    {
        moveDirection = target - transform.position;
        if (moveDirection.magnitude > 0.05f)
        {
            body.MovePosition(body.position + moveDirection.normalized * moveSpeed / 1000);

            if (moveDirection.x > 0)
            {
                spriteRenderer.flipX = true; //oposite to the witch
            }
            else
            {
                spriteRenderer.flipX = false; //oposite to the witch
            }

            if (moveDirection.y > 0)
            {
                spriteRenderer.sprite = backSprite;
            }
            else
            {
                spriteRenderer.sprite = frontSprite;
            }
        }

        target = Vector2.Lerp(target, newTarget, 0.01f);
    }
}
