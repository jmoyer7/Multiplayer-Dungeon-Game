using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIelementsMovement : MonoBehaviour
{

    

    public GameObject target;


    private float speed = 1;

    public bool movingInv = false;

    private Vector3 startingPos;

   

    private void Start()
    {
        
    }


    void Update()
        {
            if (movingInv)
            {
               openInventory();
            }
        }
    

    public void openInventory()
    {
        startingPos = GetComponent<RectTransform>().anchoredPosition;

        startingPos.y = speed * Time.deltaTime;

        GetComponent<RectTransform>().anchoredPosition = startingPos;
    }
}