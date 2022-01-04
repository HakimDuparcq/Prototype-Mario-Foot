using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gogogogogogoalllll : MonoBehaviour
{
    public Move Ballon;
    
    void Start()
	{
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Ballon")
        {
            Debug.Log("Gogogogogogoalllllllllllllllllllllllllllll");
            Ballon.Reset();
        }
    }
}

