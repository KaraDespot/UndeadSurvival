using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    public float initialRadius = 0.8f; 
    public float circularMotionSpeed = 1f; 
    public Vector3 centerOffset;

    public Camera mainCamera;

    private float currentRadius;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        currentRadius = initialRadius;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        MoveAroundCircleTowardsMouse();
    }

    private void MoveAroundCircleTowardsMouse()
    {
        if (mainCamera != null)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Vector3 direction = mousePosition - (transform.parent.position + centerOffset);
            direction.z = 0f; 

            direction.Normalize();

            Vector3 targetPosition = transform.parent.position + centerOffset + direction * currentRadius;

            transform.position = targetPosition;

            float lookAtAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(lookAtAngle, Vector3.forward);

            if (mousePosition.x < transform.parent.position.x)
                spriteRenderer.flipY = true;
            else
                spriteRenderer.flipY = false;
        }
    }
}
