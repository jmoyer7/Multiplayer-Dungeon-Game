using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    public Item item;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 lastPosition;

    [SerializeField] private Canvas canvas;

    

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

  
    public void OnPointerDown(PointerEventData eventData)
    {
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastPosition = transform.position;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(GameObject.Find("Inventory").transform);
        transform.SetAsLastSibling();

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            transform.position = lastPosition;
        }
        canvasGroup.blocksRaycasts = true;

    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
        
    }
}
