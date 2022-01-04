using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class J1join : MonoBehaviour
{
    private Vector2 movementInput;
    public float speed = 5;
    Vector3 direction;
    Quaternion target;
    Rigidbody ChildrenRb;

    float shootForce = 0;
    public GameObject ActualChildren;

    GameObject Team1, Team2;
    int playerTeam = 0;

    void Start()
    {
        GameObject parent = GameObject.Find("Players");
        transform.parent = parent.transform;
        gameObject.name = "J"+ parent.transform.childCount;


        Team1 = GameObject.Find("Team1");
        Team2 = GameObject.Find("Team2");

        if (gameObject.name == "J1")
        {
            ActualChildren = Team1.transform.GetChild(0).gameObject;
            Team1.transform.GetChild(0).SetParent(gameObject.transform);
            playerTeam = 1;
            
        }
        if (gameObject.name == "J2")
        {
            ActualChildren = Team2.transform.GetChild(0).gameObject;
            Team2.transform.GetChild(0).SetParent(gameObject.transform);
            playerTeam = 2;
        }
        ChildrenRb = gameObject.GetComponentInChildren<Rigidbody>();
        //target = ChildrenRb.rotation;


        

    }

    
    void Update()
    {
        

        if (movementInput.x != 0 || movementInput.y != 0)
        {
            direction = new Vector3(movementInput.x, 0, movementInput.y).normalized;

            
            ChildrenRb.MovePosition(ChildrenRb.position + direction * speed * Time.deltaTime);

            target = Quaternion.LookRotation(direction, Vector3.up);
        }

        ChildrenRb.MoveRotation(Quaternion.Slerp(ChildrenRb.rotation, target, Time.deltaTime * 10));

        
        if (Balle1.Instance.ActualPlayer != ActualChildren && Balle1.Instance.ActualTeam==playerTeam )//si le joueur qui a la balle n'est pas le joueur controlé && 
        {
            Debug.Log("change joueur"+ Balle1.Instance.ActualPlayer);
            ChangeChildrenOnPass();
        }   
        
        



    }
    private void ChangeChildrenOnPass()
    {
        if (playerTeam==1)
        {
            ActualChildren.transform.parent = Team1.transform;
        }
        else
        {
            ActualChildren.transform.parent = Team2.transform;
        }
        
        Balle1.Instance.ActualPlayer.transform.parent = this.gameObject.transform;
        ActualChildren = Balle1.Instance.ActualPlayer;
        ChildrenRb = gameObject.GetComponentInChildren<Rigidbody>();
    }

    private void ChangePlayer()
    {
        if (playerTeam == 1)
        {
            Vector3 posObj = new Vector3(100000000, 12000000000000, 100000000000000);
            
            GameObject memo = ActualChildren;
            ActualChildren.transform.parent = Team1.transform;
            foreach (GameObject player in Balle1.Instance.PlayersTeam1)
            {
                if (player != memo && (player.transform.position - Balle1.Instance.transform.position  ).magnitude < posObj.magnitude)
                {
                    ActualChildren = player;
                    posObj = (player.transform.position - memo.transform.position);
                   
                }
               
            }
            ActualChildren.transform.parent = this.gameObject.transform;//je dis qui est le parent 
            ChildrenRb = gameObject.GetComponentInChildren<Rigidbody>();

            Debug.Log(ActualChildren);
        }

        else
        {
            Vector3 posObj = new Vector3(100000000, 12000000000000, 100000000000000);

            ActualChildren.transform.parent = Team2.transform;
            foreach (GameObject player in Balle1.Instance.PlayersTeam2)
            {
                if (player != ActualChildren && (ActualChildren.transform.position - Balle1.Instance.transform.position).magnitude<posObj.magnitude )
                {
                    ActualChildren = player;
                    ChildrenRb = gameObject.GetComponentInChildren<Rigidbody>();
                    ActualChildren.transform.parent = this.gameObject.transform;
                    posObj = player.transform.position;
                }
            }
        }

    }




    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    public void OnPass(InputAction.CallbackContext value)
    {
        if (value.started && Balle1.Instance.transform.parent!=null && Balle1.Instance.ActualTeam== playerTeam) //si ta la balle
        { 
            Balle1.Instance.OnPasse();
        }
        else if(value.started)
        {
            ChangePlayer();
            Debug.Log("Pour nico quand o appayé");
        }
    }

    public void OnShoot(InputAction.CallbackContext value)
    {
        if (Balle1.Instance.transform.parent != null)
        {
            if (value.started)
                shootForce = Time.time;

            if (value.canceled)
            {
                shootForce = (Time.time - shootForce) * 20;
                Debug.Log(shootForce);
                Balle1.Instance.OnShoot(ActualChildren.transform.forward, shootForce);
                shootForce = 0;
            }
        }
    }
}
