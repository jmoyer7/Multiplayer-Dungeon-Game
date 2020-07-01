using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIelementsMovement : MonoBehaviour
{

    

    public GameObject target;


    private float speed = 1;

    public bool movingInv = false;

    

    private Vector3 startingPos;

    private Vector3 closedPos;

   

    private void Start()
    {
        
    }


    void Update()
        {
           
        }

    public void openInventory()
    {
        Vector2 pos = transform.position;
        
            pos.y += 100f;
            transform.position = pos;
            
        
    }


    

    public void closeInventory()
    {
        Vector2 pos = transform.position;

        pos.y -= 100f;
        transform.position = pos;
        //GetComponent<RectTransform>().anchoredPosition = GameObject.Find("Position").transform.position;
    }
}