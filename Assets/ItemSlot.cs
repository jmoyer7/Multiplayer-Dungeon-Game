using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        print("Dropped");
        if (eventData.pointerDrag != null)
        {
            float yCoord;
            float xCoord;
            yCoord = this.transform.position.y;
            xCoord = this.transform.position.x + 50f;

            Vector3 newCoords = new Vector3(xCoord, yCoord, 0);
            eventData.pointerDrag.transform.position = newCoords;
            eventData.pointerDrag.transform.SetParent(this.transform);
        }
        }
}
