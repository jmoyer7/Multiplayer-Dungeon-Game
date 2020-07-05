using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArmorSlot : MonoBehaviour
{
    public GameObject player;

    public void start()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {

        if (eventData.pointerDrag != null)
        {
            float yCoord;
            float xCoord;
            yCoord = this.transform.position.y;

            //Temporary Fix(Sets offset of item sprite in slot)
            if (PhotonNetwork.IsMasterClient)
            {
                xCoord = this.transform.position.x + -10f;
            }
            else
            {
                xCoord = this.transform.position.x + 25f;
            }
            Vector3 newCoords = new Vector3(xCoord, yCoord, 0);
            eventData.pointerDrag.transform.position = newCoords;
            eventData.pointerDrag.transform.SetParent(this.transform);

            player = transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
            player.GetComponent<PlayerStats>().updateEquipment(transform.GetChild(0).GetComponent<ItemDragHandler>().equipment);


        }
    }
}
