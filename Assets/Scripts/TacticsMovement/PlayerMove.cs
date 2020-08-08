using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class PlayerMove : TacticsMove 
{
    public LayerMask IgnoreMe;

    public GameObject enemy; 

    // Use this for initialization
    void Start () 
	{
        Init();
        //enemy = GameObject.FindGameObjectsWithTag("NPC");
        enemy = GameObject.Find("enemy");
       
    }
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        
        //COuld be problematic
        //if (NPCMove.IsMoving)
        //{
        //    return;
        //}

        Debug.DrawRay(transform.position, transform.forward);
        if (!turn || !base.photonView.IsMine || enemyTurn)
        {
            return;
        }

        if (!moving && base.photonView.IsMine)
        {
            FindSelectableTiles();
            CheckMouse();
        }
        else
        {
            Move(this.gameObject.GetPhotonView());
        }
	}

    //public void ()
    

    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
        

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~IgnoreMe))
        {
            if (hit.collider.tag == "Tile" && !EventSystem.current.IsPointerOverGameObject())
            {

                Tile t = hit.collider.GetComponent<Tile>();

                if (t.selectable)
                {
                    MoveToTile(t);
                }
            }
        }
        }
        
        
    }
}
