using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Balle1 : MonoBehaviour
{
    public Rigidbody rb_balle;
    Vector3 posObj;
    //public GameObject obj3;
    public GameObject Ballon;

    public List<GameObject> PlayersTeam1 = new List<GameObject>();
    public List<GameObject> PlayersTeam2 = new List<GameObject>();

    public static Balle1 Instance;

    public GameObject ActualPlayer;
    public int ActualTeam = 0;
    public float speed = 3f;
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent!=null)
        {
            if (gameObject.transform.parent.transform.parent.name == "J1")
            {
                ActualTeam = 1;
                Debug.Log("ActualTeam = 1");
            }
            else if (gameObject.transform.parent.transform.parent.name == "J2")
            {
                ActualTeam = 2;
            }
        }
        else
        {
            ActualTeam = 0;

        }
        
        //Debug.Log("actuelplayer "+ActualPlayer);
    }
    public void ResetVelocity()
    {
        rb_balle.velocity = Vector3.zero;
    }

    public void OnPasse()
    {
        
        rb_balle.useGravity = true;
        rb_balle.isKinematic = false;

        ResetVelocity();
        GameObject closerPlayer = GotoCloser(Ballon);
        ActualPlayer.transform.rotation = Quaternion.LookRotation(closerPlayer.transform.position-ActualPlayer.transform.position, Vector3.up);
        //posObj1 = obj1.transform.position - Ballon.transform.position;
        //Debug.Log(posObj1);
        rb_balle.AddForce((closerPlayer.transform.position - ActualPlayer.transform.position) * speed, ForceMode.Impulse);
        Ballon.transform.parent = null;
        //Debug.LogError("passe");

        ActualTeam = 0;
    }

    public void OnShoot(Vector3 direction,float shootforce)
    {
        rb_balle.useGravity = true;
        rb_balle.isKinematic = false;

        ResetVelocity();
       
        rb_balle.AddForce(direction*shootforce * speed, ForceMode.Impulse);
        Ballon.transform.parent = null;
    }
    public GameObject GotoCloser(GameObject closerTo)
    {
        posObj = new Vector3(100000000, 12000000000000, 100000000000000);
        GameObject closerPlayer=null;
        if (ActualTeam==1 )
        {
            foreach (GameObject player in PlayersTeam1)
            {
                if (closerTo.transform.parent != player.transform && (player.transform.position - closerTo.transform.position).magnitude < posObj.magnitude)
                {
                    posObj = player.transform.position - closerTo.transform.position;
                    closerPlayer = player;
                }
            }
        }
        else if (ActualTeam == 2 )
        {
            foreach (GameObject player in PlayersTeam2)
            {
                if (closerTo.transform.parent != player.transform && (player.transform.position - closerTo.transform.position).magnitude < posObj.magnitude)
                {
                    posObj = player.transform.position - closerTo.transform.position;
                    closerPlayer = player;
                }
            }
        }
        return closerPlayer ;
    }

  
    public void OnCollisionEnter(Collision other)
    {
        if (PlayersTeam1.Contains(other.gameObject))
        {
            ActualPlayer = other.gameObject;
            ResetVelocity();
            rb_balle.useGravity = false;

            
            Ballon.transform.parent = other.transform;
            Ballon.transform.localPosition = new Vector3(0, 0, 2);
            ResetVelocity();

            rb_balle.isKinematic = true;

            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            ActualTeam = 1;
           
        }

        if (PlayersTeam2.Contains(other.gameObject))
        {
            ActualPlayer = other.gameObject;
            ResetVelocity();
            rb_balle.useGravity = false;


            Ballon.transform.parent = other.transform;
            Ballon.transform.localPosition = new Vector3(0, 0, 2);

            //Debug.Log("other");
            
            //Debug.Log("colide"+other);
            ResetVelocity();

            rb_balle.isKinematic = true;

            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            ActualTeam = 2;
        }

    }

}
