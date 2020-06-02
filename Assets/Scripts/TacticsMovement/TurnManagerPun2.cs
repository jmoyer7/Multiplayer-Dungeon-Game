using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TurnManagerPun2 : TacticsMove
{
    public static bool EndOfTurn = false;
    public static bool EndOfEnemyTurn = false;

    int turnCount = 0;

    public void OnGameBegins()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            BeginTurn();
            turnCount++;
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
