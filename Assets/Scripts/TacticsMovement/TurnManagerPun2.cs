﻿using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;

public class TurnManagerPun2 : TacticsMove
{


    public static bool EndOfTurn = false;
    public static bool EndOfEnemyTurn = false;

    public static int turnCount = 0;

    private const byte SEND_TURN_EVENT = 0;

    public void OnGameBegins()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            BeginTurn();
            
        }
    }



    [PunRPC]
    public void SendTurn(bool EndOfTurn)
    {
        
        
        if (EndOfTurn)
        {
            BeginTurn();


        }
        
    }


    //Needs work, slightly bugged
    //Put turnCount variable in Tacticsmove instead
    public static void SendTurnEvent()
    {
        turnCount++;
        if (turnCount == 3)
        {
            turnCount = 0;
        }

        
        print(turnCount);
        TurnManagerPun2.EndOfTurn = true;

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            TargetActors = new int[] { PlayerNumbering.SortedPlayers[turnCount].ActorNumber }

            
            //GameObject.Find("Player").GetComponent<PhotonView>().ViewID
        };

        

        //TurnManagerPun2.EndOfTurn = true;

        object[] datas = new object[] { TurnManagerPun2.EndOfTurn };

        PhotonNetwork.RaiseEvent(SEND_TURN_EVENT, datas, raiseEventOptions, SendOptions.SendReliable);

        
    }


    

    public void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;      
    }
     
    private void NetworkingClient_EventReceived(EventData obj)
    {

        if (obj.Code == SEND_TURN_EVENT)
        {
            
            print("Here");
            TurnManagerPun2.EndOfTurn = true;
            print("YES");
            BeginTurn();

            
        }
    }

    public void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }












    // Start is called before the first frame update
    void Start()
    {
        if(turnCount == 0)
        {
            OnGameBegins();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
