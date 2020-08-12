using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour
{

    public Sprite regular;
    public Sprite mouseOver;
    public Sprite mouseClicked;
    public TextMeshPro buttonText;

    public static int attacksThisTurn = 0;

    PlayerStats playerStats;

    public static GameObject activePlayer;

    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        attacksThisTurn++;
        print("ATTACKING");
        if (attacksThisTurn < 3)
        {
            this.transform.GetComponentInParent<NPCStatus>().health -= QuickInstantiate.myPlayer.GetComponent<PlayerStats>().attack;
            print(this.transform.GetComponentInParent<NPCStatus>().health);
            this.transform.parent.gameObject.GetComponent<PhotonView>().RPC("SetHealth", RpcTarget.All, this.transform.GetComponentInParent<NPCStatus>().health);
        }
    }

    

    private void OnMouseEnter()
    {

    }

    private void OnMouseExit()
    {

    }

    private void OnMouseUpAsButton()
    {

    }
}