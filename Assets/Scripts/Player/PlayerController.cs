using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;
    private Rigidbody2D rb;

    private Transform playerTransform;

    private BoundaryController boundaryController;

    private void Awake()
	{
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerTransform = GetComponent<Transform>();

        boundaryController = FindObjectOfType<BoundaryController>();
    }

    private void FixedUpdate()
	{
        MovementControl();
        if (!boundaryController.IsWithinBoundary(playerTransform))
        {
            boundaryController.RestrictPosition(playerTransform);
        }
    }

    //==================================Movement and animations============================================
    private void MovementControl()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        rb.velocity = movement * speed;
    }
}
