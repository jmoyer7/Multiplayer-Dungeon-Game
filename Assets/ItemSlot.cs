using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public float xOffset;
    public float yOffset;

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
                xCoord = this.transform.position.x + xOffset;
            }
            else
            {
                xCoord = this.transform.position.x + yOffset;
            }
            Vector3 newCoords = new Vector3(xCoord, yCoord, 0);
            eventData.pointerDrag.transform.position = newCoords;
            eventData.pointerDrag.transform.SetParent(this.transform);
        }
        }
}
