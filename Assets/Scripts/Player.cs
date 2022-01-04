using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int pulse = 10;
    public Rigidbody rb;
    public Move Balle;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("update");
        if (transform.childCount == 1)//transform.childCount==1
        {
            
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
                
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(Vector3.left * pulse);
                //Debug.Log("S");
            }
        }
        
    }
}
