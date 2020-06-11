using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EndTurnButton : TacticsMove
{

    public TacticsMove tacticsMove;

 public void OnClick()
    {
        endingTurn = true;
    }




}
