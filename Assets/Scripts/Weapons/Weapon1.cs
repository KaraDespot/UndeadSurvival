using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon1 : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        AdjustPlayerFacingDirection();
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = GetMousePosition();
        Vector3 playerPos = GetPlayerScreenPosition();

        if (mousePos.x > playerPos.x)
            anim.SetBool("IsFacingLeft", false);
        else if (mousePos.x < playerPos.x)
            anim.SetBool("IsFacingLeft", true);
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        return mousePos;
    }

    private Vector3 GetPlayerScreenPosition() //getting player's position
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }
}
