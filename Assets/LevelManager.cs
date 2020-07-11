using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LevelManager : MonoBehaviourPun
{
    public void SetTrap(int playerID)
    {
        photonView.RPC("SyncTrap", RpcTarget.All,playerID);
    }

    [PunRPC]
    void SyncTrap(int playerID)
    {
        RaycastHit hit;
        GameObject tile = null;

        GameObject targetPlayer = PhotonView.Find(playerID).gameObject;

        if (Physics.Raycast(targetPlayer.transform.position, -Vector3.up, out hit, 1))
        {

            tile = hit.collider.gameObject;
            tile.AddComponent<TrapTrigger>();
        }
    }
}
