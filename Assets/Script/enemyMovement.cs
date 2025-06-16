using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBodyEnemy;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int startDirection = -1; //move ke kiri dulu.

    public int currentDirection;
    private float halfWidth;

    private float halfHeight;

    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        halfWidth = spriteRenderer.bounds.extents.x;
        halfHeight = spriteRenderer.bounds.extents.y;

        currentDirection = startDirection;
        spriteRenderer.flipX = startDirection == 1 ? true : false;
    }

    private void FixedUpdate()
    {
        movement.x = speed * currentDirection;
        movement.y = rigidBodyEnemy.velocity.y;
        rigidBodyEnemy.velocity = movement;

        FlipEnemy();
    }

    private void FlipEnemy()
    {
        Vector2 rightPos = transform.position;
        Vector2 leftPos = transform.position;
        rightPos.x += halfWidth;
        leftPos.x -= halfWidth;

        if (rigidBodyEnemy.velocity.x > 0 )
        {
            if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Ground")))
            {
                currentDirection *= -1;
                spriteRenderer.flipX = false;
            }
            else if (!Physics2D.Raycast(rightPos, Vector2.down, halfHeight + 0.1f, LayerMask.GetMask("Ground")))
            {
                currentDirection *= -1;
                spriteRenderer.flipX = false;
            }
        } else if (rigidBodyEnemy.velocity.x < 0 )
        {
            if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("Ground")))
            {
                currentDirection *= -1;
                spriteRenderer.flipX = true;
            }
            else if (!Physics2D.Raycast(leftPos, Vector2.down, halfHeight + 0.1f, LayerMask.GetMask("Ground")))
            {
                currentDirection *= -1;
                spriteRenderer.flipX = true;
            }
        }

        Debug.DrawRay(transform.position, Vector2.right * (halfWidth + 0.1f), Color.red);
        Debug.DrawRay(transform.position, Vector2.left * (halfWidth + 0.1f), Color.red);
        Debug.DrawRay(transform.position, Vector2.down * (halfHeight + 0.1f), Color.red);
    }
}
