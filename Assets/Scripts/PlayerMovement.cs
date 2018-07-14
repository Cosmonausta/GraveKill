using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float timeBetweenScytheShines = 5;

    private float idleTimer;
    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Rigidbody2D rb;
    private Animator animator;
    private bool currentlyMoving = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        moveVelocity = moveDirection * moveSpeed;

        currentlyMoving = SetMovingAnimation();

        PlayScytheShineAnimationOnTimer();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVelocity;
    }

    private void PlayScytheShineAnimationOnTimer()
    {
        if(!currentlyMoving)
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= timeBetweenScytheShines)
            {
                animator.Play("ScytheShine");
                idleTimer = 0f;
            }
        }
        else
        {
            idleTimer = 0f;
        }
    }

    private bool SetMovingAnimation()
    {
        if (rb.velocity != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
            return true;
        }
        else
        {
            animator.SetBool("isMoving", false);
            return false;
        }
    }
}
