using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMovement : MonoBehaviour
{
    public float normalMoveSpeed;
    public Sprite frontSprite;
    public Sprite backSprite;

    [HideInInspector]
    public float moveSpeed;

    [Header("Other")]

    private Vector2 moveDirection;

    private Rigidbody2D body;
    private Transform spriteTransform;
    private SpriteRenderer spriteRenderer;

    private IEnumerator FloatAnimation()
    {
        float spriteYPos = 0f;
        float amplitude = 0.0035f;
        int sideMultiplier = 1;
        float speed = 0.0075f;

        while (true)
        {
            if (spriteYPos > amplitude)
            {
                sideMultiplier = -1;
            }
            else if (spriteYPos < -amplitude)
            {
                sideMultiplier = 1;
            }

            spriteYPos += speed * sideMultiplier * Time.deltaTime;

            spriteTransform.position += new Vector3(0, spriteYPos, 0);
            yield return new WaitForEndOfFrame();
        }
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteTransform = transform.GetChild(0);
        spriteRenderer = spriteTransform.GetComponent<SpriteRenderer>();

        moveSpeed = normalMoveSpeed;

        StartCoroutine(FloatAnimation());
    }

    void Update()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        if (moveDirection.magnitude > 0.1f)
        {
            if (moveDirection.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
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
    }

    void FixedUpdate()
    {
        if(moveDirection.magnitude > 1)
        {
            moveDirection = moveDirection.normalized;
        }

        body.MovePosition(body.position + moveDirection * moveSpeed / 1000);
    }
}
