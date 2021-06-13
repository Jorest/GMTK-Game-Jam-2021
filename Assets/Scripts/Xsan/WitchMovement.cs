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
    private Animator animator;
    private Transform broomEnd;

    //private float spriteYPos = 0f;
    //private float amplitude = 0.0035f;
    //private int sideMultiplier = 1;
    //private float speed = 0.0075f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteTransform = transform.GetChild(0);
        spriteRenderer = spriteTransform.GetComponent<SpriteRenderer>();
        broomEnd = transform.GetChild(1);
        animator = transform.GetChild(0).GetComponent<Animator>();

        moveSpeed = normalMoveSpeed;
    }

    void Update()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        if(moveDirection.magnitude > 1)
        {
            moveDirection = moveDirection.normalized;
        }

        if (moveDirection.magnitude > 0.1f)
        {
            float xLocal = 0f;
            float yLocal = 0f;

            if (moveDirection.x > 0)
            {
                spriteRenderer.flipX = true;
                xLocal = -0.375f;

            }
            else if(moveDirection.x < 0)
            {
                spriteRenderer.flipX = false;
                xLocal = 0.375f;
            }

            if (moveDirection.y > 0)
            {
                //spriteRenderer.sprite = backSprite;
                yLocal = -0.2f;
            }
            else
            {
                //spriteRenderer.sprite = frontSprite;
                yLocal = 0.135f;
            }

            animator.SetFloat("YVector", -moveDirection.y);

            broomEnd.localPosition = new Vector2(xLocal, yLocal);
        }

        //

        if (moveDirection.magnitude > 1)
        {
            moveDirection = moveDirection.normalized;
        }

        body.MovePosition(body.position + moveDirection * moveSpeed * Time.deltaTime / 20);

        //

        //if (spriteYPos > amplitude)
        //{
        //    sideMultiplier = -1;
        //}
        //else if (spriteYPos < -amplitude)
        //{
        //    sideMultiplier = 1;
        //}

        //spriteYPos += speed * sideMultiplier * Time.deltaTime;

        //spriteTransform.position += new Vector3(0, spriteYPos, 0);
    }

    //void FixedUpdate()
    //{
    //    if(moveDirection.magnitude > 1)
    //    {
    //        moveDirection = moveDirection.normalized;
    //    }

    //    body.MovePosition(body.position + moveDirection * moveSpeed / 1000);
    //}
}
