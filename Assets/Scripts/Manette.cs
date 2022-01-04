using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Manette : MonoBehaviour
{
    private Vector2 movementInput;
    void Start()
    {
        
    }

    public Vector2 OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
        return ctx.ReadValue<Vector2>();
    }
    void Update()
    {
        
    }

}
