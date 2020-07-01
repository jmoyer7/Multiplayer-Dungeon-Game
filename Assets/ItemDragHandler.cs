using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

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
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(GameObject.Find("Inventory").transform);
        transform.SetAsLastSibling();

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("END DRAG");
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
        
    }
}
