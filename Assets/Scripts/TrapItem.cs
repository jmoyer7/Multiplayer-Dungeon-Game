using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrapItem : MonoBehaviour, IPointerDownHandler
{
    

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked++;
        if (clicked == 1) clicktime = Time.time;

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            setTrap(eventData);
            clicked = 0;
            clicktime = 0;
            

        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;

    }

    public void setTrap(PointerEventData eventData)
    {
        print("SETTING TRAP HERE");
        PhotonNetwork.Instantiate("Trap", GameObject.Find("Player(Clone)").transform.position, Quaternion.identity);
        
    }

}

    

