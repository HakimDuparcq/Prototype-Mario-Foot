using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{

    public Rigidbody rb_balle;
    public int pulse = 1;
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject Ballon;
    public int Cloche = 1;
    public float testForce = 5f;

    Vector3 posObj1;
    Vector3 posObj3;
    Vector3 posCloche;
    List<GameObject> Players = new List<GameObject>();
    public Player ply;
    private Vector2 movementInput;

    private void Start()
	{
        Players.Add(obj1);
        Players.Add(obj2);
        Players.Add(obj3);
        rb_balle = GetComponent<Rigidbody>();
        rb_balle.useGravity = true;


        
    }
    
    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
        //croix = ctx.ReadValue<PenButton>;
    }
    

    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddForce(Vector3.forward * pulse);
            //Debug.Log("Q");
        }
        if (Input.GetKey(KeyCode.D))
        {           
            rb.AddForce(Vector3.back * pulse);
            //Debug.Log("D");
        }
        if (Input.GetKey(KeyCode.Z))
        {           
            rb.AddForce(Vector3.right * pulse);
            //Debug.Log("Z");
        }
        if (Input.GetKey(KeyCode.S))
        {           
            rb.AddForce(Vector3.left * pulse);
            //Debug.Log("S");
        }
        */
        /*
        if (movementInput.)//Input.GetKeyDown(KeyCode.O)
        {

            rb_balle.useGravity = true;
            rb_balle.isKinematic = false;
           
            ResetVelocity();
            posObj3 = GotoCloser(obj1, obj2, obj3);
            //posObj1 = obj1.transform.position - Ballon.transform.position;
            //Debug.Log(posObj1);
            rb_balle.AddForce(posObj3, ForceMode.Impulse);
            Ballon.transform.parent = null;
        }
        */
        /*
        if (Input.GetKeyDown(KeyCode.K))
        {
            rb_balle.useGravity = true;
            rb_balle.isKinematic = false;
            
            ResetVelocity();
            posObj3=GotoCloser(obj1, obj2, obj3);
            //posObj3 = obj3.transform.position - Ballon.transform.position;
            //Debug.Log(posObj3);
            posCloche = posObj3 + new Vector3(-posObj3.x/ testForce, Cloche, -posObj3.z/testForce);
            //Debug.Log(posCloche);
            rb_balle.AddForce(posCloche , ForceMode.Impulse);
            Ballon.transform.parent = null;
        }
        */
    }

    public Vector3 GotoCloser(GameObject obj1, GameObject obj2, GameObject obj3)
    {

        posObj3 = new Vector3(100000000,12000000000000,100000000000000);
        foreach (GameObject  player in Players)
        {
            if (Ballon.transform.parent!=player.transform && (player.transform.position - Ballon.transform.position).magnitude < posObj3.magnitude)
            {
                posObj3 = player.transform.position - Ballon.transform.position;
                print(player);
            }
            if (Ballon.transform.parent != player)
            {
                print(Ballon.transform.parent+" "+ player.transform);
            }
        }
        /*
        posObj3 = obj1.transform.position - Ballon.transform.position;
        if ((obj2.transform.position - Ballon.transform.position).magnitude< posObj3.magnitude)
        {
            posObj3 = obj2.transform.position - Ballon.transform.position;
        }
        if ((obj3.transform.position - Ballon.transform.position).magnitude < posObj3.magnitude)
        {
            posObj3 = obj3.transform.position - Ballon.transform.position;
        }
        */
        return posObj3;
    }


   

    public void ResetVelocity()
    {
        rb_balle.velocity = Vector3.zero;


    }
    public void Reset()
	{
        transform.position = new Vector3(0, 5, 0);
        rb_balle.velocity = Vector3.zero;
    }

    public void OnTriggerEnter(Collider other)
    {
        ResetVelocity();
        rb_balle.useGravity = false;
        //Debug.Log("other");
        Ballon.transform.parent = other.transform;
        //Debug.Log("colide"+other);

        ResetVelocity();
        print("ok");
        rb_balle.isKinematic = true;
    }

    public void OnCollisionEnter(Collision other)
    {
        if (Players.Contains(  other.gameObject))
        {
            ResetVelocity();
            rb_balle.useGravity = false;
            //Debug.Log("other");
            Ballon.transform.parent = other.transform;
            //Debug.Log("colide"+other);
            ResetVelocity();
           
            rb_balle.isKinematic = true;

            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        }
        
    }



    public void OnPasse(InputAction.CallbackContext value)
    {
        if (value.started)//Input.GetKeyDown(KeyCode.O)
        {
            rb_balle.useGravity = true;
            rb_balle.isKinematic = false;

            ResetVelocity();
            posObj3 = GotoCloser(obj1, obj2, obj3);
            //posObj1 = obj1.transform.position - Ballon.transform.position;
            //Debug.Log(posObj1);
            rb_balle.AddForce(posObj3, ForceMode.Impulse);
            Ballon.transform.parent = null;
            Debug.LogError("passe");
        }
    }






}
