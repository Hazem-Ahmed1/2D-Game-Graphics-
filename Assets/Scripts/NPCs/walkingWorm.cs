using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingWorm : MonoBehaviour
{


    public Transform startPoint;
    public Transform endPoint;
    private float speed = 5f;
    private bool facingRight = true;
    Animator animator;

    private Transform targetPoint; // Current target point

    private void Start()
    {
        animator = GetComponent<Animator>();
        // Set the initial target point to startPoint
        targetPoint = startPoint;
    }

    private void Update()
    {
        animator.SetBool("walkWorm", true);
        // Move towards the target point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Check if reached the target point
        if (transform.position == targetPoint.position)
        {
            if (targetPoint == startPoint)
                targetPoint = endPoint;
            else
                targetPoint = startPoint;

            FlipCharacter();
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
