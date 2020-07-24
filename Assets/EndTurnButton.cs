using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EndTurnButton : TacticsMove
{

    public TacticsMove tacticsMove;

 public void OnClick()
    {
           
        if (TacticsMove.myTurn)
        {
            endingTurn = true;
            UIButton.attacksThisTurn = 0;
        }
    }




}
