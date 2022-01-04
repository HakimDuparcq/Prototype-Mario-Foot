using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1 : MonoBehaviour
{
    public float speed = 5;
    private Vector2 movementInput;

    private bool Shoot;
    Rigidbody ChildrenRb;
    Quaternion target;
    Vector3 direction;

    public GameObject FirstChildren;
    GameObject ActualChildren;

 

    void Start()
    {

        ActualChildren = FirstChildren;
        FirstChildren.transform.parent = gameObject.transform;
        ChildrenRb = gameObject.GetComponentInChildren<Rigidbody>();
        target = ChildrenRb.rotation;
    }
    
    public void Update()
    {
        

        if(Balle1.Instance.ActualPlayer!= ActualChildren)
        {
            ChangeChildren();
        }

        if (movementInput.x != 0 || movementInput.y != 0)
        {
            direction = new Vector3(movementInput.x, 0, movementInput.y).normalized;

            //rb.AddForce((transform.forward)* speed );
            ChildrenRb.MovePosition(ChildrenRb.position + direction * speed * Time.deltaTime);

            target = Quaternion.LookRotation(direction, Vector3.up);
        }


        ChildrenRb.MoveRotation(Quaternion.Slerp(ChildrenRb.rotation, target, Time.deltaTime * 10));


    }

    private void ChangeChildren()
    {
        ActualChildren.transform.parent = null;
        Balle1.Instance.ActualPlayer.transform.parent=this.gameObject.transform;
        ActualChildren = Balle1.Instance.ActualPlayer;
        ChildrenRb = gameObject.GetComponentInChildren<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    public void OnPass(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Balle1.Instance.OnPasse();
        }
    }
    
}